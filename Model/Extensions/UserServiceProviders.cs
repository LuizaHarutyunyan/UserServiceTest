using Newtonsoft.Json;
using System.Text;
using UserServiceTest.Model.Request;
using UserServiceTest.Model.Extensions;
using UserServiceTest.Model.NewFolder;
using System.Net.Http;
using System.Net;

namespace UserServiceTest.Model.Extensions
{
    class UserServiceProviders
    {
        private readonly string _baseURL = "https://userservice-uat.azurewebsites.net";
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<CommonResponse<Object>> CreateUser(CreateRequestBody request)
        {


            string serializedBody = JsonConvert.SerializeObject(request);

            //Request
            HttpRequestMessage CreateUserRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseURL}/Register/RegisterNewUser"),
                Content = new StringContent(serializedBody, Encoding.UTF8, "application/json")
            };

            //Response
            HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(CreateUserRequest);
            return await HttpResponseMessageExtension.ToCommonResponse<Object>(httpResponseMessage);
        }


        public async Task<CommonResponse<Boolean>> GetUserStatus(string id)
        {
            HttpRequestMessage getUserStatusById = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseURL}/UserManagement/GetUserStatus?userId={id}")
            };

            HttpResponseMessage userStatusResponse = await _httpClient.SendAsync(getUserStatusById);

            return await HttpResponseMessageExtension.ToCommonResponse<bool>(userStatusResponse);

        }
        public async Task<CommonResponse<UserResponseBody>> DeleteUser(string id)
        {
            HttpRequestMessage getUserStatusById = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_baseURL}/Register​/DeleteUser?userId={id}")
            };

            HttpResponseMessage userStatusResponse = await _httpClient.SendAsync(getUserStatusById);

            return await HttpResponseMessageExtension.ToCommonResponse<UserResponseBody>(userStatusResponse);

        }
        public async Task<CommonResponse<UserResponseBody>> GetAllUsers()
        {
            HttpRequestMessage getUserStatusById = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseURL}/CacheManagement")
            };

            HttpResponseMessage userStatusResponse = await _httpClient.SendAsync(getUserStatusById);

            return await HttpResponseMessageExtension.ToCommonResponse<UserResponseBody>(userStatusResponse);

        }

        public async Task<CommonResponse<UserResponseBody>> DeleteAllUsers()
        {
            HttpRequestMessage getUserStatusById = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_baseURL}/CacheManagement")
            };

            HttpResponseMessage userStatusResponse = await _httpClient.SendAsync(getUserStatusById);

            return await HttpResponseMessageExtension.ToCommonResponse<UserResponseBody>(userStatusResponse);

        }

        public async Task<CommonResponse<UserResponseBody>> ChangeUserStatus(string id,bool status)
        {
            

            HttpRequestMessage StatusOfUserChangeRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_baseURL}/UserManagement/SetUserStatus?userId={id}&newStatus={status}"),

            };

            HttpResponseMessage responseOfChangedStatus = await _httpClient.SendAsync(StatusOfUserChangeRequest);

            return await HttpResponseMessageExtension.ToCommonResponse<UserResponseBody>(responseOfChangedStatus);

        }


    }
}
