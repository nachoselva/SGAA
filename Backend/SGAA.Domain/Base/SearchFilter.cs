namespace SGAA.Domain.Base
{
    using System;
    using System.Linq.Expressions;

    public class SearchFilter<T>
        where T : class, IEntity
    {
        const int MAX_ROWS_PER_PAGE = 1000;

        public IList<Expression<Func<T, bool>>> Filters { get; }
        public IList<SearchModelOrderBy<T>> OrderBy { get; }
        public int Page { get; }
        public int RowsByPage { get; }

        public SearchFilter()
        {
            Page = 1;
            RowsByPage = MAX_ROWS_PER_PAGE;
            Filters = new List<Expression<Func<T, bool>>>();
            OrderBy = new List<SearchModelOrderBy<T>>();
        }

        public SearchFilter<T> AddFilter(Expression<Func<T, bool>> filter)
        {
            Filters.Add(filter);
            return this;
        }

        public SearchFilter<T> AddOrderBy(OrderCriteria criteria, Expression<Func<T, object>> field)
        {
            OrderBy.Add(new SearchModelOrderBy<T>(criteria, field));
            return this;
        }
    }

    public class SearchModelOrderBy<T>
        where T : class, IEntity
    {
        public OrderCriteria Criteria { get; }
        public Expression<Func<T, object>> Field { get; }

        public SearchModelOrderBy(OrderCriteria criteria, Expression<Func<T, object>> field)
        {
            Criteria = criteria;
            Field = field;
        }
    }

    public enum OrderCriteria
    {
        Ascending,
        Descending
    }

    public static class DomainQueryableExtensions
    {
        public static IQueryable<T> ImplementSearchModel<T>(this IQueryable<T> queryable, SearchFilter<T> searchFilter)
            where T : class, IEntity
        {
            queryable = queryable.ImplementFilters(searchFilter.Filters);
            queryable = queryable.ImplementOrderBy(searchFilter.OrderBy);
            queryable = queryable.ImplementPagination(searchFilter.Page, searchFilter.RowsByPage);

            return queryable;
        }

        public static IQueryable<T> ImplementFilters<T>(this IQueryable<T> queryable, params Expression<Func<T, bool>>[] filters)
            where T : class, IEntity
        {
            foreach (var filter in filters)
            {
                queryable = queryable.Where(filter);
            }

            return queryable;
        }

        public static IQueryable<T> ImplementFilters<T>(this IQueryable<T> queryable, IList<Expression<Func<T, bool>>> filters)
            where T : class, IEntity
        {
            return queryable.ImplementFilters(filters.ToArray());
        }

        public static IQueryable<T> ImplementOrderBy<T>(this IQueryable<T> queryable, params SearchModelOrderBy<T>[] orderByCriterias)
            where T : class, IEntity
        {
            SearchModelOrderBy<T> firstOrderBy = orderByCriterias.First();
            if (firstOrderBy != null)
            {
                IOrderedQueryable<T> orderedQueryable;
                switch (firstOrderBy.Criteria)
                {
                    case OrderCriteria.Ascending:
                    default:
                        orderedQueryable = queryable.OrderBy(firstOrderBy.Field);
                        break;
                    case OrderCriteria.Descending:
                        orderedQueryable = queryable.OrderByDescending(firstOrderBy.Field);
                        break;
                }

                foreach (SearchModelOrderBy<T> orderBy in orderByCriterias.Skip(1))
                {
                    switch (orderBy.Criteria)
                    {
                        case OrderCriteria.Ascending:
                        default:
                            orderedQueryable = orderedQueryable.ThenBy(firstOrderBy.Field);
                            break;
                        case OrderCriteria.Descending:
                            orderedQueryable = orderedQueryable.ThenByDescending(firstOrderBy.Field);
                            break;
                    }
                }

                queryable = orderedQueryable;
            }

            return queryable;
        }

        public static IQueryable<T> ImplementOrderBy<T>(this IQueryable<T> queryable, IList<SearchModelOrderBy<T>> orderByCriterias)
            where T : class, IEntity
        {
            return queryable.ImplementOrderBy(orderByCriterias.ToArray());
        }

        public static IQueryable<T> ImplementPagination<T>(this IQueryable<T> queryable, int pageNumber, int rowsPerPage)
            where T : class, IEntity
        {
            if (pageNumber > 1)
            {
                queryable = queryable.Skip((pageNumber - 1) * rowsPerPage);
            }
            queryable = queryable.Take(rowsPerPage);
            return queryable;
        }
    }
}
