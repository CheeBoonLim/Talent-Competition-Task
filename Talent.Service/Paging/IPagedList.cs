using System.Collections;
using System.Collections.Generic;

namespace Talent.Service.Paging
{
    public interface IPagedList : IEnumerable
    {
        int TotalCount { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }

    public interface IPagedList<T> : IPagedList
    {
        IEnumerable<T> Items { get; }
    }
}
