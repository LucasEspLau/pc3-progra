using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pc3_progra.Integration.Entity;

namespace pc3_progra.Integration
{
    public class ListarUsuarioApiIntegration
    {
        private readonly ILogger<ListarUsuarioApiIntegration> _logger;

        private const string API_URL = "https://reqres.in/api/users/";
        private readonly HttpClient httpClient;
        public ListarUsuarioApiIntegration(ILogger<ListarUsuarioApiIntegration> logger)
        {
            _logger = logger;
            httpClient = new HttpClient();

        }
        public async Task<Usuario> GetUser(int Id)
        {

            string requestUrl =$"{API_URL}{Id}";
            Usuario user = new Usuario();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiRespuesta>();
                    if (apiResponse != null)
                    {
                        user = apiResponse.Data ?? new Usuario();
                    }
                }
            }
            catch(Exception ex){
                _logger.LogDebug($"Error al llamar a la API: {ex.Message}");
            }
            return user;

        }

        class ApiRespuesta
        {
            public Usuario Data { get; set; }
            public Support Support { get; set; }
        }


    }
}