using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spiritkitchen
{
    public class PartyDinner : Dinner
    {
        public new const string type = "宴会用餐";
        public PartyDinner(int numberofPeople) : base(numberofPeople)
        {

        }
        //定义一个十进制字段来保存人均装饰费用
        public decimal costOfDecorationsPerPerson;

        public void SetFinceDecorationsOption(bool decorationsOption)
        {
            if (decorationsOption)
            {
                costOfDecorationsPerPerson = 40.00M;
            }
            else
                costOfDecorationsPerPerson = 20.00M;
        }

        public override decimal Calculate()
        {
            //计算不打折之前总费用
            decimal totalCost = (costofFoodPerPerson + costofBeveragesPerPerson + costOfDecorationsPerPerson) * NumberofPeople;
            if (customer.memberOption)
            {
                //当客户是会员时打85折
                this.Cost = totalCost * 0.85M;
                return totalCost * 0.85M;
            }
            else
            {
                this.Cost = totalCost;
                return totalCost;
            }
        }
    }
}
