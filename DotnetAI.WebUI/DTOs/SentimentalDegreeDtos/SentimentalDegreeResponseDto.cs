namespace DotnetAI.WebUI.DTOs.SentimentalDegreeDtos
{
    public class SentimentalDegreeResponseDto
    {
        public int joy { get; set; }
        public int sadness { get; set; }
        public int anger { get; set; }
        public int fear { get; set; }
        public int surprised { get; set; }
        public int neutral { get; set; }
        public string hasHateSpeech { get; set; }

    }
}
