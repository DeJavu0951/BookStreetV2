﻿using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace Application.Extentions
{
    public static class DatatableExtensions
    {
        /// <summary>
        /// Hàm chuyển đổi kết quả từ IQueryable thành DataResult để phục vụ cho DataTables
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static DataResult<T> ToDataResult<T>(this IQueryable<T> query, DataRequest request, Guid? branchId = null) where T : class
        {

            var result = new DataResult<T>();
            if (request.Limit != -1)
            {
                if (request.Limit > DataRequest.MaxPageSize)
                {
                    request.Limit = DataRequest.MaxPageSize;
                }

                if (request.Limit <= 0 && request.Limit != -1)
                {
                    request.Limit = 10;
                }
            }

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            int start = (request.Page - 1) * request.Limit;
            int length = request.Limit;


            var branchProperty = GetPropertyByField<T>("BranchId");

            if (branchProperty != null && branchId != null)
            {
                ParameterExpression param = Expression.Parameter(typeof(T), "t");
                MemberExpression member = Expression.Property(param, branchProperty);
                var operand = Operand.Equal;
                var exp = ExpressionBuilder.GetExpression<T>(new FilterDefinition { Operand = operand, Field = branchProperty, Value = branchId.ToString() });
                if (exp != null) query = query.Where(exp);
            }

            // Xác định thuộc tính khóa chính của kiểu T
            //var key = typeof(T).GetProperties().FirstOrDefault(a => a.CustomAttributes.Any(b => b.AttributeType == typeof(System.ComponentModel.DataAnnotations.KeyAttribute)));
            result.RecordsTotal = result.RecordsFiltered = query.Count();

            foreach (var item in request.Filters)
            {
                // Lấy biểu thức tương ứng với điều kiện tìm kiếm
                var exp = GetExpression<T>((Operand)item.Operand, item.Field, item.Value);
                if (exp != null) query = query.Where(exp);
            }

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                Expression<Func<T, bool>>? exp = null;
                var listExp = new List<FilterDefinition>();

                foreach (var item in request.Columns.Where(a => a.Searchable))
                {
                    // Tạo biểu thức tương ứng với từng cột có thể tìm kiếm
                    ParameterExpression param = Expression.Parameter(typeof(T), "t");
                    item.Field = GetPropertyByField<T>(item.Field);
                    MemberExpression member = Expression.Property(param, item.Field);
                    var operand = member.Type == typeof(string) ? Operand.Contains : Operand.Equal;
                    listExp.Add(new FilterDefinition { Operand = operand, Field = item.Field, Value = request.Search.Value });
                }

                // Kết hợp các biểu thức tìm kiếm theo chuỗi hoặc
                exp = ExpressionBuilder.GetExpression<T>(listExp);
                if (exp != null) query = query.Where(exp);
            }

            if (!string.IsNullOrEmpty(request.Search?.Value) || request.Filters.Any())
            {
                result.RecordsFiltered = query.Count();
            }

            // Tính tổng của thuộc tính tùy chỉnh
            if (request.TotalColumns != null && request.TotalColumns.Any())
            {
                result.TotalColumns = new Dictionary<string, decimal>();
                foreach (var item in request.TotalColumns)
                {
                    result.TotalColumns.Add(item, query.SumCreate(item));
                }
            }

            // nếu có GroupByColumns thì thực hiện GroupBy
            if (request.GroupSumColumns != null && request.GroupSumColumns.Any())
            {
                // group by sum entity framework c#
                // GroupSumColumns is dictionary with key is multiple field and value is multiple field
                // ex: key = "Name,Code", value = "Total"

                var keySelector = request.GroupSumColumns.Keys.Distinct().ToArray();
                var valueSelector = string.Join(", ", request.GroupSumColumns.Values.Distinct());

                var lambda = GroupByExpression<T>(keySelector);
                var currentItemFields = query.GroupBy(lambda.Compile());

                // sum multiple field
                var resultFields = currentItemFields.Select(a => new
                {
                    Key = a.Key,
                    Value = a.Sum(b => (decimal)typeof(T).GetProperty(valueSelector).GetValue(b))
                });

                result.TotalGroupColumns = new Dictionary<string, object>();
                foreach (var item in resultFields)
                {
                    var key = string.Join(",", item.Key);
                    key = key.Replace("(", "").Replace(")", "");
                    result.TotalGroupColumns.Add(key, item.Value);
                }
            }


            if (!request.Orders.Any())
            {
                // check xem có model có modified date không
                var modifiedField = typeof(T).GetProperties().FirstOrDefault(a => a.Name.ToLower() == "modified");
                if (modifiedField != null)
                {
                    query = query.OrderByDescending("Modified");
                }

                var modifiedAtField = typeof(T).GetProperties().FirstOrDefault(a => a.Name.ToLower() == "modifiedat");
                if (modifiedAtField != null)
                {
                    query = query.OrderByDescending("ModifiedAt");
                }
            }
            else
            {
                string orderByClause = string.Join(",", request.Orders.Select(item => $"{item.Field} {item.Dir}"));
                // xử lý tạo query order động
                query = DynamicQueryable.OrderBy(query, orderByClause);
            }

            //query = query.Skip(request.Start).Take(request.Length);
            if (request.Limit != -1)
            {
                query = query.Skip(start).Take(request.Limit);
            }

            result.List = query.ToList();

            if (request.Limit == -1)
            {
                request.Limit = query.Count();
            }

            // tính lại Pagination sau khi đã lọc dữ liệu
            result.Pagination = new Pagination
            {
                Size = request.Limit,
                Page = request.Page,
                Total = result.RecordsFiltered
            };

            return result;
        }

        /// <summary>
        /// Lấy ra property chuẩn của kiểu T dựa trên tên field (upper case, lower case)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetPropertyByField<T>(string field)
        {
            return typeof(T).GetProperties().FirstOrDefault(a => a.Name.ToLower() == field.ToLower())?.Name ?? field;
        }

        /// <summary>
        /// Hàm tạo biểu thức cho điều kiện tìm kiếm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operand"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>>? GetExpression<T>(Operand operand, string field, string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            field = GetPropertyByField<T>(field);
            return ExpressionBuilder.GetExpression<T>(new FilterDefinition
            {
                Operand = operand,
                Field = field,
                Value = value
            });
        }

        /// <summary>
        /// Hàm sắp xếp theo tên thuộc tính
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string memberName)
        {
            return OrderByCreate(query, memberName, "OrderBy");
        }

        /// <summary>
        /// Hàm sắp xếp theo tên thuộc tính
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        private static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string memberName)
        {
            return OrderByCreate(query, memberName, "OrderByDescending");
        }

        /// <summary>
        /// Hàm sắp xếp theo tên thuộc tính
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="memberName"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private static IOrderedQueryable<T> OrderByCreate<T>(this IQueryable<T> query, string memberName, string methodName)
        {
            var typeParams = new ParameterExpression[] { Expression.Parameter(typeof(T), "") };
            var pi = typeof(T).GetProperty(GetPropertyByField<T>(memberName));

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new Type[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams)
                )
            );
        }

        /// <summary>
        /// Hàm tính tổng của thuộc tính tùy chỉnh
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        private static decimal SumCreate<T>(this IQueryable<T> query, string memberName)
        {
            var typeParams = new ParameterExpression[] { Expression.Parameter(typeof(T), "") };
            var pi = typeof(T).GetProperty(GetPropertyByField<T>(memberName));


            var sumProperty = Expression.Lambda<Func<T, decimal>>(Expression.Property(typeParams[0], pi), typeParams);

            return query.Sum(sumProperty);
        }

        public static Expression<Func<TItem, object>> GroupByExpression<TItem>(string[] propertyNames)
        {
            var properties = propertyNames.Select(name => typeof(TItem).GetProperty(name)).ToArray();
            var propertyTypes = properties.Select(p => p.PropertyType).ToArray();
            var tupleTypeDefinition = typeof(Tuple).Assembly.GetType("System.Tuple`" + properties.Length);
            var tupleType = tupleTypeDefinition.MakeGenericType(propertyTypes);
            var constructor = tupleType.GetConstructor(propertyTypes);
            var param = Expression.Parameter(typeof(TItem), "item");
            var body = Expression.New(constructor, properties.Select(p => Expression.Property(param, p)));
            var expr = Expression.Lambda<Func<TItem, object>>(body, param);
            return expr;
        }
    }
}
