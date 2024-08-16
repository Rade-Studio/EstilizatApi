using System.Data.Common;
using System.Net.Http.Json;
using Data.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.ResponseModels;
using Newtonsoft.Json;
using UnitTests.core.Enums;
using WebApi.Helpers;

namespace UnitTests.core;

public class ApiFactory(TypeControllerTesting controller) : WebApplicationFactory<IApiAssemblyMarker>
{
    private readonly string _controller = controller.ToString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .UseEnvironment("Development")
            .ConfigureTestServices(
            services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)
                );
                
                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("ApplicationDb");
                });
            });
    }

    public HttpClient GetClient()
    {
        var httpClient = CreateClient();
        httpClient.BaseAddress = new Uri($"http://localhost/api/{_controller}/");

        return httpClient;
    }

    public HttpClient GetClientWithAuthenticated(string? email = null, string? password = null)
    {
        var httpClient = CreateClient();

        var response = CreateClient().PostAsJsonAsync("/api/Account/authenticate", new
        {
            email = email ?? "prueba@prueba.com",
            password = password ?? "P@ssw0rd"
        });

        var result = response.Result.Content.ReadAsStringAsync().Result;
        var token = JsonConvert.DeserializeObject<BaseResponse<AuthenticationResponse>>(result)?.Data.JWToken;

        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        httpClient.BaseAddress = new Uri($"http://localhost/api/{_controller}/");

        return httpClient;
    }
}