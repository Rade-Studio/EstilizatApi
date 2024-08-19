using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Data.Repos;
using Models.DbEntities;
using Models.DbEntities.JsonEntities;
using Models.DTOs.Shop;
using Models.Enums;
using Models.Exceptions;
using Services.Interfaces;

namespace Services.Concrete;

public class ShopService : IShopService
{
    private readonly IGenericRepository<Shop> _shopRepository;

    private readonly IGenericRepository<Models.DbEntities.ShopService> _shopServiceRepository;
    private readonly IGenericRepository<Employee> _employeeRepository;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public ShopService(IGenericRepository<Shop> shopRepository,
        IGenericRepository<Employee> employeeRepository,
        IAuthenticatedUserService authenticatedUserService,
        IGenericRepository<Models.DbEntities.ShopService> shopServiceRepository)
    {
        _shopRepository = shopRepository;
        _shopServiceRepository = shopServiceRepository;
        _employeeRepository = employeeRepository;
        _authenticatedUserService = authenticatedUserService;
    }

    public string RegisterShop(RegisterShop registerShop)
    {
        Shop shop = _shopRepository.Find(x => x.Name == registerShop.Name);
        if (shop != null)
        {
            throw new ApiException($"Shop already exists with name {registerShop.Name}")
                { StatusCode = (int)HttpStatusCode.Conflict };
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
            throw new ApiException($"Shop already exists with name {updateShop.Name}")
                { StatusCode = (int)HttpStatusCode.Conflict };
        }

        Shop shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
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
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
        }

        return shop;
    }

    public Shop GetShopByUserId(int userId)
    {
        Shop shop = _shopRepository.Find(x => x.OwnerId == userId);
        if (shop == null)
        {
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
        }

        return shop;
    }

    public Shop GetShopByUserAuthenticated()
    {
        Shop shop = _shopRepository.Find(x => x.OwnerId == _authenticatedUserService.UserId);
        if (shop == null)
        {
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
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
            throw new ApiException("Shop not found") { StatusCode = 404 };
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
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
        }

        // Todo: Add shopServiceRepository
        // var service = _shopServiceRepository.Find(x => x.Id == serviceId);
        var service = new Models.DbEntities.ShopService();
        if (service == null)
        {
            throw new ApiException("Service not found") { StatusCode = (int)HttpStatusCode.NotFound };
        }

        // Verificar si hay empleados asociados al servicio
        if (service.Employees.Any())
        {
            throw new ApiException("Service has employees associated with it")
                { StatusCode = (int)HttpStatusCode.BadRequest };
        }

        // Verificar si hay pedidos asociados al servicio
        if (service.Appointments.Any())
        {
            throw new ApiException("Service has orders associated with it")
                { StatusCode = (int)HttpStatusCode.BadRequest };
        }

        // Verificar si hay pedidos asociados en la lista de espera al servicio
        if (service.WaitLists.Any())
        {
            throw new ApiException("Service has wait lists associated with it")
                { StatusCode = (int)HttpStatusCode.BadRequest };
        }

        shop.ShopServices.Remove(service);

        _shopRepository.Update(shop);

        // Eliminar el servicio
        _shopServiceRepository.Delete(service);

        return "Service removed successfully";
    }

    public string AddEmployee(Guid shopId, RegisterEmployee registerEmployee)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
        }

        var employee = new Employee()
        {
            Name = registerEmployee.Name,
            PhoneNumber = registerEmployee.Phone,
            Status = EmployeeStatus.Active,
            Services = new List<EmployeeSkill>()
        };

        // Verificar si hay servicios para agregar
        if (registerEmployee.Services != null && registerEmployee.Services.Any())
        {
            var serviceIds = registerEmployee.Services.Select(s => s.Id).ToList();
            var servicesToAdd = _shopServiceRepository.FindAll(x => serviceIds.Contains(x.Id));

            // Añadir los servicios encontrados, ignorando los que no existen
            foreach (var serviceToAdd in servicesToAdd)
            {
                employee.Services.Add(new EmployeeSkill
                {
                    ShopServiceId = serviceToAdd.Id,
                    Service = serviceToAdd
                });
            }
        }

        shop.Employees.Add(employee);

        _shopRepository.Update(shop);

        return "Employee added successfully";
    }

    public string UpdateEmployee(Guid shopId, UpdateEmployee updateEmployee)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
        }

        var employee = shop.Employees.First(x => x.Id == updateEmployee.EmployeeId);
        if (employee == null)
        {
            throw new ApiException("Employee not found") { StatusCode = (int)HttpStatusCode.NotFound };
        }

        employee.Name = updateEmployee.Name;
        employee.PhoneNumber = updateEmployee.PhoneNumber;
        employee.Status = updateEmployee.Status;

        // Verificar si hay servicios para agregar o actualizar
        if (updateEmployee.Services != null && updateEmployee.Services.Any())
        {
            var serviceIds = updateEmployee.Services.Select(s => s.Id).ToList();
            var servicesToAdd = _shopServiceRepository.FindAll(x => serviceIds.Contains(x.Id));
            var servicesToRemove = employee.Services
                .Where(s => serviceIds.All(id => id != s.ShopServiceId)).ToList();

            // Añadir los servicios encontrados, ignorando los que no existen
            foreach (var serviceToAdd in from serviceToAdd in servicesToAdd
                     let serviceExists = employee.Services.Any(s => s.ShopServiceId == serviceToAdd.Id)
                     where !serviceExists
                     select serviceToAdd)
            {
                employee.Services.Add(new EmployeeSkill
                {
                    ShopServiceId = serviceToAdd.Id,
                    Service = serviceToAdd
                });
            }

            foreach (var serviceToRemove in servicesToRemove)
            {
                employee.Services.Remove(serviceToRemove);
            }
        }


        _shopRepository.Update(shop);

        return "Employee updated successfully";
    }


    public string RemoveEmployee(Guid shopId, Guid employeeId)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
        }

        var employee = _employeeRepository.Find(x => x.Id == employeeId);
        if (employee == null)
        {
            throw new ApiException("Employee not found") { StatusCode = (int)HttpStatusCode.NotFound };
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
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
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
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
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
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
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
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
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
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
        }

        var reviewToUpdate = shop.Reviews.FirstOrDefault(x => x.Id == review.ReviewId);
        if (reviewToUpdate == null)
        {
            throw new ApiException("Review not found") { StatusCode = (int)HttpStatusCode.NotFound };
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
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
        }

        return (IReadOnlyList<ShopReview>)shop.Reviews;
    }

    public IReadOnlyList<ShopReview> GetShopReviewsByPage(Guid id, int page, int pageSize)
    {
        var shop = _shopRepository.Find(x => x.Id == id, x => x.Reviews);
        if (shop == null)
        {
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
        }

        return (IReadOnlyList<ShopReview>)shop.Reviews.Skip((page - 1) * pageSize).Take(pageSize);
    }

    public string AddShopReview(Guid shopId, AddShopReview review)
    {
        var shop = _shopRepository.Find(x => x.Id == shopId);
        if (shop == null)
        {
            throw new ApiException("Shop not found") { StatusCode = (int)HttpStatusCode.NotFound };
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