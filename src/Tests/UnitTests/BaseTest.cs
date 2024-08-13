using Models.ResponseModels;
using Newtonsoft.Json;
using UnitTests.core;
using UnitTests.core.Enums;

namespace UnitTests;

public class BaseTest
{
    protected readonly HttpClient HttpClient;
    
    public BaseTest(TypeControllerTesting typeControllerTesting)
    {
        var api = new ApiFactory(typeControllerTesting);
        HttpClient = api.GetClientWithAuthenticated();
    }

    protected static BaseResponse<T>? GetResponse<T>(HttpResponseMessage response)
    {
        var content = response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<BaseResponse<T>>(content);
    }
    
}