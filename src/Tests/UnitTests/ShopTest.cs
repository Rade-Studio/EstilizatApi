using System.Net;
using System.Net.Http.Json;
using Models.DTOs.Shop;
using UnitTests.core.Enums;

namespace UnitTests;

public class ShopTest() : BaseTest(TypeControllerTesting.Shop)
{
    [Fact]
    public async void Should_return_200_ok_when_fetch_all_shops()
    {
        var response = await HttpClient.GetAsync("allshops");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Should_return_zero_shops_when_fetch_all_shops()
    {
        var response = await HttpClient.GetAsync("allshops");
        var shops = GetResponse<IReadOnlyList<ShopDto>>(response);

        Assert.NotNull(shops);
        Assert.Empty(shops.Data);
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

    [Fact]
    public async void Should_return_ok_when_add_shop_with_social_media()
    {
        var shopId = await AddShop(HttpClient);
        
        var shopData = new
        {
            SocialMedia = new[]
            {
                new { Name = "Facebook", Url = "https://facebook.com" },
                new { Name = "Instagram", Url = "https://instagram.com" }
            }
        };

        var response = await HttpClient.PostAsJsonAsync($"{shopId}/updatesocialmedia", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Should_return_ok_when_add_opening_hours_to_shop()
    {
        var shopId = await AddShop(HttpClient);
        
        var shopData = new
        {
            OpeningHours = new[]
            {
                new { Day = "Monday", Open = "08:00", Close = "18:00" },
                new { Day = "Tuesday", Open = "08:00", Close = "18:00" }
            }
        };

        var response = await HttpClient.PostAsJsonAsync($"{shopId}/updateopeninghours", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

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

    [Fact]
    public async void Should_return_ok_when_add_settings_to_shop()
    {
        var shopId = await AddShop(HttpClient);
        
        var shopData = new
        {
            Id = "",
            Key = "Setting 1",
            Value = "Value 1"
        };

        var response = await HttpClient.PostAsJsonAsync($"{shopId}/updatesetting", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Should_return_ok_when_add_employee_to_shop()
    {
        var shopId = await AddShop(HttpClient);
        
        var shopData = new
        {
            Id = "",
            Name = "Employee 1",
            PhoneNumber = "1234567890"
        };

        var response = await HttpClient.PostAsJsonAsync($"{shopId}/updateemployee", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Should_return_ok_when_add_gallery_to_shop()
    {
        var shopId = await AddShop(HttpClient);
        
        var shopData = new
        {
            Gallery = new[]
            {
                new { Image = "https://image1.com" },
                new { Image = "https://image2.com" }
            }
        };

        var response = await HttpClient.PostAsJsonAsync($"{shopId}/updategallery", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Should_return_ok_when_add_review_to_shop()
    {
        var shopId = await AddShop(HttpClient);
        
        var shopData = new
        {
            Rating = 5,
            Review = "Good"
        };

        var response = await HttpClient.PostAsJsonAsync($"{shopId}/addreview", shopData);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Should_return_ok_when_set_review()
    {
        var shopId = await AddShop(HttpClient);
        
        var shopData = new
        {
            Rating = 5,
            Review = "Good"
        };

        var responseAdd = await HttpClient.PostAsJsonAsync($"{shopId}/addreview", shopData);

        var review = GetResponse<string>(responseAdd);

        var reviewData = new
        {
            Id = review.Data,
            Reply = "Thank you for your review",
            Visibility = true
        };

        var responseUpdate = await HttpClient.PostAsJsonAsync($"{shopId}/setreview", reviewData);

        Assert.Equal(HttpStatusCode.OK, responseUpdate.StatusCode);
    }

    private static async Task<string> AddShop(HttpClient httpClient)
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

        var response = await httpClient.PostAsJsonAsync("addshop", shopData);

        var data = GetResponse<string>(response);

        if (data == null) 
            throw new Exception("Error adding shop");
        
        return data.Data;
    }
}