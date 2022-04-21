using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Game_Testes_WPF.API
{
    public class ApiAzure
    {
        public HttpClient HttpClient { get; }
        public ApiAzure()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("http://gamesfunction.azurewebsites.net");
            HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }


        public int GetMin()
        {
            HttpResponseMessage response = HttpClient.GetAsync("/api/ObterMin/ballons").Result;
            HttpContent content = response.Content;
            string str = content.ReadAsStringAsync().Result;
            return Convert.ToInt32(str);

        }

        public void Salvar(Record record)
        {
            HttpContent strContent = new StringContent(JsonSerializer.Serialize(record));
            var response = HttpClient.PostAsync("/api/Salvar", strContent).Result;
            if(response.IsSuccessStatusCode)
            {

            } 


        }

        public List<Record> GetAll()
        {
            HttpResponseMessage response = HttpClient.GetAsync("/api/ObterTodos/ballons").Result;
            HttpContent content = response.Content;
            string str = content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<List<Record>>(str);
        }
    }
}
