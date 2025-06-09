using InventoryCleanApp.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Text.Json;

namespace InventoryCleanApp.Services;

public class ProductRepo
{
    private readonly AuthService _authService;
    private readonly JsonSerializerOptions _options;
    public ProductRepo(AuthService authService)
    {
        _authService = authService;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<PagingResponse<ProductModel>> GetProductsAsync(int pageNumber, string? searchTerm = null)
    {
        try
        {
            var httpClient = await _authService.GetAuthenticatedHttpclient();

            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = pageNumber.ToString(),
                ["searchTerm"] = searchTerm == null ? "" : searchTerm
            };

            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString("api/Product/GetProducts", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            var pagingResponse = new PagingResponse<ProductModel>
            {
                Items = JsonSerializer.Deserialize<List<ProductModel>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            //var productList = await httpClient.GetFromJsonAsync<List<ProductModel>>("api/Product/GetProducts");
            //return productList?.ToArray() ?? Array.Empty<ProductModel>();

            return pagingResponse;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }
    
}
