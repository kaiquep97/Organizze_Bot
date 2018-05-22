using Newtonsoft.Json;
using OrganizzeBot.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrganizzeBot.Services
{
    public class CategoriaService : BaseService,IDisposable
    {
        private HttpClient client;

        public CategoriaService()
        {
            this.client = new HttpClient();

            AdicionaCabecalho(client);
        }

        public async Task<Categoria[]> BuscaCategorias()
        {
            try
            {
                var response = await client.GetAsync("categories");
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Deu erro");
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var categorias = JsonConvert.DeserializeObject<Categoria[]>(json);

                    return categorias;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }



        public void Dispose()
        {
            client.Dispose();
        }
    }
}