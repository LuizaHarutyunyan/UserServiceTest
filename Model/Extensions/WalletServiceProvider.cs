using Newtonsoft.Json;
using System.Text;
using UserServiceTest.Model.NewFolder;
using UserServiceTest.Model.Request;

namespace UserServiceTest.Model.Extensions
{


    internal class WalletServiceProvider
    {
        private readonly string _baseURL = "https://walletservice-uat.azurewebsites.net/";
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<CommonResponse<object>> GetBalance(string id)
        {
            HttpRequestMessage getUserBalanaceById = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseURL}/Balance/GetBalance?userId={id}")
            };
       

            HttpResponseMessage userBalanceResponse = await _httpClient.SendAsync(getUserBalanaceById);

            return await HttpResponseMessageExtension.ToCommonResponse<object>(userBalanceResponse);

        }
        public async Task<CommonResponse<string>> ChangeBalance(BalanceChange request)
        {


            string serializedBody = JsonConvert.SerializeObject(request);


            HttpRequestMessage changeBalanceRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseURL}/Balance/Charge"),
                Content = new StringContent(serializedBody, Encoding.UTF8, "application/json")
            };

       
            HttpResponseMessage changeBalanceResponse = await _httpClient.SendAsync(changeBalanceRequest);
            return await HttpResponseMessageExtension.ToCommonResponse<string>(changeBalanceResponse);
        }
        public async Task<CommonResponse<string>> CancelTransaction(string id)
        {

            
            HttpRequestMessage StatusOfUserChangeRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_baseURL}/Balance/RevertTransaction?transactionId={id}"),

            };

            HttpResponseMessage responseOfChangedStatus = await _httpClient.SendAsync(StatusOfUserChangeRequest);

            return await HttpResponseMessageExtension.ToCommonResponse<string>(responseOfChangedStatus);

        }
    }
}
