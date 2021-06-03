using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spiritkitchen
{
    public class Customer
    {
        //定义客户姓名
        public string customerName;
        //定义客户是否为会员
        public bool memberOption;
        //定义客户电话
        public string phoneNumber;

        public void SetMemberOption(bool option)
        {
            memberOption = option;
        }
    }
}
