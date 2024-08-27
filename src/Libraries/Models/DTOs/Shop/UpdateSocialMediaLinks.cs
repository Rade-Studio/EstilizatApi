using System.Collections.Generic;

namespace Models.DTOs.Shop;

public class UpdateSocialMediaLinks
{
    public List<SocialMediaLink> SocialMediaLinks { get; set; }
}

public class SocialMediaLink
{
    public string Name { get; set; }
    public string Url { get; set; }
}