using static UserServiceTest.Tests.UserServiceTest;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using UserServiceTest.Model.NewFolder;

namespace UserServiceTest.Model.Extensions
{
    static class HttpResponseMessageExtension
    {

        public static async Task<CommonResponse<T>> ToCommonResponse<T>(HttpResponseMessage message)
        {
            string responseString = await message.Content.ReadAsStringAsync();


            var commonReponse = new CommonResponse<T>
            {
                Status = message.StatusCode,
                Content = responseString
            };

            try
            {
                commonReponse.Body = JsonConvert.DeserializeObject<T>(responseString);
            }
            catch (JsonReaderException)
            {
            }

            return commonReponse;
        }
    }
}
