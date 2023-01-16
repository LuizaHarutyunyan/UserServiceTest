
using NUnit.Framework;
using Task = System.Threading.Tasks.Task;
using System.Net;

using TechTalk.SpecFlow;
using UserServiceTest.DataContext;

namespace UserServiceTest.Steps.WalletServiceSteps
{
    [Binding]
    internal class WalletServiceAsserts
    {
        private WalletServiceDataContext _context;
        public WalletServiceAsserts(WalletServiceDataContext context)
        {
            _context = context;
        }

        [Then("Get balance status code is '(.*)'")]
        public async Task ThenGetBalanceResponseStatusIsOk(HttpStatusCode expectedStatusCode)
        {
            var getBalanaceResponse = _context.GetUserBalance;
            Assert.AreEqual(expectedStatusCode, getBalanaceResponse.Status);
        }
        [Then("User balance body is '([^']*)'")]
        public async Task ThenGetBalanceResponsebodyIs(string amount)
        {
            var getBalanaceResponse = _context.GetUserBalance;
            Assert.AreEqual(amount, getBalanaceResponse.Content);
        }
        [Then("Get balance error message is '([^']*)'")]
        public async Task ThenGetBalanceErrorMessage(string errorMessage)
        {
            var getBalanaceResponse = _context.GetUserBalance;
            Assert.AreEqual(errorMessage, getBalanaceResponse.Content);
        }
        [Then("Change balance status code is '(.*)'")]
        public async Task ThenChangeBalananceResponseStatusIs(HttpStatusCode expectedStatusCode)
        {
            var responseOfChangedBalance = _context.ChangeBalance;
            Assert.AreEqual(expectedStatusCode, responseOfChangedBalance.Status);
        }
        [Then("Change balance error message is '(.*)'")]
        public async Task ThenChangeBalananceErrorMessage(string errorMessage)
        {
            var responseOfChangedBalance = _context.ChangeBalance;
            Assert.AreEqual(errorMessage, responseOfChangedBalance.Content);
        }
       
        [Then("Change balance body is in guid formate")]
        public async Task ThenChangeBalananceBodyIs()
        {
            Model.NewFolder.CommonResponse<string> changeBalance = _context.ChangeBalance;
            
            Assert.AreEqual(changeBalance.Content.Length, 38);

        }
        [Then(@"Change balance with negative amount error message is 'User have '([^']*)', you try to charge '([^']*)''")]
        public void ThenChangeBalanceWithNegativeAmountWhenBalanceIsErrorMessageIs(string currentBalance, string changedBalance)
        {
            var responseOfChangedBalance = _context.ChangeBalance;
            Assert.AreEqual(responseOfChangedBalance.Content, $"User have '{currentBalance}', you try to charge '{changedBalance}'.");
           
        }
        [Then(@"Canceled transaction response status code is '([^']*)'")]
        public void ThenCanceledTransactionResponseStatusCodeIs(HttpStatusCode expectedStatusCode)
        {
            var revertTransactionResponse = _context.RevertTransaction;
            Assert.AreEqual(expectedStatusCode,revertTransactionResponse.Status);
            
        }
        [Then(@"Canceled transaction body is in guid formate")]
        public void ThenCanceledTransactionBodyIsInGuidFormate()
        {
            var revertTransactionResponse = _context.RevertTransaction;
            Assert.IsNotNull(revertTransactionResponse.Content);
        }
        [Then(@"Canceled transaction error message is '([^']*)'")]
        public void ThenCanceledTransactionErrorMessageIs(string errorMessage)
        {
            var revertTransactionResponse = _context.RevertTransaction;
            Assert.AreEqual(errorMessage, revertTransactionResponse.Content);
        }





    }
}
