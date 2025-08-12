using DotnetAI.WebUI.Services.RecipeSuggestionServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class OpenAIRecipeSuggestionController(IRecipeSuggestionService recipeSuggestionService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string ingredients)
        {
            var response = await recipeSuggestionService.SuggestRecipeWithAIAsync(ingredients);
            ViewBag.ingredients = ingredients;
            ViewBag.response= response;
            return View();
        }
    }
}
