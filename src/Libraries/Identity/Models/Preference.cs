namespace Identity.Models;

public record Preference
{
    public string Type { get; set; }
    public string Value { get; set; }
}
