using FoundChildrenGP.BL;
using LostChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace LostChildrenGP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SearchController : Controller
    {
        IFoundChildren _oClsFoundedChildren;
        ILostChildren _oClslostChildren;
        UserManager<ApplicationUser> _userManager;
        private readonly CloudinaryService _cloudinaryService;
        public SearchController(IFoundChildren ClsFoundedChildren, UserManager<ApplicationUser> userManager,
            CloudinaryService cloudinaryService, ILostChildren oClslostChildren)
        {
            _oClsFoundedChildren = ClsFoundedChildren;
            _userManager = userManager;
            _cloudinaryService = cloudinaryService;
            _oClslostChildren = oClslostChildren;
        }
        public IActionResult SearchForm()
        {
            return View(new LostChild());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Result(LostChild model, IFormFile File)
        {
            if (File != null && File.FileName != "")
            {
                var arr = File.FileName.ToLower().Split(".");
                if (arr[arr.Length - 1] == "png" || arr[arr.Length - 1] == "jpg" || arr[arr.Length - 1] == "jpeg")
                {
                    ModelState.Remove("LostChildrenImage");
                }
                else
                {
                    ModelState.AddModelError("LostChildrenImage", "Only PNG, JPG, and JPEG images are allowed");
                }
            }
            if (!ModelState.IsValid)
            {
                return View("SearchForm", model);
            }

            try
            {
                // Get all embeddings from database
                var foundedChildren = _oClsFoundedChildren.GetAllSpaicific(model.Gender, model.DateOfBirth.Year);
                var lstEmbedding = foundedChildren.Select(a => a.FC_Embedding).ToList();

                // Convert image to base64
                using var memoryStream = new MemoryStream();
                await File.CopyToAsync(memoryStream);
                var base64String = Convert.ToBase64String(memoryStream.ToArray());

                // Prepare request payload
                var payload = new
                {
                    image = base64String,
                    embedding_strings = lstEmbedding
                };

                using var httpClient = new HttpClient();
                //httpClient.Timeout = TimeSpan.FromSeconds(30);

                // Serialize and send request
                var jsonContent = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(payload),
                    Encoding.UTF8,
                    "application/json");

                var response = await httpClient.PostAsync(
                    "https://ozeniny-fastapi-deployment.hf.space/search",
                    jsonContent);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode,
                        $"AI API request failed: {response.ReasonPhrase}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

                model.LC_Embedding = apiResponse.image_embedding;
                // Get top 5 matches (ordered by similarity)
                var topMatches = apiResponse.similar_embeddings
                    .OrderByDescending(x => x.similarity)
                    .ToList();


                var results = topMatches.Select((match) => new VmSearchResultChildren
                {
                    Child = foundedChildren[match.index],
                    Similarity = match.similarity
                }).ToList();
                model.LostChildrenImage = await _cloudinaryService.UploadImageAsync(File, "LostChildren");
                VmUserSearchResult vm = new VmUserSearchResult()
                {
                    lostChild = model,
                    lstSearchResultChildren = results
                };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(vm));
                var cart = HttpContext.Session.GetString("cart");
                return View(vm);
            }
            catch (Exception ex)
            {
                return Redirect("/Error/E500");
            }
            
        }
        public class ApiResponse
        {
            public string image_embedding { get; set; }
            public List<SimilarEmbedding> similar_embeddings { get; set; }
        }

        public class SimilarEmbedding
        {
            public int index { get; set; }
            public double similarity { get; set; }
        }

        public async Task<IActionResult> Save()
        {
            var vmUserSearchResult = new VmUserSearchResult();
            if (HttpContext.Session.GetString("cart") != null)
            {
                vmUserSearchResult = JsonConvert.DeserializeObject<VmUserSearchResult>(HttpContext.Session.GetString("cart"));
            }
            else
            {
                vmUserSearchResult = new VmUserSearchResult();
            }
            var user = await _userManager.GetUserAsync(User);

            _oClslostChildren.Save(vmUserSearchResult.lostChild, user.Id, vmUserSearchResult.lstSearchResultChildren);
            return Redirect("/Home/Index");
        }
    }
}
