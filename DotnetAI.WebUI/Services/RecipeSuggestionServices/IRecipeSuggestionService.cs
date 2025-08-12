namespace DotnetAI.WebUI.Services.RecipeSuggestionServices
{
    public interface IRecipeSuggestionService
    {
        Task<string> SuggestRecipeWithAIAsync(string ingredients);
    }
}
