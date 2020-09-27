using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentityServer.Infra.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }
        public static Pagelist<T> PageBy<T, TKey>(this IQueryable<T> query, PageInputDto page, Expression<Func<T, TKey>> orderBy)
        {
            var pagelist = new Pagelist<T>();
            var result = new List<T>();
            if (page.PageSize <= 0)
            {
                if (page.Isdesc)
                {
                    result = query.OrderByDescending(orderBy).ToList();
                }
                else
                {
                    result = query.ToList();
                }

            }
            else
            {
                if (page.Isdesc)
                {
                    result = query.OrderByDescending(orderBy).Skip(page.PageSize * (page.Page - 1)).Take(page.PageSize).ToList();

                }
                else
                {
                    result = query.Skip(page.PageSize * (page.Page - 1)).Take(page.PageSize).ToList();
                }
            }

            pagelist.Data.AddRange(result);
            pagelist.TotalCount = query.Count();
            pagelist.PageSize = page.PageSize;
            return pagelist;
        }
        public static Pagelist<T> PageBy<T, TKey>(this IEnumerable<T> query, PageInputDto page, Func<T, TKey> orderBy)
        {
            var pagelist = new Pagelist<T>();
            var result = new List<T>();
            if (page.PageSize <= 0)
            {
                if (page.Isdesc)
                {
                    result = query.OrderByDescending(orderBy).ToList();
                }
                else
                {
                    result = query.ToList();
                }

            }
            else
            {
                if (page.Isdesc)
                {
                    result = query.OrderByDescending(orderBy).Skip(page.PageSize * (page.Page - 1)).Take(page.PageSize).ToList();

                }
                else
                {
                    result = query.Skip(page.PageSize * (page.Page - 1)).Take(page.PageSize).ToList();
                }
            }


            pagelist.Data.AddRange(result);
            pagelist.TotalCount = query.Count();
            pagelist.PageSize = page.PageSize;
            return pagelist;
        }
    }
}
