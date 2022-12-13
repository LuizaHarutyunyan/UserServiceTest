using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServiceTest.Model.Request
{
    internal class BalanceChange
    {
        public string userId { get; set; }
        public double amount { get; set; }
    }
}
