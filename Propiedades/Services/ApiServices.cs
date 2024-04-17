using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Propiedades.Interfaces;
using Propiedades.Models;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using static Propiedades.Models.DetallesPropiedad;

namespace Propiedades.Services
{
    public class ApiServices : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApiServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<vmPropiedadResponse> ObtenerTodasLasPropiedades(int? pageNumber, string orderBy, string searchTerm,
                                                                          int? pageSize, string orderDirection, int? idType, int? Sector)
        {
            string apiUrl = _configuration["AppSettings:ApiUrl"]; // dejo la api en el appsettings parametrizado
            string Subscription = _configuration["AppSettings:Subscription-Key"]; // dejo la key en el appsettings parametrizado
            try
            {
                _httpClient.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
                _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Subscription);

                var qParams = new Dictionary<string, string>
                {
                    {"PageNumber", pageNumber?.ToString()},
                    {"OrderBy", orderBy},
                    {"SearchTerm", searchTerm},
                    {"PageSize", pageSize?.ToString()},
                    {"OrderDirection", orderDirection},
                };
                string endpoints = CreaEndpoint(apiUrl, qParams); // genero metodo para formar las cadenas endpoint para la api, se puede utilizar para todas
                
                var myHeaderObject = new BodyTodasPropiedades
                {
                    idType = idType == -1 ? null : idType,
                    idBorough = Sector == 0 ? null : Sector,
                }; // aca envio los body para filtrar en los select

                string jsonBody = JsonConvert.SerializeObject(myHeaderObject);

                HttpResponseMessage response;

                using (var content = new StringContent(jsonBody, Encoding.UTF8, "application/json"))
                {
                    response = await _httpClient.PostAsync(endpoints, content);
                }
                response.EnsureSuccessStatusCode(); // valido codigo de error para caer el try en caso de != 200
                string jsonResponse = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                var properties = apiResponse.Items;

                var propiedadResponse = new vmPropiedadResponse
                {
                    Response = new ApiResponse
                    {
                        Items = properties,
                        TotalPages = apiResponse.TotalPages,
                        TotalRecords = apiResponse.TotalRecords,
                        PageNumber = apiResponse.PageNumber,
                        PageSize = apiResponse.PageSize,
                        HttpCode = apiResponse.HttpCode
                    }
                }; // aca transformo el modelo a mi viewModel vmPropiedadResponse, asi puedo trabajar comodo en la vista.
                return propiedadResponse;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }
        // metodo para formatear los endpoints
        public static string CreaEndpoint(string baseEndpoint, Dictionary<string, string> qParams)
        {
            var endpointBuilder = new StringBuilder(baseEndpoint);

            if (qParams != null && qParams.Count > 0)
            {
                var queryString = new StringBuilder();

                foreach (var kvp in qParams) // voy recorriendo los parametros enviados y voy agregandolos, asi puedo dejar fuera los opcionales y agregar solo lo necesario
                {
                    if (!string.IsNullOrEmpty(kvp.Value))
                    {
                        queryString.Append($"{kvp.Key}={kvp.Value}&");
                    }
                }

                if (queryString.Length > 0)
                {
                    queryString.Insert(0, "?"); // inserto el ? para el inicio de los parametros
                    queryString.Length--; // quito el ultimo & en caso de tener
                    endpointBuilder.Append(queryString); // formateo la cadena
                }
            }

            return endpointBuilder.ToString();
        }

        public async Task<PropertyDetalle> ObtenerDetalles(int? PropiedadId)
        {
            string apiUrl = _configuration["AppSettings:ApiUrl"] + "/" + PropiedadId;
            string Subscription = _configuration["AppSettings:Subscription-Key"];
            try
            {
                _httpClient.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
                _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Subscription);
                HttpResponseMessage response;
                using (var content = new StringContent("{}", Encoding.UTF8, "application/json"))
                {
                    response = await _httpClient.GetAsync(apiUrl);
                }
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<PropertyDetalle>(jsonResponse);
                return apiResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<TipoPropiedades>> GetTipoPropiedad()
        {
            string apiUrl = _configuration["AppSettings:ApiUrlTipoPropiedades"];
            string Subscription = _configuration["AppSettings:Subscription-Key"];
            try
            {
                _httpClient.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
                _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Subscription);
                HttpResponseMessage response;
                using (var content = new StringContent("{}", Encoding.UTF8, "application/json"))
                {
                    response = await _httpClient.GetAsync(apiUrl);
                }
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var TipoPropiedades = JsonConvert.DeserializeObject<List<TipoPropiedades>>(jsonResponse);
                return TipoPropiedades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Regiones>> GetRegiones()
        {
            string apiUrl = _configuration["AppSettings:ApiUrlRegiones"];
            string Subscription = _configuration["AppSettings:Subscription-Key"];
            try
            {
                _httpClient.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
                _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Subscription);
                HttpResponseMessage response;
                using (var content = new StringContent("{}", Encoding.UTF8, "application/json"))
                {
                    response = await _httpClient.GetAsync(apiUrl);
                }
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var Regiones = JsonConvert.DeserializeObject<List<Regiones>>(jsonResponse);
                return Regiones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Boroughs>> GetBoroughs(int RegionId)
        {
            string apiUrl = _configuration["AppSettings:ApiUrlDistritos"];
            string Subscription = _configuration["AppSettings:Subscription-Key"];
            try
            {
                _httpClient.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
                _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Subscription);

                var qParams = new Dictionary<string, string>
                {
                    {"idRegion", RegionId.ToString()},
                };
                string endpoints = CreaEndpoint(apiUrl, qParams);

                HttpResponseMessage response;
                using (var content = new StringContent("{}", Encoding.UTF8, "application/json"))
                {
                    response = await _httpClient.GetAsync(endpoints);
                }
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var Boroughs = JsonConvert.DeserializeObject<List<Boroughs>>(jsonResponse);
                return Boroughs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}