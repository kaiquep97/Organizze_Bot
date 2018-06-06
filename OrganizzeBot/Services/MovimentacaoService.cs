using Newtonsoft.Json;
using OrganizzeBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace OrganizzeBot.Services
{
    public class MovimentacaoService : BaseService, IDisposable
    {
        private HttpClient client;

        public MovimentacaoService()
        {
            this.client = new HttpClient();
            AdicionaCabecalho(client);
        }

        public async Task<Movimentacao[]> BuscaMovimentacoes()
        {
            try
            {
                var response = await client.GetAsync("transactions");
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Deu erro");
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var movimentacoes = JsonConvert.DeserializeObject<Movimentacao[]>(json);
                    return movimentacoes;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Movimentacao[]> BuscaMovimentacoes(DateTime startDate,DateTime endDate)
        {
            try
            {
                var response = await client.GetAsync($"transactions?start_date{startDate.ToString("yyyy-MM-dd")}&end_date={endDate.ToString("yyyy-MM-dd")}");
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Deu erro");
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var movimentacoes = JsonConvert.DeserializeObject<Movimentacao[]>(json);
                    return movimentacoes;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> AddMovimentacao(Movimentacao movimentacao)
        {
            try
            {
                var response = await client.PostAsJsonAsync<Movimentacao>("transactions", movimentacao);

                if (!response.IsSuccessStatusCode)
                    return false;
                else{
                    var json = await response.Content.ReadAsStringAsync();
                    var retorno = JsonConvert.DeserializeObject<Movimentacao>(json);

                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Dispose()
        {
            client.Dispose();
        }
    }
}