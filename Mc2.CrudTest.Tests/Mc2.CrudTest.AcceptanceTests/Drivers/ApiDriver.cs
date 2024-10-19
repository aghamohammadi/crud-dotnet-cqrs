using Mc2.CrudTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Web.AcceptanceTests;
using Mc2.CrudTest.Presentation.Shared.Dtos;
using System.Text.Json;

namespace Mc2.CrudTest.AcceptanceTests.Drivers
{
    public class ApiDriver
    {
        private readonly HttpClient _httpClient;

        public ApiDriver()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri($"{ConfigurationHelper.GetBaseUrl()}/api/") };
        }

        public async Task<Guid> CreateCustomer(CustomerDto customer)
        {
            var json = JsonSerializer.Serialize(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("customers", content);

            var responseBody = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw (new Exception(responseBody));
            return Guid.Parse(JsonSerializer.Deserialize<string>(responseBody));
        }

        public async Task<List<CustomerDto>> GetCustomers()
        {
            var response = await _httpClient.GetAsync("customers");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CustomerDto>>(responseString);
        }

        public async Task<CustomerDto> GetCustomer(Guid id)
        {
            var response = await _httpClient.GetAsync($"customers/{id}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CustomerDto>(responseString);
        }

        public async Task<bool> UpdateCustomer(CustomerDto customer)
        {
            var json = JsonSerializer.Serialize(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"customers/{customer.Id}", content);
            var responseBody = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw (new Exception(responseBody));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCustomer(Guid customerId)
        {
            var response = await _httpClient.DeleteAsync($"customers/{customerId}");
            return response.IsSuccessStatusCode;
        }
    }
}
