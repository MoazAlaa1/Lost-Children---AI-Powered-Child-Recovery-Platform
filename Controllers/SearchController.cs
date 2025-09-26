using FoundChildrenGP.BL;
using LostChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace LostChildrenGP.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        
        IFoundChildren _oClsFoundedChildren;
        ILostChildren _oClslostChildren;
        UserManager<ApplicationUser> _userManager;
        private readonly CloudinaryService _cloudinaryService;
        public SearchController(IFoundChildren ClsFoundedChildren, ILostChildren lostChildren,
            UserManager<ApplicationUser> userManager, CloudinaryService cloudinaryService)
        {
            _oClsFoundedChildren = ClsFoundedChildren;
            _oClslostChildren = lostChildren;
            _userManager = userManager;
            _cloudinaryService = cloudinaryService;
        }
        public IActionResult SearchForm()
        {

            return View(new LostChild());
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Result(LostChild model, IFormFile File)
        //{
        //    // File validation (same as before)
        //    if (!ModelState.IsValid)
        //    {
        //        return View("SearchForm", model);
        //    }

        //    try
        //    {
        //        // Get all founded children and parse their embeddings
        //        var foundedChildren = _oClsFoundedChildren.GetAll().ToList();
        //        var embeddings = foundedChildren
        //            .Select(x => ParseEmbedding(x.FC_Embedding))
        //            .ToList();

        //        // Convert image to base64 and get its embedding
        //        using var memoryStream = new MemoryStream();
        //        await File.CopyToAsync(memoryStream);
        //        var base64String = Convert.ToBase64String(memoryStream.ToArray());

        //        // Get the search embedding from API
        //        var searchEmbedding = await GetImageEmbedding(base64String);
        //        var searchEmbeddingArray = ParseEmbedding(searchEmbedding);

        //        // Calculate similarities and get top 5 matches
        //        var matches = new List<(int Index, double Similarity, FoundChild Child)>();

        //        for (int i = 0; i < embeddings.Count; i++)
        //        {
        //            var similarity = CalculateSimilarity(searchEmbeddingArray, embeddings[i]);
        //            matches.Add((i, similarity, foundedChildren[i]));
        //        }

        //        var topMatches = matches
        //            .OrderByDescending(x => x.Similarity)
        //            .Take(5)
        //            .ToList();

        //        // Prepare view model
        //        var viewModel = new SearchResultViewModel
        //        {
        //            InputEmbedding = searchEmbedding,
        //            Matches = topMatches.Select(x => new MatchResult
        //            {
        //                Child = x.Child,
        //                SimilarityScore = x.Similarity,
        //                Rank = topMatches.IndexOf(x) + 1
        //            }).ToList()
        //        };

        //        return View("SearchResults", viewModel);
        //    }
        //    catch (Exception ex)
        //    {

        //        return Redirect("/Error/E500");
        //    }
        //}

        //// Helper methods
        //private async Task<string> GetImageEmbedding(string base64Image)
        //{
        //    using var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };

        //    var payload = new { image = base64Image };
        //    var jsonContent = new StringContent(
        //        System.Text.Json.JsonSerializer.Serialize(payload),
        //        Encoding.UTF8,
        //        "application/json");

        //    var response = await httpClient.PostAsync(
        //        "https://ozeniny-fastapi-deployment.hf.space/report",
        //        jsonContent);

        //    response.EnsureSuccessStatusCode();

        //    var responseContent = await response.Content.ReadAsStringAsync();
        //    var result = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);

        //    return result?["image_embedding"];
        //}

        //private float[] ParseEmbedding(string embeddingStr)
        //{
        //    // Parse string like "[-0.011, 0.023]" to float array
        //    return embeddingStr.Trim('[', ']')
        //        .Split(',')
        //        .Select(x => float.Parse(x.Trim(), CultureInfo.InvariantCulture))
        //        .ToArray();
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Result(LostChild model, IFormFile File)
        {
            // Validate file
            if (File != null && !string.IsNullOrEmpty(File.FileName))
            {
                var extension = Path.GetExtension(File.FileName).ToLowerInvariant();
                if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
                {
                    ModelState.AddModelError("LostChildrenImage", "Only PNG, JPG, and JPEG images are allowed");
                }
                else
                {
                    ModelState.Remove("LostChildrenImage");
                }
            }

            if (!ModelState.IsValid)
            {
                return View("SearchForm", model);
            }

            try
            {
                // Get all embeddings from database
                var foundedChildren = _oClsFoundedChildren.GetAllSpaicific(model.Gender,model.DateOfBirth.Year);
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
                return View("Result", vm);
            }
            catch (Exception ex)
            {
                return Redirect("/Error/E500");
            }
        }
        // Define classes to match the JSON structure
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

       

        public async Task<IActionResult> FinalResult()
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

           var isDone = _oClslostChildren.Save(vmUserSearchResult.lostChild, user.Id, vmUserSearchResult.lstSearchResultChildren);
            if (isDone)
            {
                return Redirect("/Home/SuccessPage");
            }
            else
            {
                return Redirect("/Error/505");
            }
        }

        public IActionResult ChildDetails(int id)
        {
            return View(_oClsFoundedChildren.GetFoundChild(id)??new FoundChild());
        }

        
    }
    
    
}
