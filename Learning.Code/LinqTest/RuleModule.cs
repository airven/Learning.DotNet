using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Learning.Code.LinqTest
{
    public class RuleModule
    {
        /// <summary>
        /// 规则名称
        /// </summary>
        public string MemberName
        {
            get;
            set;
        }
        /// <summary>
        /// 规则运用的流程
        /// </summary>
        public string Operator
        {
            get;
            set;
        }
        /// <summary>
        /// 响应规则的阀值
        /// </summary>
        public string TargetValue
        {
            get;
            set;
        }
        public RuleModule()
        {

        }
        public RuleModule(string MemberName, string Operator, string TargetValue)
        {
            this.MemberName = MemberName;
            this.Operator = Operator;
            this.TargetValue = TargetValue;
        }
    }

}
