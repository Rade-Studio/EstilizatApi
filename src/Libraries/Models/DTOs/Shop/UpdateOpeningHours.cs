using System.Collections;
using System.Collections.Generic;

namespace Models.DTOs.Shop;

public class UpdateOpeningHours
{
    public List<OpeningHourItem> OpeningHours { get; set; }
}

public class OpeningHourItem
{
    public string Day { get; set; }
    public string OpenTime { get; set; }
    public string CloseTime { get; set; }
}