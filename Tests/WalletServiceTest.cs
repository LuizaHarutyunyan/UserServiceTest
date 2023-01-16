using NUnit.Framework;
using System.Net;
using UserServiceTest.Model.Extensions;
using UserServiceTest.Model.Request;
using UserServiceTest.Model.NewFolder;


namespace UserServiceTest.Tests
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
        public async Task GetUserBalance_ValidateGetBalanceResponseStatus_ResponceStatusIsOk()
        {
            //Action
            var getBalanaceResponse = await walletServiceProvider.GetBalance(_userId);
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, getBalanaceResponse.Status);

        }
        [Test]
        public async Task GetBalanceOfNewUser_ValidateGetBalanseResponseBody_ResponceStatusIsOk()
        {
            //Action
            var responseGetBalance = await walletServiceProvider.GetBalance(_userId);
            string body = responseGetBalance.Content;
            //Assert
            Assert.AreEqual(body, "0");

        }

        [Test]
        public async Task GetUserBalanceOfUnexistedUser_ValidateGetBalanseResponseStatusAndMessage_ResponceStatusIsInternalServerError()
        {
            //Precondition
            string userId = (int.Parse(_userId) + 999).ToString();
            //Action
            var responseGetBalance = await walletServiceProvider.GetBalance(userId);
            string actual = responseGetBalance.Content;
            string errorMessage = "not active user";
            //Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, responseGetBalance.Status);
            Assert.AreEqual(errorMessage, actual);

        }
        [Test]
        public async Task GetUserBalanceOfNotActiveUser_ValidateGetBalanseResponseStatusAndMessage_ResponceStatusIsInternalServerError()
        {
            //Precondition
            var changeUserStatusInActive = await userServiceProviders.ChangeUserStatus(_userId, false);
            //Action
            var responseGetBalance = await walletServiceProvider.GetBalance(_userId);
            string actual = responseGetBalance.Content;
            string errorMessage = "not active user";
            //Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, responseGetBalance.Status);
            Assert.AreEqual(actual, errorMessage);

        }

        [Test]
        public async Task GetBalanceAfterRevert_CheckResponseStatus_ResponseStatusIsOK()
        {
            //Precondition
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 300
            };

            CommonResponse<string> ChangeBalance = await walletServiceProvider.ChangeBalance(balance);
            string Id = ChangeBalance.Body;
            //Action
            CommonResponse<string> revertTransaction = await walletServiceProvider.CancelTransaction(Id);
            //Action
            CommonResponse<object> getBalance = await walletServiceProvider.GetBalance(_userId);
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, getBalance.Status);

        }

        [Test]
        public async Task GetBalanceBodyAfterRevert_CheckResponseBody_BalanceDecreaseWithChangedAmount()
        {
            //Precondition
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 300
            };
            CommonResponse<string> ChangeBalance = await walletServiceProvider.ChangeBalance(balance);
            string Id = ChangeBalance.Body;
            //Action
            CommonResponse<string> revertTransaction = await walletServiceProvider.CancelTransaction(Id);
            //Action
            CommonResponse<object> getBalance = await walletServiceProvider.GetBalance(_userId);
            //Assert
            Assert.AreEqual(getBalance.Content, "0.0");

        }
        [TestCase(0.01)]
        [TestCase(9999999.99)]
        [TestCase(10000000)]
        public async Task ChangeUserBalance_CheckResponseStatus_ResponceStatusIsOk(double sum)
        {
            //Action
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = sum
            };
            CommonResponse<string> responseOfChange = await walletServiceProvider.ChangeBalance(balance);
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, responseOfChange.Status);
        }

        [Test]
        public async Task ChangeUserBalanace_ChecResponsekBody_BodyIsIdInGuidFormat()
        {
            //Action
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 10
            };
            CommonResponse<string> responseOfChange = await walletServiceProvider.ChangeBalance(balance);
            string actual = responseOfChange.Content;
            //Assert
            Assert.AreEqual(actual.Length, 38);
        }
        [Test]
        public async Task ChangeBalanceOfUnExistedUser_ValidateChangeBalanceResponseStatusAndErrorMessage_ResponceStatusIsInternalServerError()
        {
            //Precondition
            string userId = (int.Parse(_userId) + 999).ToString();
            //Action
            BalanceChange balance = new BalanceChange
            {
                userId = userId,
                amount = 10
            };
            CommonResponse<string> response = await walletServiceProvider.ChangeBalance(balance);
            string actual = response.Content;
            string errorMessage = "not active user";
            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.InternalServerError, response.Status);
                Assert.AreEqual(errorMessage, actual);
            });

        }

        [TestCase(-0.01)]
        [TestCase(-10000000.01)]
        public async Task BalanceChangeWithNegativeAmountWhenBalanceIs0_ChangeUserBalanaceWithNegativeAmountCheckStatusAndMessage_ResponceStatusIsInternalServerError(double sum)
        {

            //Precondition
            var responseGetBalance = await walletServiceProvider.GetBalance(_userId);
            //Action
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = sum
            };
            CommonResponse<string> response = await walletServiceProvider.ChangeBalance(balance);
            string actual = response.Content;
            string currentBalance = responseGetBalance.Content;
            string errorMessage = $"User have '{currentBalance}', you try to charge '{balance.amount}'.";
            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.InternalServerError, response.Status);
                Assert.AreEqual(errorMessage, actual);
            });



        }
        [Test]
        public async Task ChangeBalanceWithNegativeAmountBiggerThanBalance_ChangeUserBalanaceWithNegativeAmountCheckStatusAndMessage_ResponceStatusIsInternalServerError()
        {
            //Precondition
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 50
            };

            var responseMessage = await walletServiceProvider.ChangeBalance(balance);
            //Action
            BalanceChange changedBalance = new BalanceChange
            {
                userId = _userId,
                amount = -1000
            };
            CommonResponse<string> response = await walletServiceProvider.ChangeBalance(changedBalance);
            string actual = response.Content;
            string errorMessage = $"User have '{balance.amount}.0', you try to charge '{changedBalance.amount}.0'.";
            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.InternalServerError, response.Status);
                Assert.AreEqual(errorMessage, actual);
            });


        }
        [Test]
        public async Task ChangeBalanceWithZeroAmount_CheckResponseStatusAndErrorMessage_ResponceStatusIsInternalServerError()
        {
            //Action
            BalanceChange amount = new BalanceChange
            {
                userId = _userId,
                amount = 0
            };
            CommonResponse<string> response = await walletServiceProvider.ChangeBalance(amount);
            string actual = response.Content;
            string errorMessage = "Amount cannot be '0'";
            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.InternalServerError, response.Status);
                Assert.AreEqual(actual, errorMessage);

            });


        }

        [Test]
        public async Task BalancePlusAmountBiggerThan10m_CheckResponseStatusAndErrorMessage_ResponceStatusIsInternalServerError()
        {
            //Precondition
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 5000000
            };
            var responseMessage = await walletServiceProvider.ChangeBalance(balance);
            //Action
            BalanceChange changedBalance = new BalanceChange
            {
                userId = _userId,
                amount = 5000001
            };
            var responseMessageChangedBaalance = await walletServiceProvider.ChangeBalance(changedBalance);
            string actual = responseMessageChangedBaalance.Content;
            string errorMessage = $"After this charge balance could be '{changedBalance.amount + balance.amount}.0', maximum user balance is '10000000'";
            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.InternalServerError, responseMessageChangedBaalance.Status);
                Assert.AreEqual(errorMessage, actual);
            });




        }

        [TestCase(500000, 500000)]
        [TestCase(499999, 500000)]
        public async Task BalancePlusAmountSmallerOrEqual10m_CheckResponseStatus_ResponceStatusIsOk(double firstAmount, double secondAmount)
        {
            //Precondition
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = firstAmount
            };
            var responseMessage = await walletServiceProvider.ChangeBalance(balance);
            //Action
            BalanceChange changedBalance = new BalanceChange
            {
                userId = _userId,
                amount = secondAmount
            };
            var responseMessageChangedBaalance = await walletServiceProvider.ChangeBalance(changedBalance);
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, responseMessageChangedBaalance.Status);

        }



        [TestCase(0.1, 0.001)]
        [TestCase(5.78, 6.325)]
        public async Task AmountValueAfterDotMustHavePrecisionTwoNumbers_CheckCheckResponseStatusAndErrorMessage_ResponceStatusIsInternalServerError(double firstAmount, double secondAmount)
        {
            //Precondition
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = firstAmount
            };
            var responseMessage = await walletServiceProvider.ChangeBalance(balance);
            //Action
            BalanceChange changedBalance = new BalanceChange
            {
                userId = _userId,
                amount = secondAmount
            };
            var responseMessageChangedBaalance = await walletServiceProvider.ChangeBalance(changedBalance);
            string actual = responseMessageChangedBaalance.Content;
            string errorMessage = "Amount value must have precision 2 numbers after dot";
            //Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, responseMessageChangedBaalance.Status);
            Assert.AreEqual(errorMessage, actual);


        }

        [TestCase(0.01)]
        [TestCase(10000000)]
        [TestCase(999999.99)]

        public async Task RevertTransactionWithPositiveAmounts_CheckResponseStatus_ResponseStatusIsOk(double sum)
        {
            //Precondition
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = sum
            };
            CommonResponse<string> ChangeBalance = await walletServiceProvider.ChangeBalance(balance);
            string Id = ChangeBalance.Body;
            //Action
            CommonResponse<string> revertTransaction = await walletServiceProvider.CancelTransaction(Id);
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, revertTransaction.Status);
        }
        [TestCase(-0.01)]
        [TestCase(-10000000.01)]
        public async Task RevertTransactionWithNegativeAmounts_CheckResponseStatus_ResponseStatusIsBadRequest(double sum)
        {
            //Precondition
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = sum
            };
            CommonResponse<string> ChangeBalance = await walletServiceProvider.ChangeBalance(balance);
            string Id = ChangeBalance.Body;
            //Action
            CommonResponse<string> revertTransaction = await walletServiceProvider.CancelTransaction(Id);
            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, revertTransaction.Status);
        }

        [Test]
        public async Task RevertTransaction_CheckNewlyCreatedTransactionBody_BodyIsIDInGuidFormat()
        {
            //Precondition
            BalanceChange balance = new BalanceChange
            {
                userId = _userId,
                amount = 300
            };
            CommonResponse<string> ChangeBalance = await walletServiceProvider.ChangeBalance(balance);
            string id = ChangeBalance.Content;
            //Action
            CommonResponse<string> revertTransaction = await walletServiceProvider.CancelTransaction(id);
            //Assert
            Assert.IsNotNull(revertTransaction);
        }

        [Test]
        public async Task RevertTransactionWithWrongId_CheckResponseStatusAndErrorMessage_ResponseStatusIsNotFound()
        {
            //Precondition
            Guid randomId = new Guid();
            //Action
            CommonResponse<string> revertTransaction = await walletServiceProvider.CancelTransaction(randomId.ToString());
            string actual = revertTransaction.Content;
            string errorMessage = "The given key was not present in the dictionary.";
            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.NotFound, revertTransaction.Status);
                Assert.AreEqual(actual, errorMessage);
            });

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
