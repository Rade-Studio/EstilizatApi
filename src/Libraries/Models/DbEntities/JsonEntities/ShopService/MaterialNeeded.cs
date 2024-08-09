namespace Models.DbEntities.JsonEntities.ShopService;

public record MaterialNeeded(
    string Name,
    string Description,
    string Quantity,
    string Unit);