using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Reactive.Linq;
using UserServiceTest.Model.Extensions;
using UserServiceTest.Model.NewFolder;
using UserServiceTest.Model.Request;
using Task = System.Threading.Tasks.Task;

namespace UserServiceTest.Tests
{
    internal class UserServiceTest

    {
        UserServiceProviders userServiceProviders = new UserServiceProviders();



        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }
        [TearDown]
        public async Task TearDown()
        {
            await userServiceProviders.DeleteAllCreatedUsers();

        }

        [TestCase("Luiza", "Harutyunyan")]
        [TestCase("", "")]
        [TestCase("012345", "543210")]
        [TestCase("@luiza", "harutyunyan#")]
        [TestCase("l", "h")]
        [TestCase("$", "$")]
        [TestCase("LUIZA", "HARUTYUNYAN")]

        public async Task RegisterNewUser_ValidatePostRequestStatus_ResponceStatusIsOk(string firstname, string lastname)
        {
            //Action
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = firstname,
                lastName = lastname
            };

            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string responseBody = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(responseBody);
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, createUser.Status);
        }


        [Test]
        public async Task RegisterNewUserFieldsEqualToNull_ValidateStatusCode_ResponceStatusIs500()
        {
            //Action
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = null,
                lastName = null
            };

            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string responseBody = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(responseBody);
            //Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, createUser.Status);
        }



        [Test]
        public async Task DeleteAllUsers_CheckDeleteRequestResponseStatus_ResponceStatusIsOk()
        {
            //Action
            var response = await userServiceProviders.DeleteAllUsers();
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.Status);
        }

        [Test]
        public async Task DeleteUserById_CheckDeleteRequestStatusCode_ResponceStatusIsOk()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };


            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Action
            CommonResponse<UserResponseBody> deleteUser = await userServiceProviders.DeleteUser(id);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, deleteUser.Status);

        }


        [Test]
        public async Task DeleteUserById_CheckDeleteRequestBody_ResponceBodyIsEmpty()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };


            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Action
            CommonResponse<UserResponseBody> deleteUser = await userServiceProviders.DeleteUser(id);
            string actual = deleteUser.Content;
            //Assert
            Assert.IsEmpty(actual);
        }
        [Test]
        public async Task DeleteNotActiveUser_CheckDeleteResponseStatus_ResponceStatusIsOk()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };


            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Action
            CommonResponse<UserResponseBody> deleteUser = await userServiceProviders.DeleteUser(id);
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, deleteUser.Status);

        }

        [Test]
        public async Task DeleteNotActiveUser_CheckDeleteRequestBody_ResponceBodyIsEmpty()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);

            //Action
            CommonResponse<UserResponseBody> deleteUser = await userServiceProviders.DeleteUser(id);
            string actual = deleteUser.Content;

            //Assert
            Assert.IsEmpty(actual);

        }

        [Test]
        public async Task ValidateUserStatusAfterRepeatedDeleting_CheckResponseStatusAndErrorMessage_InternalServerError()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Action
            CommonResponse<UserResponseBody> deleteUser = await userServiceProviders.DeleteUser(id);
            CommonResponse<UserResponseBody> repeatdeleteUser = await userServiceProviders.DeleteUser(id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.InternalServerError, repeatdeleteUser.Status);
                Assert.AreEqual(repeatdeleteUser.Content, "Sequence contains no elements");
            });

        }

        [Test]
        public async Task ValidateNotExistingUser_CheckStatusOfNotExistingUser_ResponseStatusIsNotFound()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };
            //Action
            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            string notExistingId = (int.Parse(id) + 9999).ToString();
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Action
            CommonResponse<UserResponseBody> deleteUser = await userServiceProviders.DeleteUser(notExistingId);

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, deleteUser.Status);

        }

        [Test]
        public async Task ChangeStatusOfNotExistingUser_CheckResponseStatus_ResponseStatusIsNotFound()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Action
            string notExistingId = (int.Parse(id) + 9999).ToString();
            //Action
            CommonResponse<UserResponseBody> changeStatus = await userServiceProviders.ChangeUserStatus(notExistingId, true);
            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, changeStatus.Status);

        }

        [Test]
        public async Task GetUserStatusById_CheckResponseStatus_ResponseStatusIsOk()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Action
            CommonResponse<bool> getUser = await userServiceProviders.GetUserStatus(id);
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, getUser.Status);


        }
        [Test]
        public async Task GetUserStatusById_CheckResponseBody_ResponseStatusIsOk()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };


            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Action
            CommonResponse<bool> userStatus = await userServiceProviders.GetUserStatus(id);
            string actual = userStatus.Content;
            //Assert
            actual.Should().Be("false");

        }



        [Test]
        public async Task ChangeUserStatusFromFalseToTrue_CheckUserStatusResponseStatus_ResponseStatusIsOk()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Action
            CommonResponse<UserResponseBody> userStatus = await userServiceProviders.ChangeUserStatus(id, true);
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, userStatus.Status);
        }


        [Test]
        public async Task ChangeUserStatusFromFalseToTrue_CheckChangedUserStatus_UserStatusIsTrue()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Action
            CommonResponse<UserResponseBody> userStatus = await userServiceProviders.ChangeUserStatus(id, true);
            //Action
            CommonResponse<bool> getUserStatus = await userServiceProviders.GetUserStatus(id);
            string actual = getUserStatus.Content;
            //Assert
            actual.Should().Be("true");
        }

        [Test]
        public async Task ChangeUserStatusFromTrueToFalse_CheckChangedUserStatus_UserStatusIsFalse()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Precondition
            CommonResponse<UserResponseBody> userStatusTrue = await userServiceProviders.ChangeUserStatus(id, true);
            //Action
            CommonResponse<UserResponseBody> userStatusFalse = await userServiceProviders.ChangeUserStatus(id, false);
            CommonResponse<bool> getUserStatus = await userServiceProviders.GetUserStatus(id);
            string actual = getUserStatus.Content;
            //Assert
            actual.Should().Be("false");
        }


        [Test]
        public async Task ChangeUserStatusFromFalseTrueFalse_CheckUserStatus_UserStatusFalse()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Precondition
            CommonResponse<UserResponseBody> userStatusFalseFirst = await userServiceProviders.ChangeUserStatus(id, false);
            //Action
            CommonResponse<UserResponseBody> userStatusTrue = await userServiceProviders.ChangeUserStatus(id, true);
            //Action
            CommonResponse<UserResponseBody> userStatusFalseSecond = await userServiceProviders.ChangeUserStatus(id, false);
            //Action
            CommonResponse<bool> getUserStatus = await userServiceProviders.GetUserStatus(id);
            string actual = getUserStatus.Content;
            //Assert
            actual.Should().Be("false");

        }

        [Test]
        public async Task ChangeUserStatusFromFalseTrueFalseTrue_CheckUserStatus_UserStatusIsTrue()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Action
            CommonResponse<UserResponseBody> userStatusFirstChange = await userServiceProviders.ChangeUserStatus(id, true);
            //Action
            CommonResponse<UserResponseBody> userStatusSecondChange = await userServiceProviders.ChangeUserStatus(id, false);
            //Action
            CommonResponse<UserResponseBody> userStatusThirdChange = await userServiceProviders.ChangeUserStatus(id, true);
            //Action
            CommonResponse<bool> getUserStatus = await userServiceProviders.GetUserStatus(id);
            string actual = getUserStatus.Content;
            //Assert
            actual.Should().Be("true");

        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task ChangeUserStatus_CheckStatusCode_UserStatusIsOk(bool status)
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };


            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Action
            CommonResponse<UserResponseBody> userStatus = await userServiceProviders.ChangeUserStatus(id, status);
            //Assert
            Assert.AreEqual(userStatus.Status, HttpStatusCode.OK);

        }

        [Test]
        public async Task ChangeUserStatusTrueToTrue_CheckStatus_UserStatusIsTrue()
        {
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> createUser = await userServiceProviders.CreateUser(body);
            string id = createUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(id);
            //Action
            CommonResponse<UserResponseBody> userStatusFirstChange = await userServiceProviders.ChangeUserStatus(id, true);
            //Action
            CommonResponse<UserResponseBody> userStatusSecondChange = await userServiceProviders.ChangeUserStatus(id, true);
            CommonResponse<bool> getUserStatus = await userServiceProviders.GetUserStatus(id);

            string actual = getUserStatus.Content;
            //Assert
            actual.Should().Be("true");

        }



        [Test]
        public async Task IdSequence_CheckIdSequence_AfterDeletingCreatedUserIdIsBiggerByOne()
        {
            List<string> ids = new List<string>();
            //Precondition
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = "serhii",
                lastName = "mykhailov"
            };

            CommonResponse<object> createFirstUser = await userServiceProviders.CreateUser(body);
            string idOfFirstUser = createFirstUser.Content;
            //Action
            CommonResponse<UserResponseBody> deleteUser = await userServiceProviders.DeleteUser(idOfFirstUser);
            //Action
            CommonResponse<object> createSecondUser = await userServiceProviders.CreateUser(body);
            string idOfSecondtUser = createSecondUser.Content;
            userServiceProviders.AddUserTocreatedUsersCollection(idOfSecondtUser);
            ids.Add(idOfFirstUser);
            ids.Add(idOfSecondtUser);
            //Assert
            CollectionAssert.IsOrdered(ids);

        }
    }


}

