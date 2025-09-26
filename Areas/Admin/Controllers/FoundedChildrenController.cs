using FoundChildrenGP.BL;
using LostChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FoundChildrenGP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FoundedChildrenController : Controller
    {
        IFoundChildren _oClsFoundedChildren;
        private readonly CloudinaryService _cloudinaryService;
        UserManager<ApplicationUser> _userManager;
        public FoundedChildrenController(IFoundChildren FoundedChildren, CloudinaryService cloudinaryService,
             UserManager<ApplicationUser> userManager)
        {
            _oClsFoundedChildren = FoundedChildren;
            _cloudinaryService = cloudinaryService;
            _userManager = userManager; 
        }
        public IActionResult List()
        {
            return View(_oClsFoundedChildren.GetAll());
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                ViewBag.Title = "Add Report";
                return View(new FoundChild());
            }
            else
            {
                ViewBag.Title = "Child Information";
                var foundChild = _oClsFoundedChildren.GetFoundChild(Convert.ToInt32(id));
                if (foundChild == null)
                    // To Error 404 or not found
                    return Redirect("/Error/E404?type=Admin");
                return View(foundChild);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(FoundChild model, IFormFile File)
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
                return View("Edit", model);
            }
            model.FC_Embedding = await ProcessImageWithAI(File);
            if (model.FC_Embedding == "False")
                return Redirect("/Error/E500?type=Admin");
            model.FoundChildImage = await _cloudinaryService.UploadImageAsync(File, "FoundedChildren");
            var user = await _userManager.GetUserAsync(User);
            _oClsFoundedChildren.Save(model,user.Id);
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool isDeleted = _oClsFoundedChildren.Delete(id);
            if (isDeleted == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
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
            catch (Exception ex)
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
