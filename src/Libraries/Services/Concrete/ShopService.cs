using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repos;
using Models.DbEntities;
using Models.DbEntities.JsonEntities;
using Models.DTOs.Shop;
using Models.DTOs.ShopService;
using Models.ResponseModels;
using Services.Interfaces;

namespace Services.Concrete;

public class ShopService : IShopService
{
    private readonly IGenericRepository<Shop> _shopRepository;

    // private readonly IGenericRepository<ShopService> _shopServiceRepository;
    private readonly IGenericRepository<Employee> _employeeRepository;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public ShopService(IGenericRepository<Shop> shopRepository,
        IGenericRepository<Employee> employeeRepository, IAuthenticatedUserService authenticatedUserService)
    {
        _shopRepository = shopRepository;
        // _shopServiceRepository = shopServiceRepository;
        _employeeRepository = employeeRepository;
        _authenticatedUserService = authenticatedUserService;
    }

    public Task<BaseResponse<string>> RegisterShop(RegisterShop registerShop)
    {
        Shop shop = _shopRepository.Find(x => x.Name == registerShop.Name);
        if (shop != null)
        {
            throw new ApplicationException($"Shop already exists with name {registerShop.Name}");
        }

        shop = new Shop
        {
            Name = registerShop.Name,
            OwnerId = _authenticatedUserService.UserId,
            Address = registerShop.Address,
            City = registerShop.City,
            Country = registerShop.Country,
            PostalCode = registerShop.PostalCode,
            Description = registerShop.Description,
            Phone = registerShop.Phone,
            Email = registerShop.Email,
            Website = registerShop.Website,
            ServiceDescription = registerShop.ServiceDescription,
        };

        _shopRepository.Insert(shop);

        return Task.FromResult(new BaseResponse<string>("Shop registered successfully"));
    }

    public Task<BaseResponse<string>> UpdateShop(Guid shopId, UpdateShop updateShop)
    {
        var shopExists = _shopRepository.Find(x => x.Name == updateShop.Name && x.Id != shopId);
        if (shopExists != null)
        {
            throw new ApplicationException($"Shop already exists with name {updateShop.Name}");
        }

        Shop shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        shop.Name = updateShop.Name;
        shop.Address = updateShop.Address;
        shop.City = updateShop.City;
        shop.Country = updateShop.Country;
        shop.PostalCode = updateShop.PostalCode;
        shop.Description = updateShop.Description;
        shop.Phone = updateShop.Phone;
        shop.Email = updateShop.Email;
        shop.Website = updateShop.Website;
        shop.ServiceDescription = updateShop.ServiceDescription;

        _shopRepository.Update(shop);

        return Task.FromResult(new BaseResponse<string>("Shop updated successfully"));
    }

    public Task<BaseResponse<Shop>> GetShopById(Guid id)
    {
        Shop shop = _shopRepository.Find(x => x.Id == id);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        return Task.FromResult(new BaseResponse<Shop>(shop));
    }

    public Task<BaseResponse<Shop>> GetShopByUserId(int userId)
    {
        Shop shop = _shopRepository.Find(x => x.OwnerId == userId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        return Task.FromResult(new BaseResponse<Shop>(shop));
    }

    public Task<BaseResponse<Shop>> GetShopByUserAuthenticated()
    {
        Shop shop = _shopRepository.Find(x => x.OwnerId == _authenticatedUserService.UserId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        return Task.FromResult(new BaseResponse<Shop>(shop));
    }

    public Task<BaseResponse<List<Shop>>> GetShopsByCategory(string category)
    {
        List<Shop> shops = _shopRepository.FindAll(x =>
            x.ShopServices.First(y => y.Name == category) != null);

        return Task.FromResult(new BaseResponse<List<Shop>>(shops));
    }

    public Task<BaseResponse<List<Shop>>> GetAllShops()
    {
        List<Shop> shops = _shopRepository.GetAll();

        return Task.FromResult(new BaseResponse<List<Shop>>(shops));
    }

    public Task<BaseResponse<string>> DeleteShop(Guid shopId)
    {
        Shop shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        _shopRepository.Delete(shop);

        return Task.FromResult(new BaseResponse<string>("Shop deleted successfully"));
    }

    public Task<BaseResponse<string>> AddShopService(Guid shopId, RegisterShopService service)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<string>> UpdateShopService(Guid shopId, UpdateShopService service)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<string>> RemoveShopService(Guid shopId, Guid serviceId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<string>> AddEmployee(Guid shopId, RegisterEmployee employeeId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<string>> RemoveEmployee(Guid shopId, Guid employeeId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<string>> UpdateShopSettings(Guid shopId, UpdateShopSettings settings)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        ShopSetting setting = new ShopSetting()
        {
            ShopId = shopId,
            Key = settings.Key,
            Value = settings.Value
        };

        shop.ShopSettings.Add(setting);

        _shopRepository.Update(shop);

        return Task.FromResult(new BaseResponse<string>($"{settings.Key} updated successfully"));
    }

    public Task<BaseResponse<string>> UpdateSocialMediaLinks(Guid shopId, UpdateSocialMediaLinks links)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        var socialMedia = new SocialMedia(links.Name, links.Url);
        shop.SocialMedia.Add(socialMedia);

        _shopRepository.Update(shop);

        return Task.FromResult(new BaseResponse<string>($"{links.Name} updated successfully"));
    }

    public Task<BaseResponse<string>> UpdateShopImage(Guid shopId, UpdateShopImage image)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<string>> UpdateOpeningHoursGuid(Guid shopId, UpdateOpeningHours updateOpeningHours)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        var openingHours = updateOpeningHours.openingHours.Select(openingHour =>
            new OpeningHour(openingHour.Day, openingHour.OpenTime, openingHour.CloseTime)).ToList();

        shop.OpeningHours = openingHours;
        _shopRepository.Update(shop);

        return Task.FromResult(new BaseResponse<string>("Opening hours updated successfully"));
    }

    public Task<BaseResponse<string>> UpdateGallery(Guid shopId, UpdateGallery gallery)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }
        
        var galleryImages = gallery.Images.Select(image =>
            new GalleryShop(image.Url, image.Description)).ToList();
        
        shop.Gallery = galleryImages;
        _shopRepository.Update(shop);
        
        return Task.FromResult(new BaseResponse<string>("Gallery updated successfully"));
    }
}