using Azure;
using FoundChildrenGP.BL;
using LostChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Polly;
using System.Buffers.Text;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static LostChildrenGP.Controllers.SearchController;

namespace LostChildrenGP.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        IFoundChildren _oClsFoundedChildren;
        private readonly CloudinaryService _cloudinaryService;
        UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ReportController> _logger;
        public ReportController(IFoundChildren FoundedChildren, CloudinaryService cloudinaryService,
             UserManager<ApplicationUser> userManager, ILogger<ReportController> logger)
        {
            _oClsFoundedChildren = FoundedChildren;
            _cloudinaryService = cloudinaryService;
            _userManager = userManager;
            _logger = logger;
        }
        public IActionResult ReportForm()
        {
            return View(new FoundChild());
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ReportResult(FoundChild model, IFormFile File)
        //{
        //    if (File != null && File.FileName != "")
        //    {
        //        var arr = File.FileName.ToLower().Split(".");
        //        if (arr[arr.Length - 1] == "png" || arr[arr.Length - 1] == "jpg" || arr[arr.Length - 1] == "jpeg")
        //        {
        //            ModelState.Remove("FoundChildImage");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("FoundChildImage", "Only PNG, JPG, and JPEG images are allowed");
        //        }
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        return View("ReportForm", model);
        //    }

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        await File.CopyToAsync(memoryStream);
        //        var fileBytes = memoryStream.ToArray();
        //        var base64String = Convert.ToBase64String(fileBytes);

        //        var payload = new
        //        {
        //            image = base64String
        //        };

        //        using (var httpClient = new HttpClient())
        //        {
        //            var aiApiUrl = "https://ozeniny-fastapi-deployment.hf.space/report";

        //            // Corrected JSON serialization and content type
        //            var jsonContent = new StringContent(
        //                System.Text.Json.JsonSerializer.Serialize(payload),
        //                Encoding.UTF8,
        //                "application/json");

        //            var response = await httpClient.PostAsync(aiApiUrl, jsonContent);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var result = await response.Content.ReadAsStringAsync();
        //                var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(result);
        //                if (jsonResponse != null && jsonResponse.TryGetValue("image_embedding", out var embedding))
        //                {
        //                    model.FC_Embedding = embedding;
        //                }
        //                else
        //                {
        //                    return Redirect("/Error/E505");
        //                }

        //            }
        //            else
        //            {
        //                return StatusCode((int)response.StatusCode,
        //                    $"AI API request failed: {response.ReasonPhrase}");
        //            }
        //        }

        //        model.FoundChildImage = "test";
        //        //model.FoundChildImage = await _cloudinaryService.UploadImageAsync(File, "FoundedChildren");
        //        var user = await _userManager.GetUserAsync(User);
        //        _oClsFoundedChildren.Save(model, user.Id);
        //        return Redirect("/Home/Index");
        //    }
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportResult(FoundChild model, IFormFile File)
        {
            if (File != null && File.FileName != "")
            {
                var arr = File.FileName.ToLower().Split(".");
                if (arr[arr.Length - 1] == "png" || arr[arr.Length - 1] == "jpg" || arr[arr.Length - 1] == "jpeg")
                {
                    ModelState.Remove("FoundChildImage");
                }
                else
                {
                    ModelState.AddModelError("FoundChildImage", "Only PNG, JPG, and JPEG images are allowed");
                }
            }
            if (!ModelState.IsValid)
            {
                return View("ReportForm", model);
            }


            model.FC_Embedding = await ProcessImageWithAI(File);
            if (model.FC_Embedding == "False")
                return Redirect("/Error/E500");
            model.FoundChildImage = await _cloudinaryService.UploadImageAsync(File, "FoundedChildren");
            var user = await _userManager.GetUserAsync(User);
            _oClsFoundedChildren.Save(model, user.Id);
            return Redirect("/Home/SuccessPage"); 
            
        }


        private async Task<string> ProcessImageWithAI(IFormFile file)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var base64String = Convert.ToBase64String(memoryStream.ToArray());
                var payload = new
                {
                    image = base64String
                };
                // Configure HttpClient with appropriate timeout
                using var httpClient = new HttpClient();
                //httpClient.Timeout = TimeSpan.FromSeconds(30); 
                var jsonContent = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(payload),
                    Encoding.UTF8,
                    "application/json");
                // Add retry policy for transient failures
                //var retryPolicy = Policy
                //    .Handle<HttpRequestException>()
                //    .Or<TaskCanceledException>() 
                //    .WaitAndRetryAsync(
                //        sleepDurations: new[]
                //        {
                //            TimeSpan.FromSeconds(1),
                //            TimeSpan.FromSeconds(3),
                //            TimeSpan.FromSeconds(5)
                //        },
                //        onRetry: (exception, delay, retryCount, context) =>
                //        {
                //            _logger.LogWarning($"Retry {retryCount} after {delay.TotalSeconds} seconds due to: {exception.Message}");
                //        });

                //var jsonContent = new StringContent(
                //    System.Text.Json.JsonSerializer.Serialize(new { image = base64String }),
                //    Encoding.UTF8,
                //    "application/json");
                var response = await httpClient.PostAsync(
                    "https://ozeniny-fastapi-deployment.hf.space/search",
                    jsonContent);
                if (!response.IsSuccessStatusCode)
                {
                    return "False";
                }

                // Execute with retry policy
                //var response = await retryPolicy.ExecuteAsync(async () =>
                //{
                //    var cts = new CancellationTokenSource();
                //    cts.CancelAfter(httpClient.Timeout); // Ensures timeout enforcement

                //    var response = await httpClient.PostAsync(
                //        "https://ozeniny-fastapi-deployment.hf.space/report",
                //        jsonContent,
                //        cts.Token);

                //    response.EnsureSuccessStatusCode();
                //    return response;
                //});

                var jsonResponse = await response.Content.ReadAsStringAsync();
                //var result = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(jsonResponse);
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                return apiResponse.image_embedding;
            }
            catch 
            {
                return "False";
            }
        }
        public class ApiResponse
        {
            public string image_embedding { get; set; }
        }
    }

}
