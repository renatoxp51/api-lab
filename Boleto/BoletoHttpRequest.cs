using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace LabReserva.Boleto
{
    public class BoletoHttpRequest : InterfaceBoleto
    {
        public async Task<BoletoResponseContent> pagarBoleto(string numeroBoleto, int userId)
        {
            BoletoBody boletoBody = new BoletoBody
            {
                boleto = numeroBoleto,
                user_id = userId
            };

            // inicia o client Http
            HttpClient client = new HttpClient();

            // converte a variavel "boletoBody" em um Json para ir no body da requisição
            var jsonBody = System.Text.Json.JsonSerializer.Serialize(boletoBody);

            // inicia a requisição POST com a URL
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api-go-wash-efc9c9582687.herokuapp.com/api/pay-boleto");

            // incluindo o corpo de requisição
            request.Content = new StringContent(jsonBody);

            // informa o tipo do corpo da requisição
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // adiciona a autenticação no header
            // obs.: nesse caso, as variáveis estão sendo colocadas diretamente no código por fins didáticos, o que não aconteceria no dia a dia de uma empresa
            request.Headers.Add("Authorization", "Vf9WSyYqnwxXODjiExToZCT9ByWb3FVsjr");

            // enviando a requisição e armazenando o retorno
            var response = await client.SendAsync(request);

            // verifica se a requisição aconteceu com sucesso
            response.EnsureSuccessStatusCode();

            /*
             * A partir daqui estou somente tratando o resultado para retornar o objeto do tipo BoletoResponseContent
             */

            // convertendo a resposta da requisição em string 
            var content = await response.Content.ReadAsStringAsync();

            // convertendo a string em um objeto do tipo BoletoResponse
            BoletoResponse dataResponse = JsonConvert.DeserializeObject<BoletoResponse>(content);

            // a API do professor retorna da seguinte forma:
            // {"data":{"boleto":"12345678","user_id":6,"payment_date":"2023-10-27","status":"approved"}}
            // por conta disso estou criando um objeto abaixo para não retornar um "dicionario" dentro de outro.
            BoletoResponseContent resultado = new BoletoResponseContent
            {
                boleto = dataResponse.data.boleto,
                user_id = dataResponse.data.user_id,
                payment_date = dataResponse.data.payment_date,
                status = dataResponse.data.status
            };

            return resultado;
        }
    }
}
