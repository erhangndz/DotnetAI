﻿namespace DotnetAI.WebUI.DTOs.ChatResponseDtos
{
    public class ChatResponseDto
    { 
        public List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        public Message Message { get; set; }
    }

    public class Message
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }
}
