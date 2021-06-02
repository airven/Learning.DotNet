using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spiritkitchen
{
    public class Dinner
    {
        public const string type = " 自助用餐";//定义一个常量来保存用餐类型
        public int numberofPeople;

        //定义一个整型字段用户存储用餐人数
        public const int costofFoodPerPerson = 35;//定义一个整型常量来保存人均食品费用
        public decimal costofBeveragesPerPerson;//定义一个十进制字段来保存人均饮料费用

        public Customer customer;
        //声明一个客户类对象来记录用餐客户信息
        public DateTime date;
        //定义一个日期对象
        private decimal cost;

        //定义一个总费用字段
        public Dinner(int numberofPeople)
        {
            //类的构造函数
            customer = new Customer();
            //在构造函数中创建一个对象的实例
            this.numberofPeople = numberofPeople;// 初始化用餐人数为5
        }

        //定义个带boo1类型参数的方法, 用于设置人均饮料费
        public void SetwineOption(bool wineOption)
        {
            if (wineOption)
            {
                costofBeveragesPerPerson = 40.00M;
            }
            else
                costofBeveragesPerPerson = 15.00M;
        }

        //定义了一个计算总费用的方法,该方法具有返回值,用于返回总费用
        public decimal Calculate()
        {
            //计算总费用
            decimal totalcost = (costofFoodPerPerson * costofBeveragesPerPerson) * numberofPeople;
            //当客户是会员时打85折
            if (customer.memberOption)
            {
                this.cost = totalcost * 0.85M;
                return totalcost * 0.85M;
            }
            else
            {
                this.cost = totalcost;
                return totalcost;
            }
        }


    }
}
