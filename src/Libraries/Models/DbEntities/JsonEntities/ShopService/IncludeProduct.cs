namespace Models.DbEntities.JsonEntities.ShopService;

public record IncludeProduct(
    string Name,
    string Description,
    string Quantity,
    string Unit,
    string Price,
    string Image);