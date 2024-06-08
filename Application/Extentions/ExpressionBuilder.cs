using System.Linq.Expressions;
using System.Reflection;

namespace Application.Extentions
{
    public class ExpressionBuilder
    {
        private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
        private static readonly MethodInfo StartsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static readonly MethodInfo EndsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

        /// <summary>
        /// Builds a dynamic Linq expression based on a FilterDefinition for a specific data type.
        /// </summary>
        public static Expression<Func<T, bool>>? GetExpression<T>(FilterDefinition filter)
        {
            if (filter == null) return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");

            var exp = GetExpression<T>(param, filter);
            if (exp == null) return null;

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        /// <summary>
        /// Builds a dynamic Linq expression based on a FilterDefinition and a parameter for a specific data type.
        /// </summary>
        private static Expression? GetExpression<T>(ParameterExpression param, FilterDefinition filter)
        {
            MemberExpression member = Expression.Property(param, filter.Field);
            MemberExpression? hasValueExpression = null;
            //BinaryExpression? equalExpression = null;
            Expression leftExpresstion = null;
            var underlyingType = Nullable.GetUnderlyingType(member.Type);
            TypeCode type = Type.GetTypeCode(underlyingType ?? member.Type);
            var cType = ConvertDynamic(type, filter.Value);
            if (cType == null) return null;

            ConstantExpression constant = Expression.Constant(cType);

            if (member.Type == typeof(Guid) || member.Type == typeof(Guid?))
            {
                // Convert the string constant to Guid
                constant = Expression.Constant(Guid.Parse(constant.Value.ToString()));
            }


            if (underlyingType != null)
            {
                hasValueExpression = Expression.Property(member, "HasValue");
                leftExpresstion = Expression.Property(member, "Value");
            }

            switch (filter.Operand)
            {
                case Operand.Equal:
                    return underlyingType != null ? Expression.AndAlso(hasValueExpression, Expression.Equal(leftExpresstion, constant)) : Expression.Equal(member, constant);

                case Operand.NotEqual:
                    return underlyingType != null ? Expression.AndAlso(hasValueExpression, Expression.NotEqual(leftExpresstion, constant)) : Expression.NotEqual(member, constant);

                case Operand.GreaterThan:
                    return underlyingType != null ? Expression.AndAlso(hasValueExpression, Expression.GreaterThan(leftExpresstion, constant)) : Expression.GreaterThan(member, constant);

                case Operand.GreaterThanOrEqual:
                    return underlyingType != null ? Expression.AndAlso(hasValueExpression, Expression.GreaterThanOrEqual(leftExpresstion, constant)) : Expression.GreaterThanOrEqual(member, constant);

                case Operand.LessThan:
                    return underlyingType != null ? Expression.AndAlso(hasValueExpression, Expression.LessThan(leftExpresstion, constant)) : Expression.LessThan(member, constant);

                case Operand.LessThanOrEqual:
                    return underlyingType != null ? Expression.AndAlso(hasValueExpression, Expression.LessThanOrEqual(leftExpresstion, constant)) : Expression.LessThanOrEqual(member, constant);

                case Operand.Contains:
                    return underlyingType != null ? Expression.AndAlso(hasValueExpression, Expression.Call(leftExpresstion, ContainsMethod, constant)) : Expression.Call(member, ContainsMethod, constant);

                case Operand.DoesNotContain:
                    return underlyingType != null ? Expression.AndAlso(hasValueExpression, Expression.Not(Expression.Call(leftExpresstion, ContainsMethod, constant))) : Expression.Not(Expression.Call(member, ContainsMethod, constant));

                case Operand.StartsWith:
                    return underlyingType != null ? Expression.AndAlso(hasValueExpression, Expression.Call(leftExpresstion, StartsWithMethod, constant)) : Expression.Call(member, StartsWithMethod, constant);

                case Operand.EndsWith:
                    return underlyingType != null ? Expression.AndAlso(hasValueExpression, Expression.Call(leftExpresstion, EndsWithMethod, constant)) : Expression.Call(member, EndsWithMethod, constant);

                case Operand.IsEmpty:
                    return underlyingType != null ? Expression.AndAlso(hasValueExpression, Expression.Equal(leftExpresstion, Expression.Constant(string.Empty))) : Expression.Equal(member, Expression.Constant(string.Empty));

                case Operand.IsNotEmpty:
                    return underlyingType != null ? Expression.AndAlso(hasValueExpression, Expression.NotEqual(leftExpresstion, Expression.Constant(string.Empty))) : Expression.NotEqual(member, Expression.Constant(string.Empty));
            }
            return null;
        }

        /// <summary>
        /// Converts a string value to a dynamically determined data type based on the specified TypeCode.
        /// </summary>
        /// <param name="code">The TypeCode representing the target data type.</param>
        /// <param name="value">The string value to be converted.</param>
        /// <returns>The converted object of the dynamically determined data type, or null if the conversion fails.</returns>
        private static object? ConvertDynamic(TypeCode code, string value)
        {
            try
            {
                switch (code)
                {
                    case TypeCode.Empty:
                        return string.Empty;
                    case TypeCode.Object:
                        return value;
                    case TypeCode.Boolean:
                        return Convert.ToBoolean(value);
                    case TypeCode.Char:
                        return Convert.ToChar(value);
                    case TypeCode.SByte:
                        return Convert.ToSByte(value);
                    case TypeCode.Byte:
                        return Convert.ToByte(value);
                    case TypeCode.Int16:
                        return Convert.ToInt16(value);
                    case TypeCode.UInt16:
                        return Convert.ToUInt16(value);
                    case TypeCode.Int32:
                        return Convert.ToInt32(value);
                    case TypeCode.UInt32:
                        return Convert.ToUInt32(value);
                    case TypeCode.Int64:
                        return Convert.ToInt64(value);
                    case TypeCode.UInt64:
                        return Convert.ToUInt64(value);
                    case TypeCode.Single:
                        return Convert.ToSingle(value);
                    case TypeCode.Double:
                        return Convert.ToDouble(value);
                    case TypeCode.Decimal:
                        return Convert.ToDecimal(value);
                    case TypeCode.DateTime:
                        return Convert.ToDateTime(value);
                    case TypeCode.String:
                        return value;
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Builds a dynamic Linq expression that combines multiple filter conditions based on a list of FilterDefinition objects for a specific data type.
        /// </summary>
        /// <typeparam name="T">The data type for which the Linq expression is generated.</typeparam>
        /// <param name="filters">A list of FilterDefinition objects, each representing a filter condition to be combined.</param>
        /// <returns>
        /// An Expression<Func<T, bool>> that represents a dynamic Linq expression combining the filter conditions.
        /// Returns null if the list of filters is empty.
        /// </returns>
        public static Expression<Func<T, bool>>? GetExpression<T>(IList<FilterDefinition> filters)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression? exp = null;

            foreach (var filter in filters)
            {
                var expin = GetExpression<T>(param, filter);
                if (expin != null)
                {
                    if (exp == null)
                        exp = expin;
                    else
                        exp = Expression.Or(exp, expin);
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }
    }

    public class FilterDefinition
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public Operand Operand { get; set; }
    }

    public enum Operand
    {
        Equal = 0,
        NotEqual = 1,
        GreaterThan = 2,
        GreaterThanOrEqual = 3,
        LessThan = 4,
        LessThanOrEqual = 5,
        Contains = 6,
        DoesNotContain = 7,
        StartsWith = 8,
        EndsWith = 9,
        IsEmpty = 10,
        IsNotEmpty = 11,
    }
}
