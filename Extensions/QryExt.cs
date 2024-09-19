using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using uni_cap_pro_be.Core.QueryParameter;

namespace uni_cap_pro_be.Extensions
{
	public static class QueryableExtension
	{
		public static QueryParameterResult<T> ApplyQueryParameters<T>(
			this IQueryable<T> query,
			QueryParameters parameters
		)
		{
			// Filtering
			if (!string.IsNullOrEmpty(parameters.Filter))
			{
				query = query.Where(parameters.Filter);
			}

			// Sorting
			if (!string.IsNullOrEmpty(parameters.SortBy))
			{
				var sortExpression = $"{parameters.SortBy} {parameters.SortOrder}";
				query = query.OrderBy(sortExpression);
			}

			// Get the total count before applying paging
			var totalRecords = query.Count();

			// Paging
			query = query
				.Skip((parameters.Page - 1) * parameters.PageSize)
				.Take(parameters.PageSize);

			// Field Selection using Expression Trees
			if (!string.IsNullOrEmpty(parameters.SelectFields))
			{
				try
				{
					var properties = parameters
						.SelectFields.Split(',')
						.Select(x => char.ToUpper(x[0]) + x.Substring(1));
					var parameter = Expression.Parameter(typeof(T), "x");
					var bindings = properties.Select(prop =>
						Expression.Bind(
							typeof(T).GetProperty(prop.Trim()),
							Expression.PropertyOrField(parameter, prop.Trim())
						)
					);
					var selector = Expression.Lambda<Func<T, T>>(
						Expression.MemberInit(Expression.New(typeof(T)), bindings),
						parameter
					);
					query = query.Select(selector);
				}
				catch { }
			}

			return new QueryParameterResult<T>(
				query,
				parameters.Page,
				parameters.PageSize,
				totalRecords
			);
		}
	}
}
