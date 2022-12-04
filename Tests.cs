using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using UserServiceTest.Model.Extensions;
using UserServiceTest.Model.NewFolder;
using UserServiceTest.Model.Request;

namespace UserServiceTest
{
    internal class Tests

    {
        UserServiceProviders userServiceProviders = new UserServiceProviders();
        private readonly string _baseURL = "https://userservice-uat.azurewebsites.net";
        private readonly HttpClient _httpClient = new HttpClient();

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task RegisterNewUser_ValidatePostRequestStatus_ResponceStatusIsOk()
        {

            var response = await userServiceProviders.CreateUser(new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            });
            
            Assert.AreEqual(HttpStatusCode.OK, response.Status);
        }

        [Test]
        public async Task GetUserInfo_CheckGetUserInfoStatusAndBody_ResponceStatusIsOk()
        {
            var deleteAll = userServiceProviders.DeleteAllUsers();

             CreateRequestBody request=new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };


            CommonResponse<object> commonResponse = await userServiceProviders.CreateUser(request);
            string id = commonResponse.Content;

           HttpRequestMessage getUserRequest = new HttpRequestMessage
             {
             Method = HttpMethod.Get,
            RequestUri = new Uri($"{_baseURL}/CacheManagement")

            };


            HttpResponseMessage getUserResponse = await _httpClient.SendAsync(getUserRequest);
            string responseBody = await getUserResponse.Content.ReadAsStringAsync();
            List<UserResponseBody>? actual = JsonConvert.DeserializeObject<List<UserResponseBody>>(responseBody);

            UserResponseBody expected = new UserResponseBody
            {
                id = Int32.Parse(id),
                firstName = request.firstName,
                lastName = request.lastName,
                isActive = false

            };
            Assert.AreEqual(HttpStatusCode.OK, getUserResponse.StatusCode);
            actual[0].Should().BeEquivalentTo(expected);
        }
 


        [Test]
        public async Task DeleteAllUsers_CheckDeleteRequestResponseStatus_ResponceStatusIsOk()
        {
            var response = await userServiceProviders.DeleteAllUsers();

            Assert.AreEqual(HttpStatusCode.OK, response.Status);
        }

        [Test]
        public async Task DeleteUserById_CheckDeleteRequestStatusAndBody_ResponceStatusIsOkAndBodyIsEmpty()
        {
            var deleteAll = userServiceProviders.DeleteAllUsers();

            //CreateNewUser
            CreateRequestBody request = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };


            CommonResponse<object> commonResponse = await userServiceProviders.CreateUser(request);
            string id = commonResponse.Content;

            //DeleteCreatedUser
            HttpRequestMessage deleteUserByIdResponse = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_baseURL}/Register/DeleteUser?userId={id}")

            };

            HttpResponseMessage response = await _httpClient.SendAsync(deleteUserByIdResponse);



            string actual = await response.Content.ReadAsStringAsync();
       

        
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsEmpty(actual);
        }

        [Test]
        public async Task GetDeleatedUserById_CheckDeletedUserResponseStatusAfterGetRequest_InternalServerError()
        {   
          var deleteAll =await userServiceProviders.DeleteAllUsers();
            CreateRequestBody request = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> commonResponse = await userServiceProviders.CreateUser(request);
            string id = commonResponse.Content;
            
            HttpRequestMessage deleteUserRequest = new HttpRequestMessage
            {
              Method = HttpMethod.Delete,
            RequestUri = new Uri($"{_baseURL}/Register/DeleteUser?userId={id}")
            };
            HttpResponseMessage responseDeletedUser= await _httpClient.SendAsync(deleteUserRequest);

            HttpRequestMessage repeateDeleteUserRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_baseURL}/Register/DeleteUser?userId={id}")
            };
            HttpResponseMessage responseRepeatedDeleteUserRequest = await _httpClient.SendAsync(repeateDeleteUserRequest);
            string actual = await responseRepeatedDeleteUserRequest.Content.ReadAsStringAsync();
            string errorMessage = "Sequence contains no elements";


            
            Assert.AreEqual(HttpStatusCode.InternalServerError, responseRepeatedDeleteUserRequest.StatusCode);
            Assert.AreEqual(actual, errorMessage);
        }

        [Test]
        public async Task GetNotExistingUser_CheckStatusOfNotExistingUser_ResponseStatusIsNotFound()
        {
            var deleteAll = await userServiceProviders.DeleteAllUsers();
            CreateRequestBody request = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> commonResponse = await userServiceProviders.CreateUser(request);
            string id = commonResponse.Content;
            HttpRequestMessage deleteUser = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_baseURL}/Register/DeleteUser?userId={id}")
            };
            HttpResponseMessage responseDeletedUser= await _httpClient.SendAsync(deleteUser); 
            HttpRequestMessage repeateDeleteUserRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseURL}/UserManagement/GetUserStatus?userId={id}")
            };
            HttpResponseMessage responseDeletedUser2 = await _httpClient.SendAsync(repeateDeleteUserRequest);

            Assert.AreEqual(HttpStatusCode.NotFound, responseDeletedUser2.StatusCode);

        }

        [Test]
        public async Task ChangeStatusOnNotExistingUser_CheckStatusOfNotExistingUser_ResponseStatusIsNotFound()
        {
            var deleteAll = await userServiceProviders.DeleteAllUsers();
            CreateRequestBody request = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> commonResponse = await userServiceProviders.CreateUser(request);
            string id = commonResponse.Content;
            string notExistingId = (int.Parse(id)+9999).ToString();
          
       
            HttpRequestMessage changeUsersStatus = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_baseURL}/UserManagement/SetUserStatus?userId={notExistingId}&newStatus={true}")
            };
            HttpResponseMessage responseDeletedUser2 = await _httpClient.SendAsync(changeUsersStatus);

            Assert.AreEqual(HttpStatusCode.NotFound, responseDeletedUser2.StatusCode);

        }

        [Test]
        public async Task GetUserById_CheckResponseStatus_ResponseStatusIsOk()
        {

            var deleteAll = userServiceProviders.DeleteAllUsers();
            CreateRequestBody request = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> commonResponse = await userServiceProviders.CreateUser(request);
            string id = commonResponse.Content;
            HttpRequestMessage getUserByIdrequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseURL}/UserManagement/GetUserStatus?userId={id}")

            };
            
            HttpResponseMessage response = await _httpClient.SendAsync(getUserByIdrequest);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
           

        }
        [Test]
        public async Task GetUserById_CheckResponseBody_ResponseStatusIsOk()
        {

            var deleteAll = userServiceProviders.DeleteAllUsers();
            CreateRequestBody request = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> commonResponse = await userServiceProviders.CreateUser(request);
            string id = commonResponse.Content;
            HttpRequestMessage getUserByIdrequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseURL}/UserManagement/GetUserStatus?userId={id}")

            };
           
            HttpResponseMessage response = await _httpClient.SendAsync(getUserByIdrequest);
            string actual = await response.Content.ReadAsStringAsync();
            string expected = "false";
             actual.Should().Be(expected);

        }
     


        [Test]
        public async Task ChangeUserStatus_CheckUserStatusResponseStatus_ResponseStatusIsOk()
        {
            var deleteAll = userServiceProviders.DeleteAllUsers();
            CreateRequestBody request = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> commonResponse = await userServiceProviders.CreateUser(request);
            string id = commonResponse.Content;
           

            HttpRequestMessage StatusOfUserChangeRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_baseURL}/UserManagement/SetUserStatus?userId={id}&newStatus={true}"),
                
            };

            HttpResponseMessage responseOfChangedStatus = await _httpClient.SendAsync(StatusOfUserChangeRequest);
            Assert.AreEqual(HttpStatusCode.OK, responseOfChangedStatus.StatusCode);
        }


        [Test]
        public async Task ChangeUserStatusFromFalseToTrue_CheckChangedUserStatus_UserStatusIsTrue()
        {
            //Delete All
            var deleteAll = userServiceProviders.DeleteAllUsers();
            
            CreateRequestBody request = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> commonResponse = await userServiceProviders.CreateUser(request);
            string id = commonResponse.Content;
           
            HttpRequestMessage StatusOfUserChangeRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_baseURL}/UserManagement/SetUserStatus?userId={id}&newStatus={true}"),

            };
           
           HttpResponseMessage responseOfChangesUserStatus = await _httpClient.SendAsync(StatusOfUserChangeRequest);
            CommonResponse<bool> getUserStatus = await userServiceProviders.GetUserStatus(id) ;
            string actual = getUserStatus.Content;


            actual.Should().Be("true");
        }

        [Test]
        public async Task ChangeUserStatusFromTrueToFalse_CheckChangedUserStatus_UserStatusIsFalse()
        {
            var deleteAll = userServiceProviders.DeleteAllUsers();
            CreateRequestBody request = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> commonResponse = await userServiceProviders.CreateUser(request);
            string id = commonResponse.Content;
            var responseForChangedStatus = await userServiceProviders.ChangeUserStatus(id,true);

            HttpRequestMessage StatusOfUserChangeRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_baseURL}/UserManagement/SetUserStatus?userId={id}&newStatus={false}"),

            };

          HttpResponseMessage responseOfChangedStatus = await _httpClient.SendAsync(StatusOfUserChangeRequest);
         
          CommonResponse<bool> getUserStatus = await userServiceProviders.GetUserStatus(id);
            string actual = getUserStatus.Content;

            actual.Should().Be("false");
        }




        [Test]
        public async Task IdSequence_CheckIdSequence_AfterDeletingCreatedUserIdIsBiggerByOne()
        {
            var deleteAll = userServiceProviders.DeleteAllUsers();
            CreateRequestBody firtsUserRqeuest = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> commonResponseOfFirstUser = await userServiceProviders.CreateUser(firtsUserRqeuest);
            string idOfFirstUser = commonResponseOfFirstUser.Content;
            var deleteFirstUser = await userServiceProviders.DeleteUser(idOfFirstUser);
            CreateRequestBody secondUserRqeuest = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> commonResponseOfSecondUser = await userServiceProviders.CreateUser(firtsUserRqeuest);
            string idOfSecondtUser = commonResponseOfSecondUser.Content;
            string expected = (int.Parse(idOfFirstUser) +1).ToString();


            Assert.AreEqual(expected, idOfSecondtUser);

        }
    }
}
