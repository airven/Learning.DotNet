/*
 *file:rulegine.cs
 *author:airven
 *creattime:2016/07/18
 */
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Learning.Code.LinqTest
{
    /// <summary>
    /// 规则引擎
    /// </summary>
    public class RuleEngine
    {
        /// <summary>
        /// 构建lambra表达式树
        /// </summary>
        private static Expression BuildExpr<T>(RuleModule r, ParameterExpression param)
        {
            var left = MemberExpression.Property(param, r.MemberName);
            var tProp = typeof(T).GetProperty(r.MemberName).PropertyType;
            ExpressionType tBinary;
            if (ExpressionType.TryParse(r.Operator, out tBinary))
            {
                var right = Expression.Constant(Convert.ChangeType(r.TargetValue, tProp));
                return Expression.MakeBinary(tBinary, left, right);
            }
            else
            {
                var method = tProp.GetMethod(r.Operator);
                var tParam = method.GetParameters()[0].ParameterType;
                var right = Expression.Constant(Convert.ChangeType(r.TargetValue, tParam));
                return Expression.Call(left, method, right);
            }
        }

        /// <summary>
        /// 匹配两种规则
        /// </summary>
        public static Func<T, bool> CompileRule<T>(RuleModule r)
        {
            var paramUser = Expression.Parameter(typeof(T));
            Expression expr = BuildExpr<T>(r, paramUser);
            return Expression.Lambda<Func<T, bool>>(expr, paramUser).Compile();
        }

        /// <summary>
        /// 匹配两种规则
        /// </summary>
        public static Func<T, bool> CompileRule<T>(RuleModule leftrule, RuleModule rightrule)
        {
            var paramUser = Expression.Parameter(typeof(T));
            Expression leftexpr = BuildExpr<T>(leftrule, paramUser);
            Expression rightexpr = BuildExpr<T>(rightrule, paramUser);
            Expression express = Expression.AndAlso(leftexpr, rightexpr);
            return Expression.Lambda<Func<T, bool>>(express, paramUser).Compile();
        }

        /// <summary>
        /// 匹配多种规则
        /// </summary>
        public static Func<T, bool> CompileMultiRule<T>(IList<RuleModule> rules)
        {
            Expression expression1 = null;
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            foreach (var rule in rules)
            {
                Expression expression = BuildExpr<T>(rule, parameter);
                if (rules.IndexOf(rule) == 0)
                    expression1 = expression;
                else
                    expression1 = Expression.AndAlso(expression1, expression);
            }
            return Expression.Lambda<Func<T, bool>>(expression1, parameter).Compile();
        }
    }
}
