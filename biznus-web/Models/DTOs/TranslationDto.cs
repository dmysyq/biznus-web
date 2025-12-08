namespace biznus_web.Models.DTOs
{
    public class TranslationDto
    {
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Culture { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string? Scope { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateTranslationRequest
    {
        public string Key { get; set; } = string.Empty;
        public string Culture { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string? Scope { get; set; }
    }

    public class UpdateTranslationRequest
    {
        public string Value { get; set; } = string.Empty;
        public string? Scope { get; set; }
    }
}

