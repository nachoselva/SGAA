namespace SGAA.Utils
{
    using System;
    using System.Linq.Expressions;

    public static class ExpressionTools
    {

        public static Expression<Func<TInput, TNewOuput>> CastExpression<TInput, TCurrentOuput, TNewOuput>(this Expression<Func<TInput, TCurrentOuput>> expression)
            where TCurrentOuput : TNewOuput
        {
            Expression converted = Expression.Convert
                 (expression.Body, typeof(TNewOuput));

            return Expression.Lambda<Func<TInput, TNewOuput>>
                 (converted, expression.Parameters);
        }
    }
}
