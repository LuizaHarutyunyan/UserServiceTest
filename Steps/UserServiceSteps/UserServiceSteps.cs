
using TechTalk.SpecFlow;
using UserServiceTest.DataContext;
using UserServiceTest.Model.Extensions;
using UserServiceTest.Model.NewFolder;
using UserServiceTest.Model.Request;


namespace UserServiceTest.Steps.NewFolder
{
    [Binding]
    internal class UserServiceSteps
    {
        private UserServiceProviders _userServiceProviders = new UserServiceProviders();

        private UserServiceDataContext _context;


        public UserServiceSteps(UserServiceDataContext context)
        {
            _context = context;
        }

        [When(@"Create User with ([^']*) and ([^']*)")]
        public async Task CreateUser(string firstname, string lastname)
        {
            CreateRequestBody body = new CreateRequestBody
            {
                firstName = firstname,
                lastName = lastname
            };

            CommonResponse<object> createUserResponse = await _userServiceProviders.CreateUser(body);

            var userId = createUserResponse.Content;
            _context.UserId = userId;
            _context.CreateUserResponse = createUserResponse;
        }

        [When(@"Create User with null values")]
        public async Task CreateUserWithNullValues()
        {
    
            await CreateUser(null, null);
        }

        [Given("User is created")]
        [When("User is created")]
        public async Task CreateUser()
        {

            await CreateUser("luiza", "harutyunyan");
        }

        [Given("Unexisted user")]
        public async Task CreateUnexistedUser()
        {
            var notExistingId = (int.Parse(_context.UserId) + 9999).ToString();
            _context.NotExistedUserId = notExistingId;

        }

        [When("Delete user by Id")]
        public async Task DeleteUser()
        {
            CommonResponse<UserResponseBody> deleteUser = await _userServiceProviders.DeleteUser(_context.UserId);
            _context.DeleteUserResponse = deleteUser;

        }

        [When("Delete user by not existing Id")]
        public async Task DeleteUserByNotExistingId()
        {
           
            CommonResponse<UserResponseBody> deleteUser = await _userServiceProviders.DeleteUser(_context.NotExistedUserId);
            _context.DeleteUserResponse = deleteUser;

        }

        [When("Change user status of not existing user")]
        public async Task ChangeStatusOfNotExistingUser()
        {
            string notExistingId = (int.Parse(_context.UserId) + 9999).ToString();
            _context.UserId = notExistingId;
            CommonResponse<UserResponseBody> changeStatus = await _userServiceProviders.ChangeUserStatus(notExistingId, true);
            _context.ChangeUserStatusResponse = changeStatus;
        }
        [Given(@"Change user status to '(.*)'")]
        [When(@"Change user status to '(.*)'")]

        public async Task ChangeStatus(bool status)
        {

            CommonResponse<UserResponseBody> changeStatus = await _userServiceProviders.ChangeUserStatus(_context.UserId, status);
            _context.ChangeUserStatusResponse = changeStatus;
        }

        [When("Get user status by Id")]
        public async Task GetUserStatus()
        {
            CommonResponse<bool> getUser = await _userServiceProviders.GetUserStatus(_context.UserId);
            _context.GetUserStatusResponse = getUser;

        }



    }
}
