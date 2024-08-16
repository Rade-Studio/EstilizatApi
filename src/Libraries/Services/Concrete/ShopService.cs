using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repos;
using Models.DbEntities;
using Models.DbEntities.JsonEntities;
using Models.DTOs.Shop;
using Models.Enums;
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

    public string RegisterShop(RegisterShop registerShop)
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

        var shopCreated = _shopRepository.Insert(shop);

        return shopCreated.Id.ToString();
    }

    public string UpdateShop(Guid shopId, UpdateShop updateShop)
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

        return "Shop updated successfully";
    }

    public Shop GetShopById(Guid id)
    {
        var shop = _shopRepository.Find(x => x.Id == id && x.OwnerId == _authenticatedUserService.UserId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        return shop;
    }

    public Shop GetShopByUserId(int userId)
    {
        Shop shop = _shopRepository.Find(x => x.OwnerId == userId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        return shop;
    }

    public Shop GetShopByUserAuthenticated()
    {
        Shop shop = _shopRepository.Find(x => x.OwnerId == _authenticatedUserService.UserId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        return shop;
    }

    public IReadOnlyList<Shop> GetShopsByCategory(string category)
    {
        var shops = _shopRepository.FindAll(x =>
            x.ShopServices.First(y => y.Name == category) != null);

        return shops;
    }

    public IReadOnlyList<Shop> GetAllShops()
    {
        List<Shop> shops = _shopRepository.GetAll();

        return shops;
    }

    public string DeleteShop(Guid shopId)
    {
        Shop shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        _shopRepository.Delete(shop);

        return "Shop deleted successfully";
    }

    public string AddShopService(Guid shopId, AddShopServiceToEmployee serviceToEmployee)
    {
        throw new NotImplementedException();
    }

    public string UpdateShopService(Guid shopId, AddShopServiceToEmployee serviceToEmployee)
    {
        throw new NotImplementedException();
    }

    public string RemoveShopService(Guid shopId, Guid serviceId)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        // Todo: Add shopServiceRepository
        // var service = _shopServiceRepository.Find(x => x.Id == serviceId);
        var service = new Models.DbEntities.ShopService();
        if (service == null)
        {
            throw new ApplicationException("Service not found");
        }

        // Verificar si hay empleados asociados al servicio
        if (service.Employees.Any())
        {
            throw new ApplicationException("Service has employees associated with it");
        }

        // Verificar si hay pedidos asociados al servicio
        if (service.Appointments.Any())
        {
            throw new ApplicationException("Service has orders associated with it");
        }

        // Verificar si hay pedidos asociados en la lista de espera al servicio
        if (service.WaitLists.Any())
        {
            throw new ApplicationException("Service has wait lists associated with it");
        }

        shop.ShopServices.Remove(service);

        _shopRepository.Update(shop);

        // Eliminar el servicio
        // Todo: Add shopServiceRepository
        // _shopServiceRepository.Delete(service);

        return "Service removed successfully";
    }

    public string AddEmployee(Guid shopId, RegisterEmployee registerEmployee)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        var employee = new Employee()
        {
            Name = registerEmployee.Name,
            PhoneNumber = registerEmployee.Phone,
            Status = EmployeeStatus.Active,
            Services = new List<EmployeeSkill>()
        };

        // Verificar si hay servicios para agregar
        // Todo: Add shopServiceRepository
        // if (registerEmployee.Services != null && registerEmployee.Services.Any())
        // {
        //     var serviceIds = registerEmployee.Services.Select(s => s.Id).ToList();
        //     var servicesToAdd = await _shopServiceRepository.GetServicesByIdsAsync(serviceIds);
        //
        //     // Añadir los servicios encontrados, ignorando los que no existen
        //     foreach (var serviceToAdd in servicesToAdd)
        //     {
        //         employee.Services.Add(new EmployeeSkill
        //         {
        //             ShopServiceId = serviceToAdd.Id,
        //             Service = serviceToAdd
        //         });
        //     }
        // }

        shop.Employees.Add(employee);

        _shopRepository.Update(shop);

        return "Employee added successfully";
    }

    public string UpdateEmployee(Guid shopId, UpdateEmployee updateEmployee)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        var employee = shop.Employees.First(x => x.Id == updateEmployee.EmployeeId);
        if (employee == null)
        {
            throw new ApplicationException("Employee not found");
        }

        employee.Name = updateEmployee.Name;
        employee.PhoneNumber = updateEmployee.PhoneNumber;
        employee.Status = updateEmployee.Status;

        // Verificar si hay servicios para agregar o actualizar
        // Todo: Add shopServiceRepository
        // if (updateEmployee.Services != null && updateEmployee.Services.Any())
        // {
        //     var serviceIds = updateEmployee.Services.Select(s => s.Id).ToList();
        //     var servicesToAdd = _shopServiceRepository.GetServicesByIdsAsync(serviceIds);
        //     var servicesToRemove = employee.Services
        //         .Where(s => serviceIds.All(id => id != s.ShopServiceId)).ToList();
        //
        //     // Añadir los servicios encontrados, ignorando los que no existen
        //     foreach (var serviceToAdd in servicesToAdd)
        //     {
        //         // Verificar si el servicio ya existe
        //         var serviceExists = employee.Services.Any(s => s.ShopServiceId == serviceToAdd.Id);
        //         if (!serviceExists)
        //         {
        //             employee.Services.Add(new EmployeeSkill
        //             {
        //                 ShopServiceId = serviceToAdd.Id,
        //                 Service = serviceToAdd
        //             });
        //         }
        //     }
        //
        //     foreach (var serviceToRemove in servicesToRemove)
        //     {
        //         employee.Services.Remove(serviceToRemove);
        //     }
        // }


        _shopRepository.Update(shop);

        return "Employee updated successfully";
    }


    public string RemoveEmployee(Guid shopId, Guid employeeId)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        var employee = _employeeRepository.Find(x => x.Id == employeeId);
        if (employee == null)
        {
            throw new ApplicationException("Employee not found");
        }

        shop.Employees.Remove(employee);

        _shopRepository.Update(shop);

        return "Employee removed successfully";
    }

    public string UpdateShopSettings(Guid shopId, UpdateShopSettings updateSettings)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        if (shop.ShopSettings.Any(x => x.Key == updateSettings.Key))
        {
            var setting = shop.ShopSettings.First(x => x.Key == updateSettings.Key);
            setting.Value = updateSettings.Value;
        }
        else
        {
            var settingToAdd = new ShopSetting()
            {
                ShopId = shopId,
                Key = updateSettings.Key,
                Value = updateSettings.Value
            };

            shop.ShopSettings.Add(settingToAdd);
        }

        _shopRepository.Update(shop);

        return $"{updateSettings.Key} updated successfully";
    }

    public string UpdateSocialMediaLinks(Guid shopId, UpdateSocialMediaLinks links)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        var socialMedia = new SocialMedia(links.Name, links.Url);
        shop.SocialMedia.Add(socialMedia);

        _shopRepository.Update(shop);

        return $"{links.Name} updated successfully";
    }

    public string UpdateShopImage(Guid shopId, UpdateShopImage image)
    {
        throw new NotImplementedException();
    }

    public Shop UpdateOpeningHoursGuid(Guid shopId, UpdateOpeningHours updateOpeningHours)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        var openingHours = updateOpeningHours.OpeningHours.Select(openingHour =>
            new OpeningHour(openingHour.Day, openingHour.OpenTime, openingHour.CloseTime)).ToList();

        shop.OpeningHours = openingHours;
        var shopUpdated = _shopRepository.Update(shop);

        return shopUpdated;
    }

    public string UpdateGallery(Guid shopId, UpdateGallery gallery)
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

        return "Gallery updated successfully";
    }

    public string ReplyShopReview(Guid shopId, ReplyShopReview review)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId, x => x.Reviews);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        var reviewToUpdate = shop.Reviews.FirstOrDefault(x => x.Id == review.ReviewId);
        if (reviewToUpdate == null)
        {
            throw new ApplicationException("Review not found");
        }

        reviewToUpdate.Reply = review.Reply;
        reviewToUpdate.LastUpdateUTC = DateTime.Now;

        _shopRepository.Update(shop);

        return "Review updated successfully";
    }

    public IReadOnlyList<ShopReview> GetAllShopReviews(Guid shopId)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId, x => x.Reviews);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }
        
        return (IReadOnlyList<ShopReview>)shop.Reviews;
    }

    public IReadOnlyList<ShopReview> GetShopReviewsByPage(Guid id, int page, int pageSize)
    {
        var shop = _shopRepository.Find(x => x.Id == id, x => x.Reviews);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }
        
        return (IReadOnlyList<ShopReview>)shop.Reviews.Skip((page - 1) * pageSize).Take(pageSize);
    }

    public string AddShopReview(Guid shopId, AddShopReview review)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApplicationException("Shop not found");
        }

        var reviewToAdd = new ShopReview()
        {
            Rating = review.Rating,
            Review = review.Review,
            Visibility = true,
            ShopId = shopId,
            CustomerId = _authenticatedUserService.UserId,
            CreateUTC = DateTime.Now,
            LastUpdateUTC = DateTime.Now
        };

        shop.Reviews.Add(reviewToAdd);

        _shopRepository.Update(shop);

        return reviewToAdd.Id.ToString();
    }
}