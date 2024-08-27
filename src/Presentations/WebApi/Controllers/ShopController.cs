using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DbEntities;
using Models.DTOs.Shop;
using Models.ResponseModels;
using Models.ResponseModels.Shop;
using Services.Interfaces;
using WebApi.Attributes;
using WebApi.Helpers;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ShopController : ControllerBase
{
    private readonly IShopService _shopService;
    private readonly IMapper _mapper;

    public ShopController(IShopService shopService, ICacheManager cacheManager, IMapper mapper)
    {
        _shopService = shopService;
        _mapper = mapper;
    }

    [Cached(10)]
    [HttpGet("allshops")]
    [Authorize(Policy = "OnlyAdmins")]
    public IActionResult GetAllShops()
    {
        var response = _shopService.GetAllShops();
        var data = _mapper.Map<IReadOnlyList<ShopDto>>(response);

        return Ok(new BaseResponse<IReadOnlyList<ShopDto>>(data, "Shops retrieved successfully"));
    }

    [HttpPost("addshop")]
    [Authorize(Policy = "ShopOwner")]
    public IActionResult AddNewShop([FromBody] RegisterShop registerShop)
    {
        var response = _shopService.RegisterShop(registerShop);
        return Ok(new BaseResponse<string>(response, "Shop registered successfully"));
    }

    [HttpPut("updateshop/{id:guid}")]
    [Authorize(Policy = "ShopOwner")]
    public IActionResult UpdateShop([FromBody] UpdateShop updateShop, Guid id)
    {
        var response = _shopService.UpdateShop(id, updateShop);
        return Ok(new BaseResponse<string>(response));
    }

    [HttpGet("getshop/{id:guid}")]
    [Authorize(Policy = "ShopOwner")]
    public IActionResult GetShopById(Guid id)
    {
        var response = _shopService.GetShopById(id);
        return Ok(new BaseResponse<ShopDto>(_mapper.Map<ShopDto>(response)));
    }

    [HttpGet("getshop/{userId:int}")]
    [Authorize(Policy = "OnlyAdmins")]
    public IActionResult GetShopByUserId(int userId)
    {
        var response = _shopService.GetShopByUserId(userId);
        return Ok(new BaseResponse<ShopDto>(_mapper.Map<ShopDto>(response)));
    }

    [HttpPut("{id:guid}/updatesocialmedia")]
    [Authorize(Policy = "ShopOwner")]
    public IActionResult UpdateSocialMediaLinks([FromBody] UpdateSocialMediaLinks links, Guid id)
    {
        var response = _shopService.UpdateSocialMediaLinks(id, links);
        return Ok(new BaseResponse<string>(response));
    }

    [HttpPut("{id:guid}/updateopeninghours")]
    [Authorize(Policy = "ShopOwner")]
    public IActionResult UpdateOpeningHours([FromBody] UpdateOpeningHours hours, Guid id)
    {
        var response = _shopService.UpdateOpeningHoursGuid(id, hours);
        var data = _mapper.Map<ShopDto>(response);

        return Ok(new BaseResponse<ShopDto>(data, "Opening hours updated successfully"));
    }

    [HttpPut("{id:guid}/updategallery")]
    [Authorize(Policy = "ShopOwner")]
    public async Task<IActionResult> UpdateGallery([FromForm] List<IFormFile> files, Guid id)
    {
        var permittedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
        var permittedMimeTypes = new List<string> { "image/jpeg", "image/png" };
        
        if (files == null || files.Count == 0)
            return BadRequest("No files provided");

        var gallery = new UpdateGallery()
        {
            Images = []
        };
        foreach (var file in files)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!permittedExtensions.Contains(extension))
            {
                return BadRequest($"File extension '{extension}' is not allowed. Only images are permitted.");
            }

            if (!permittedMimeTypes.Contains(file.ContentType))
            {
                return BadRequest($"File type '{file.ContentType}' is not allowed. Only images are permitted.");
            }

            var fileName = $"gallery-{Guid.NewGuid()}{extension}";

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                // TODO: Save file to storage S3 or Azure
                var imageUrl = $"http://localhost:5001/{fileName}";

                gallery.Images.Add(new UpdateGallery.GalleryImageItem
                {
                    Url = imageUrl,
                    Description = fileName
                });
            }
        }

        var response = _shopService.UpdateGallery(id, gallery);
        return Ok(new BaseResponse<string>(response));
    }

    [HttpPut("{id:guid}/updatesettings")]
    [Authorize(Policy = "ShopOwner")]
    public IActionResult UpdateShopSettings([FromBody] UpdateShopSettings settings, Guid id)
    {
        var response = _shopService.UpdateShopSettings(id, settings);
        return Ok(new BaseResponse<string>(response));
    }

    [HttpPost("{id:guid}/addemployee")]
    [Authorize(Policy = "ShopOwner")]
    public IActionResult AddEmployee([FromBody] RegisterEmployee registerEmployee, Guid id)
    {
        var response = _shopService.AddEmployee(id, registerEmployee);
        return Ok(new BaseResponse<string>(response));
    }

    [HttpPut("{id:guid}/updateemployee")]
    [Authorize(Policy = "ShopOwner")]
    public IActionResult UpdateEmployee([FromBody] UpdateEmployee updateEmployee, Guid id)
    {
        var response = _shopService.UpdateEmployee(id, updateEmployee);
        return Ok(new BaseResponse<string>(response));
    }

    [HttpDelete("{id:guid}/removeemployee")]
    [Authorize(Policy = "ShopOwner")]
    public IActionResult RemoveEmployee([FromBody] Guid employeeId, Guid id)
    {
        var response = _shopService.RemoveEmployee(id, employeeId);
        return Ok(new BaseResponse<string>(response));
    }

    [HttpPost("{id:guid}/addreview")]
    [Authorize(Roles = "Basic")]
    public IActionResult AddReview([FromBody] AddShopReview review, Guid id)
    {
        var response = _shopService.AddShopReview(id, review);
        return Ok(new BaseResponse<string>(response, "Review added successfully"));
    }

    [HttpPut("{id:guid}/replyreview")]
    [Authorize(Roles = "Basic")]
    public IActionResult UpdateReview([FromBody] ReplyShopReview review, Guid id)
    {
        var response = _shopService.ReplyShopReview(id, review);
        return Ok(new BaseResponse<string>(response));
    }

    [HttpGet("{id:guid}/allreviews")]
    [Authorize(Roles = "Basic")]
    public IActionResult GetAllReviews(Guid id)
    {
        var response = _shopService.GetAllShopReviews(id);
        var data = _mapper.Map<IReadOnlyList<ShopReviewDto>>(response);

        return Ok(new BaseResponse<IReadOnlyList<ShopReviewDto>>(data, "Reviews retrieved successfully"));
    }

    [HttpPut("{id:guid}/updateprofilepicture")]
    [Authorize(Policy = "ShopOwner")]
    public async Task<IActionResult> UpdateProfilePicture([FromForm] IFormFile image, Guid id)
    {
        var permittedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
        var permittedMimeTypes = new List<string> { "image/jpeg", "image/png" };
        
        if (image == null)
            return BadRequest("No files provided");

        var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
        if (!permittedExtensions.Contains(extension))
        {
            return BadRequest($"File extension '{extension}' is not allowed. Only images are permitted.");
        }

        if (!permittedMimeTypes.Contains(image.ContentType))
        {
            return BadRequest($"File type '{image.ContentType}' is not allowed. Only images are permitted.");
        }
        
        extension = Path.GetExtension(image.FileName);
        var fileName = $"profile-picture-{Guid.NewGuid()}{extension}";

        using (var memoryStream = new MemoryStream())
        {
            await image.CopyToAsync(memoryStream);

            // TODO: Save file to storage S3 or Azure
            var imageUrl = $"http://localhost:5001/{fileName}";

            var response = _shopService.UpdateShopImage(id, new UpdateShopImage()
            {
                ImageUrl = imageUrl
            });
            return Ok(new BaseResponse<string>(response));
        }
    }
}