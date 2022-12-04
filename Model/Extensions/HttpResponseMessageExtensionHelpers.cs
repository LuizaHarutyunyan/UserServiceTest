using Newtonsoft.Json;
using UserServiceTest.Model.NewFolder;

namespace UserServiceTest.Model.Extensions
{
    internal static class HttpResponseMessageExtensionHelpers
    {

        public static async Task<CommonResponse<T>> ToCommonResponse<T>(this HttpResponseMessage message)
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