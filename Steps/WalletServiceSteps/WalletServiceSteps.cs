
using TechTalk.SpecFlow;
using UserServiceTest.DataContext;
using UserServiceTest.Model.Extensions;
using UserServiceTest.Model.NewFolder;
using UserServiceTest.Model.Request;
using Task = System.Threading.Tasks.Task;

namespace UserServiceTest.Steps.WalletServiceSteps
{
    [Binding]
    internal class WalletServiceSteps
    {
        private WalletServiceDataContext _context;
        UserServiceProviders userServiceProviders = new UserServiceProviders();
        WalletServiceProvider walletServiceProvider = new WalletServiceProvider();
        public WalletServiceSteps(WalletServiceDataContext context)
        {
            _context = context;
        }

        [BeforeScenario("RegisteredActiveUser")]
        public async Task SetUp()
        {
            CreateRequestBody request = new CreateRequestBody
            {
                firstName = "luiza",
                lastName = "harutyunyan"
            };


            CommonResponse<object> createUserResponse = await userServiceProviders.CreateUser(request);
            var userId = createUserResponse.Content;
            _context.UserId = userId;
            _context.CreateUserResponse = createUserResponse;


            var changeUserStatusActive = await userServiceProviders.ChangeUserStatus(userId, true);
            _context.ChangeUserStatusResponse= changeUserStatusActive;
        }
      

        [When("Get user balance")]
        [Given("Get user balance")]
        public async Task GetUserBalance()
        {
            var getBalanaceResponse = await walletServiceProvider.GetBalance(_context.UserId);
            _context.GetUserBalance = getBalanaceResponse;
        }
        [When("Get balance of unexisted user")]
        public async Task GetUnexistedUserBalance()
        {
           
            var getBalanaceResponse = await walletServiceProvider.GetBalance(_context.NotExistedUserId);
            _context.GetUserBalance = getBalanaceResponse;
        }
        [When("Cancel transaction")]
        public async Task CancelTransaction()
        {
            CommonResponse<string> revertTransaction = await walletServiceProvider.CancelTransaction(_context.TransactionId);
            _context.RevertTransaction = revertTransaction;

        }
        [Given("Not existed user")]
        public async Task CreateNotExistedUser()
        {
            var notExistingId = (int.Parse(_context.UserId) + 9999).ToString();
            _context.NotExistedUserId = notExistingId;
            

        }
        [Given(@"Changed status '(.*)'")]
        public async Task ChangeStatus(bool status)
        {

            CommonResponse<UserResponseBody> changeStatus = await userServiceProviders.ChangeUserStatus(_context.UserId, status);
            _context.ChangeUserStatusResponse = changeStatus;
        }

        [Given(@"Wrong transaction id")]
        public void GivenWrongTransactionId()
        {
            Guid randomId = new Guid();
            _context.RandomId = randomId;
        }


        [Given("Change user balance with amount '([^']*)'")]
        [When(@"Change user balance with amount '(.*)'")]
        public async Task ChangeBalance(double amount)
        {
            BalanceChange balance = new BalanceChange
            {
                userId = _context.UserId,

                amount = amount
            };
            CommonResponse<string> changeBalanceResponse = await walletServiceProvider.ChangeBalance(balance);
            var Id = changeBalanceResponse.Body;
            _context.TransactionId = Id;
            _context.ChangeBalance = changeBalanceResponse;
        }

        [When("Change unexisted user balance with '([^']*)'")]
        public async Task ChangeUnExistedUserBalance(double amount)
        {
            BalanceChange balance = new BalanceChange
            {
                userId = _context.NotExistedUserId,
                amount = amount
            };
            CommonResponse<string> changeBalanceResponse = await walletServiceProvider.ChangeBalance(balance);
            var Id = changeBalanceResponse.Body;
            _context.TransactionId = Id;
            _context.ChangeBalance = changeBalanceResponse;
        }
        [When("Cancel transaction with wrong id")]
        public async Task CancelTransactionWithWrongId()
        {
            var revertTransaction = await walletServiceProvider.CancelTransaction((_context.RandomId).ToString());
            _context.RevertTransaction = revertTransaction;

        }


    }
}
