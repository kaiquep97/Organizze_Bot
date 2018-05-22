using System;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace OrganizzeBot.Services
{
    public abstract class BaseService
    {

        public void AdicionaCabecalho(HttpClient client)
        {
            var username = ConfigurationManager.AppSettings["UserName"].ToString();
            var password = ConfigurationManager.AppSettings["ApiKey"].ToString();
            var description = "Teste de Construção de ChatBot";

            AdicionaUserAgents(client, username, description);
            AdicionaAuthorization(client, username, password);
            AdicionaEndpoint(client);
        }

        private static void AdicionaEndpoint(HttpClient client)
        {
            var endpoint = ConfigurationManager.AppSettings["OrganizzeEndpoint"].ToString();
            client.BaseAddress = new Uri(endpoint);
        }

        private static void AdicionaAuthorization(HttpClient client, string username, string password)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            var authString = Convert.ToBase64String(byteArray);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authString);
        }

        private static void AdicionaUserAgents(HttpClient client, string username, string description)
        {
            //TryAddWithoutValidation pq com validation nao passa
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", username);
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", description);
        }
    }
}