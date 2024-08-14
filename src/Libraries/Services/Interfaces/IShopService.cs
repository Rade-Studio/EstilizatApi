using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DbEntities;
using Models.DTOs.Shop;
using Models.DTOs.ShopService;
using Models.ResponseModels;

namespace Services.Interfaces;

public interface IShopService
{
   Task<BaseResponse<string>> RegisterShop(RegisterShop shop);
   Task<BaseResponse<string>> UpdateShop(Guid shopId, UpdateShop shop);
   Task<BaseResponse<Shop>> GetShopById(Guid id);
   Task<BaseResponse<Shop>> GetShopByUserId(int userId);
   Task<BaseResponse<Shop>> GetShopByUserAuthenticated();
   Task<BaseResponse<List<Shop>>> GetShopsByCategory(string category);
   Task<BaseResponse<List<Shop>>> GetAllShops();
   Task<BaseResponse<string>> DeleteShop(Guid shopId);
   Task<BaseResponse<string>> AddShopService(Guid shopId, RegisterShopService service);
   Task<BaseResponse<string>> UpdateShopService(Guid shopId, UpdateShopService service);
   Task<BaseResponse<string>> RemoveShopService(Guid shopId, Guid serviceId);
   Task<BaseResponse<string>> AddEmployee(Guid shopId, RegisterEmployee employeeId);
   Task<BaseResponse<string>> RemoveEmployee(Guid shopId, Guid employeeId);
   Task<BaseResponse<string>> UpdateShopSettings(Guid shopId, UpdateShopSettings settings);
   Task<BaseResponse<string>> UpdateSocialMediaLinks(Guid shopId, UpdateSocialMediaLinks links);
   Task<BaseResponse<string>> UpdateShopImage(Guid shopId, UpdateShopImage image); 
   Task<BaseResponse<string>> UpdateOpeningHoursGuid(Guid shopId, UpdateOpeningHours hours);
   Task<BaseResponse<string>> UpdateGallery(Guid shopId, UpdateGallery gallery);
   
}