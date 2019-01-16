using System.Collections.Generic;
using System.Linq;

namespace Talent.Service.Paging
{
    public static class Pagination
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index, int pageSize = 10)
        {
            return new PagedList<T>(source, index, pageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int index, int pageSize = 10)
        {
            return new PagedList<T>(source, index, pageSize);
        }

        public static PagedList<T> ToPagedListFromPreSelectedList<T>(this IEnumerable<T> source, int index, int pageSize, int totalCount)
        {
            return new PagedList<T>(source, index, pageSize, totalCount);
        }
    }
}
