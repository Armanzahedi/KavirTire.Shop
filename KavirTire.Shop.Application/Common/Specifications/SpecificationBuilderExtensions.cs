using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using Ardalis.Specification;

namespace KavirTire.Shop.Application.Common.Specifications;


    public static class SpecificationBuilderExtensions
    {
        public static ISpecificationBuilder<T> Paginate<T>(this ISpecificationBuilder<T> query,
            int pageNumber,int pageSize)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            if (pageNumber > 1)
            {
                query = query.Skip((pageNumber - 1) * pageSize);
            }

            return query
                .Take(pageSize);
        }
    }
