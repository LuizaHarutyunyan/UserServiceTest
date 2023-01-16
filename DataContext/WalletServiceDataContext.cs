using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UserServiceTest.Model.NewFolder;

namespace UserServiceTest.DataContext
{

    internal class WalletServiceDataContext
    {
        public string UserId;
        public string NotExistedUserId;
        public string TransactionId;
        public Guid RandomId;
        public CommonResponse<string> RevertTransaction;
        public CommonResponse<object> GetUserBalance;
        public CommonResponse<string> ChangeBalance;
        public CommonResponse<object> CreateUserResponse;
        public CommonResponse<UserResponseBody> ChangeUserStatusResponse;


    }
}
