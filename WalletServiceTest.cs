using NUnit.Framework;
using System;
using System.Net;
using System.Text;
using UserServiceTest.Model.Extensions;
using UserServiceTest.Model.Request;
using UserServiceTest.Model.NewFolder;
using Newtonsoft.Json;
//using Task = Microsoft.Exchange.WebServices.Data.Task;

namespace UserServiceTest
{
    [TestFixture]
    internal class WalletServiceTest
    {
        UserServiceProviders userServiceProviders = new UserServiceProviders();
        WalletServiceProvider walletServiceProvider = new WalletServiceProvider();
        private string _userId { get; set; }



        [SetUp]
        public async Task Setup()
        {
            CreateRequestBody request = new CreateRequestBody
            {
                firstName = "luiza",
                lastName = "harutyunyan"
            };


            CommonResponse<object> commonResponse = await userServiceProviders.CreateUser(request);
            _userId = commonResponse.Content;


            var changeUserStatusActive = await userServiceProviders.ChangeUserStatus(_userId, true);

        }

        [Test]
        public async Task GetUserBalance_ValidateGetBalanseResponseStatus_ResponceStatusIsOk()
        {

            var getBalanaceResponse = await walletServiceProvider.GetBalance(_userId);
            Assert.AreEqual(HttpStatusCode.OK, getBalanaceResponse.Status);

        }
        [Test]
        public async Task GetUserBalance_ValidateGetBalanseResponseBody_ResponceStatusIsOk()
        {
            var responseGetBalance = await walletServiceProvider.GetBalance(_userId);
            string body = responseGetBalance.Content;
            Assert.AreEqual(body, "0");

        }


        [Test]
        public async Task GetUserBalanceOfUnExistedUser_ValidateGetBalanseResponseStatusAndMessage_ResponceStatusIsInternalServerError()
        {
            string userId = (int.Parse(_userId) + 999).ToString();
            var responseGetBalance = await walletServiceProvider.GetBalance(userId);
            string actual = responseGetBalance.Content;
            string errorMessage = "not active user";
            Assert.AreEqual(HttpStatusCode.InternalServerError, responseGetBalance.Status);
            Assert.AreEqual(errorMessage, actual);

        }
        [Test]
        public async Task GetUserBalanceOfInActiveUser_ValidateGetBalanseResponseStatusAndMessage_ResponceStatusIsInternalServerError()
        {

            var changeUserStatusInActive = await userServiceProviders.ChangeUserStatus(_userId, false);

            var responseGetBalance = await walletServiceProvider.GetBalance(_userId);
            string actual = responseGetBalance.Content;
            string errorMessage = "not active user";
            Assert.AreEqual(HttpStatusCode.InternalServerError, responseGetBalance.Status);
            Assert.AreEqual(actual, errorMessage);

        }
        [Test]
        public async Task CreateNewTransaction_ChangeUserBalanaceCheckResponseStatus_ResponceStatusIsOk()
        {
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 10
            };
            CommonResponse<String> responseOfChange = await walletServiceProvider.ChangeBalance(balance);
            Assert.AreEqual(HttpStatusCode.OK, responseOfChange.Status);
        }

        [Test]
        public async Task CreateNewTransaction_ChangeUserBalanaceCheckBody_BodyIsIdInGuidFormat()
        {
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 10
            };
            CommonResponse<String> responseOfChange = await walletServiceProvider.ChangeBalance(balance);

            string actual = responseOfChange.Content;
            Assert.NotNull(actual);
        }
        [Test]
        public async Task ChangeBalanceOfUnExistedUser_ValidateGetBalanseResponseStatusAndMeLssage_ResponceStatusIsInternalServerError()
        {
            string userId = (int.Parse(_userId) + 999).ToString();
            BalanceChange balance = new BalanceChange
            {
                userId = userId,
                amount = 10
            };
            CommonResponse<String> response = await walletServiceProvider.ChangeBalance(balance);
            string actual = response.Content;
            string errorMessage = "not active user";
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.Status);
            Assert.AreEqual(errorMessage, actual);

        }

        [Test]
        public async Task BalanceChangeWithNegativeAmountWhenBalanceIs0_ChangeUserBalanaceWithNegativeAmountCheckStatusAndMessage_ResponceStatusIsInternalServerError()
        {


            var responseGetBalance = await walletServiceProvider.GetBalance(_userId);
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = -100
            };
            CommonResponse<String> response = await walletServiceProvider.ChangeBalance(balance);
            string actual = response.Content;
            string currentBalance = responseGetBalance.Content;
            string errorMessage = $"User have '{currentBalance}', you try to charge '{balance.amount}.0'.";
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.Status);
            Assert.AreEqual(errorMessage, actual);


        }
        [Test]
        public async Task BalanceChangeWithNegativeAmountWhenBalanceIsPositive_ChangeUserBalanaceWithNegativeAmountCheckStatusAndMessage_ResponceStatusIsInternalServerError()
        {

            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 50
            };

            var responseMessage = await walletServiceProvider.ChangeBalance(balance);
            BalanceChange changedBalance = new BalanceChange
            {
                userId = _userId,
                amount = -1000
            };
            CommonResponse<String> response = await walletServiceProvider.ChangeBalance(changedBalance);
            string actual = response.Content;
            string errorMessage = $"User have '{balance.amount}.0', you try to charge '{changedBalance.amount}.0'.";
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.Status);
            Assert.AreEqual(errorMessage, actual);


        }
        [Test]
        public async Task ChangeBalanceWithZeroAmount_CheckResponseStatusAndErrorMessage_ResponceStatusIsInternalServerError()
        {
            BalanceChange amount = new BalanceChange
            {
                userId = _userId,
                amount = 0
            };
            CommonResponse<String> response = await walletServiceProvider.ChangeBalance(amount);
            string actual = response.Content;
            string errorMessage = "Amount cannot be '0'";
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.Status);
            Assert.AreEqual(actual, errorMessage);

        }

        [Test]
        public async Task BalancePlusAmountBiggerThan10m_CheckResponseStatusAndErrorMessage_ResponceStatusIsInternalServerError()
        {

            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 5000000.0
            };
            var responseMessage = await walletServiceProvider.ChangeBalance(balance);
            BalanceChange changedBalance = new BalanceChange
            {
                userId = _userId,
                amount = 5000001.0
            };
            var responseMessageChangedBaalance = await walletServiceProvider.ChangeBalance(changedBalance);
            string actual = responseMessageChangedBaalance.Content;
            string errorMessage = $"After this charge balance could be '{changedBalance.amount + balance.amount}.0', maximum user balance is '10000000'";
            Assert.AreEqual(HttpStatusCode.InternalServerError, responseMessageChangedBaalance.Status);
            Assert.AreEqual(errorMessage, actual);


        }

        [TestCase(500000, 500000)]
        [TestCase(499999, 500000)]
        public async Task BalancePlusAmountSmallerOrEqual10m_CheckResponseStatus_ResponceStatusIsOk(double firstAmount, double secondAmount)
        {

            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = firstAmount
            };
            var responseMessage = await walletServiceProvider.ChangeBalance(balance);
            BalanceChange changedBalance = new BalanceChange
            {
                userId = _userId,
                amount = secondAmount
            };
            var responseMessageChangedBaalance = await walletServiceProvider.ChangeBalance(changedBalance);
            Assert.AreEqual(HttpStatusCode.OK, responseMessageChangedBaalance.Status);

        }

        [TestCase(0.1, 0.001)]
        [TestCase(5.78, 6.320)]
        public async Task AmountValueAfterDotMustHavPrecisionTwoNumbers_CheckCheckResponseStatusAndErrorMessage_ResponceStatusIsInternalServerError(double firstAmount, double secondAmount)


        {

            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = firstAmount
            };
            var responseMessage = await walletServiceProvider.ChangeBalance(balance);
            BalanceChange changedBalance = new BalanceChange
            {
                userId = _userId,
                amount = secondAmount
            };
            var responseMessageChangedBaalance = await walletServiceProvider.ChangeBalance(changedBalance);
            string actual = responseMessageChangedBaalance.Content;

            string errorMessage = "Amount value must have precision 2 numbers after dot";
            Assert.AreEqual(HttpStatusCode.InternalServerError, responseMessageChangedBaalance.Status);
            Assert.AreEqual(errorMessage, actual);


        }
        [Test]
        public async Task RevertTransaction_CheckNewlyCreatedTransactionResponseStatus_ResponseStatusIsOk()
        {
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 300
            };
            CommonResponse<string> ChangeBalance = await walletServiceProvider.ChangeBalance(balance);
            string Id = ChangeBalance.Body;


            CommonResponse<string> revertTransaction = await walletServiceProvider.CancelTransaction(Id);

            Assert.AreEqual(HttpStatusCode.OK, revertTransaction.Status);
        }

        [Test]
        public async Task RevertTransaction_CheckNewlyCreatedTransactionBody_BodyIsIDInGuidFormat()
        {
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 300
            };
            CommonResponse<string> ChangeBalance = await walletServiceProvider.ChangeBalance(balance);
            string id = ChangeBalance.Content;


            CommonResponse<string> revertTransaction = await walletServiceProvider.CancelTransaction(id);

            Assert.IsNotNull(revertTransaction);
        }

        [Test]
        public async Task RevertTransactionDoesNotExist_CheckResponseStatusAndErrorMessage_ResponseStatusIsNotFound()
        {
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 300
            };
            CommonResponse<string> ChangeBalance = await walletServiceProvider.ChangeBalance(balance);
            Guid randomId = new Guid();
            CommonResponse<string> revertTransaction = await walletServiceProvider.CancelTransaction(randomId.ToString());
            string actual = revertTransaction.Content;
            string errorMessage = "The given key was not present in the dictionary.";

            Assert.AreEqual(HttpStatusCode.NotFound, revertTransaction.Status);
            Assert.AreEqual(actual, errorMessage);
        }

        [Test]
        public async Task RevertTransactionIfTransactionIsAlreadyReverted_CheckResponseStatusAndErrorMessage_ResponseStatusIsInternalServerError()
        {
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 300
            };
            CommonResponse<string> ChangeBalance = await walletServiceProvider.ChangeBalance(balance);
            string Id = ChangeBalance.Body;


            CommonResponse<string> revertTransaction = await walletServiceProvider.CancelTransaction(Id);
            CommonResponse<string> revertAlreadyRevertedTransaction = await walletServiceProvider.CancelTransaction(Id);
            string actual = revertAlreadyRevertedTransaction.Content;
            string errorMessage = $"Transaction '{Id}' cannot be reversed due to 'Reverted' current status";

            Assert.AreEqual(HttpStatusCode.InternalServerError, revertAlreadyRevertedTransaction.Status);
            Assert.AreEqual(actual, errorMessage);

        }




    }
}
