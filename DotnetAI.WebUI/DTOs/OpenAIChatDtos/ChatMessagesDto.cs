namespace DotnetAI.WebUI.DTOs.OpenAIChatDtos
{
    public class ChatMessagesDto
    {
        public string Author { get; set; } // "User" veya "Bot"
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
