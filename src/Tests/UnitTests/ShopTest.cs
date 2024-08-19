﻿using System.Net;
using System.Net.Http.Json;
using Models.DTOs.Shop;
using UnitTests.core.Enums;

namespace UnitTests;

public class ShopTest() : BaseTest(TypeControllerTesting.Shop)
{
    # region Shop Tests

    [Fact]
    public async void Should_return_200_ok_when_fetch_all_shops()
    {
        var response = await HttpClient.GetAsync("allshops");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Should_return_ok_when_add_new_shop()
    {
        var shopData = new
        {
            Name = "Test Shop",
            Description = "Test Shop Description",
            Address = "Test Shop Address",
            Phone = "1234567890",
            Email = "prueba@prueba.com",
            Website = "https://prueba.com",
            City = "Test City",
            Country = "Test Country",
            PostalCode = "123456"
        };

        var response = await HttpClient.PostAsJsonAsync("addshop", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    #endregion

    #region features into Shop

    [Fact]
    public async void Should_return_ok_when_add_shop_with_social_media()
    {
        var shopId = await AddShop(HttpClient);

        var shopData = new UpdateSocialMediaLinks
        {
            Name = "Facebook",
            Url = "https://facebook.com"
        };

        var response = await HttpClient.PutAsJsonAsync($"{shopId}/updatesocialmedia", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Should_return_ok_when_add_opening_hours_to_shop()
    {
        var shopId = await AddShop(HttpClient);

        var shopData = new UpdateOpeningHours()
        {
            OpeningHours =
            [
                new OpeningHourItem { Day = "Monday", OpenTime = "08:00", CloseTime = "18:00" },
                new OpeningHourItem { Day = "Tuesday", OpenTime = "08:00", CloseTime = "18:00" }
            ]
        };

        var response = await HttpClient.PutAsJsonAsync($"{shopId}/updateopeninghours", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Should_return_ok_when_add_gallery_to_shop()
    {
        var shopId = await AddShop(HttpClient);

        var shopData = new UpdateGallery()
        {
            Images = new List<UpdateGallery.GalleryImageItem>()
            {
                new() { Url = "https://image1.com" },
                new() { Url = "https://image2.com" }
            }
        };

        var response = await HttpClient.PutAsJsonAsync($"{shopId}/updategallery", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    #endregion

    # region segment into Shop

    [Fact]
    public async void Should_return_ok_when_add_settings_to_shop()
    {
        var shopId = await AddShop(HttpClient);

        var shopData = new
        {
            Key = "Setting 1",
            Value = "Value 1"
        };

        var response = await HttpClient.PutAsJsonAsync($"{shopId}/updatesettings", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    #endregion

    # region employee features into Shop

    [Fact]
    public async void Should_return_ok_when_add_employee_to_shop_without_services()
    {
        var shopId = await AddShop(HttpClient);

        var shopData = new
        {
            Name = "Employee 1",
            PhoneNumber = "1234567890"
        };

        var response = await HttpClient.PostAsJsonAsync($"{shopId}/addemployee", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    #endregion

    # region service featrues into Shop

    [Fact]
    public async void Should_return_ok_when_add_service_to_shop()
    {
        var shopId = await AddShop(HttpClient);
        var shopData = new
        {
            Name = "Service 1",
            Description = "Service 1 Description",
            Price = 100
        };

        var response = await HttpClient.PostAsJsonAsync($"{shopId}/addservice", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    #endregion

    # region review featrues into Shop

    [Fact]
    public async void Should_return_ok_when_all_reviews_by_shop()
    {
        var shopId = await AddShop(HttpClient);

        var response = await HttpClient.GetAsync($"{shopId}/allreviews");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async void Should_return_ok_when_add_review_to_shop()
    {
        var shopId = await AddShop(HttpClient);

        var shopData = new AddShopReview()
        {
            Rating = 5,
            Review = "Good"
        };

        var response = await HttpClient.PostAsJsonAsync($"{shopId}/addreview", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Should_return_ok_when_reply_review()
    {
        var shopId = await AddShop(HttpClient);

        var shopData = new AddShopReview()
        {
            Rating = 5,
            Review = "Good"
        };

        var responseAdd = await HttpClient.PostAsJsonAsync($"{shopId}/addreview", shopData);

        var review = GetResponse<string>(responseAdd);

        var reviewData = new 
        {
            ReviewId = review?.Data,
            Reply = "Thank you for your review",
        };

        var responseUpdate = await HttpClient.PutAsJsonAsync($"{shopId}/replyreview", reviewData);

        Assert.Equal(HttpStatusCode.OK, responseUpdate.StatusCode);
    }

    #endregion

    # region private methods

    private static async Task<string> AddShop(HttpClient httpClient)
    {
        var shopData = new
        {
            Name = "Test Shop" + Guid.NewGuid(),
            Description = "Test Shop Description",
            Address = "Test Shop Address",
            Phone = "1234567890",
            Email = "prueba@prueba.com",
            Website = "https://prueba.com",
            City = "Test City",
            Country = "Test Country",
            PostalCode = "123456"
        };

        var response = await httpClient.PostAsJsonAsync("addshop", shopData);

        var data = GetResponse<string>(response);

        if (data == null)
            throw new Exception("Error adding shop");

        return data.Data;
    }

    #endregion
}