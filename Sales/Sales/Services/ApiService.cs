namespace Sales.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using Sales.Common.Models;
    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your Internet settings",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "You don´t have Internet connection",
                };
            }

            return new Response
            {
                IsSuccess = true,
            };
        }
        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller) // Dirección Base
        {
            try
            {
                var client = new HttpClient();      //Carga la dirección
                client.BaseAddress = new Uri(urlBase);      //carga la URL Base
                var url = $"{prefix}{controller}";      //Concatena            
                var response = await client.GetAsync(url);      //Asigna el getAsync, lee la respuesta
                string answer = await response.Content.ReadAsStringAsync(); //Lee la respuesta
                if(!response.IsSuccessStatusCode)  //Si se presenan fallos
                {
                    return new Response
                    {
                        IsSuccess = false,  //Asigna False al valor IsSuccess
                        Message = answer,
                    };
                }
                var list = JsonConvert.DeserializeObject<List<T>>(answer); //Convierte la respuesta de Json en una lista
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
