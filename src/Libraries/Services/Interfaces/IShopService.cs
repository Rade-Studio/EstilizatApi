using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DbEntities;
using Models.DTOs.Shop;
using Models.ResponseModels;

namespace Services.Interfaces;

public interface IShopService
{
   string RegisterShop(RegisterShop shop);
   string UpdateShop(Guid shopId, UpdateShop shop);
   Shop GetShopById(Guid id);
   Shop GetShopByUserId(int userId);
   Shop GetShopByUserAuthenticated();
   IReadOnlyList<Shop> GetShopsByCategory(string category);
   IReadOnlyList<Shop> GetAllShops();
   string DeleteShop(Guid shopId);
   string AddShopService(Guid shopId, AddShopServiceToEmployee serviceToEmployee);
   string UpdateShopService(Guid shopId, AddShopServiceToEmployee serviceToEmployee);
   string RemoveShopService(Guid shopId, Guid serviceId);
   string AddEmployee(Guid shopId, RegisterEmployee registerEmployee);
   string UpdateEmployee(Guid shopId, UpdateEmployee updateEmployee);
   string RemoveEmployee(Guid shopId, Guid employeeId);
   string UpdateShopSettings(Guid shopId, UpdateShopSettings updateSettings);
   string UpdateSocialMediaLinks(Guid shopId, UpdateSocialMediaLinks links);
   string UpdateShopImage(Guid shopId, UpdateShopImage image); 
   Shop UpdateOpeningHoursGuid(Guid shopId, UpdateOpeningHours hours);
   string UpdateGallery(Guid shopId, UpdateGallery gallery);
   string AddShopReview(Guid shopId, AddShopReview review);
   string ReplyShopReview(Guid shopId, ReplyShopReview review);
   IReadOnlyList<ShopReview> GetAllShopReviews(Guid shopId);
   IReadOnlyList<ShopReview> GetShopReviewsByPage(Guid shopId, int page, int pageSize);
}