using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServiceTest.Model.NewFolder;

namespace UserServiceTest.DataContext
{
    internal class UserServiceDataContext
    {
        public string UserId
        {
            get { return UserIdCollection.Last(); }
            set { UserIdCollection.Add(value); }
        }
        public List<string> UserIdCollection = new List<string>();
        public string NotExistedUserId;
        public CommonResponse<object> CreateUserResponse;
        public CommonResponse<UserResponseBody> DeleteUserResponse;
        public CommonResponse<UserResponseBody> ChangeUserStatusResponse;
        public CommonResponse<bool> GetUserStatusResponse;
    }
}
