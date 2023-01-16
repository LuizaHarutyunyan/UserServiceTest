
using NUnit.Framework;
using System.Net;
using TechTalk.SpecFlow;
using UserServiceTest.DataContext;


namespace UserServiceTest.Steps.NewFolder
{
    [Binding]
    internal class UserServiceAsserts
    {
        private UserServiceDataContext _context;

        public UserServiceAsserts(UserServiceDataContext context)
        {
            _context = context;
        }

        [Then("Register new user status code is '(.*)'")]
        public async Task ThenRegisterNewUserStatusCodeIs(HttpStatusCode expectedStatusCode)
        {
            var createUserResponse = _context.CreateUserResponse;
            Assert.AreEqual(expectedStatusCode, createUserResponse.Status);
        }

        [Then("Deleted user status code is '(.*)'")]
        public async Task ThenUserResponseStatusForDeletedUserIs(HttpStatusCode expectedStatusCode)
        {
            var deleteUserResponse = _context.DeleteUserResponse;
            Assert.AreEqual(expectedStatusCode, deleteUserResponse.Status);
        }

        [Then("Deleted user error message is '([^']*)'")]
        public async Task ThenUserResponseErrorMessageForDeletedUserIs(string errormessage)
        {
            var deleteUserResponse = _context.DeleteUserResponse;
            Assert.AreEqual(deleteUserResponse.Content, errormessage);
        }

        [Then("Deleted user body is empty")]
        public async Task ThenUserResponseBodyForDeletedUserIs()
        {
            var deleteUserResponse = _context.DeleteUserResponse;
            Assert.IsEmpty(deleteUserResponse.Content);
        }

        [Then("User status change status code is '(.*)'")]
        public async Task ThenUserResponseStatusCodeForUserChangedStatusIs(HttpStatusCode expectedStatusCode)
        {
            var changeStatusUserResponse = _context.ChangeUserStatusResponse;
            Assert.AreEqual(expectedStatusCode, changeStatusUserResponse.Status);
        }
        [Then("Get user status status code is '(.*)'")]
        public async Task ThenUserResponseStatusCodeResponseStatusIs(HttpStatusCode expectedStatusCode)
        {
            var getStatusUserResponse = _context.GetUserStatusResponse;
            Assert.AreEqual(expectedStatusCode, getStatusUserResponse.Status);
        }

        [Then(@"User status body is '([^']*)'")]
        public async Task ThenUserResponseStatusCodeBodyIs(string @expectedBody)
        {
            var getStatusUserResponse = _context.GetUserStatusResponse;

            Assert.AreEqual(expectedBody, getStatusUserResponse.Content);
        }
        [Then(@"Created user ids are ordered")]
        public void ThenNewlyCreatedUserIdIsBiggerThanThePreviousOne()
        {

            CollectionAssert.IsOrdered(_context.UserIdCollection);
        }




    }
}
