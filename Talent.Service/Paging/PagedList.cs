using System;
using System.Collections.Generic;
using System.Linq;

namespace Talent.Service.Paging
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList()
        {
        }

        /// <summary>
        /// Populate PagedList from complete list of all entities
        /// </summary>
        /// <param name="source">List of all entities, from which one page will be taken</param>
        /// <param name="pageIndex">The index of the page to take</param>
        /// <param name="pageSize">The number of entities to show on one page</param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            TotalCount = source.Count();
            PageSize = pageSize;
            PageIndex = Math.Max(pageIndex, 1);

            AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// Populate PagedList from complete list of all entities
        /// </summary>
        /// <param name="source">List of all entities, from which one page will be taken</param>
        /// <param name="pageIndex">The index of the page to take</param>
        /// <param name="pageSize">The number of entities to show on one page</param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (pageSize <= 0) throw new ArgumentException(string.Format("pageSize must be greater than zero (was {0})", pageSize));

            // We only want to enumerate the source once.
            var sourceList = source;
            TotalCount = sourceList.Count();
            PageSize = pageSize;
            PageIndex = Math.Max(pageIndex, 1);

            AddRange(sourceList.Skip((PageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// Populate PagedList from a pre-selected single page of entities
        /// </summary>
        /// <param name="source">One page of entities</param>
        /// <param name="pageIndex">The index of the page to take</param>
        /// <param name="pageSize">The maximum number of entities to show on one page</param>
        /// <param name="totalCount">The number of pages in total</param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            if (pageSize <= 0) throw new ArgumentException(string.Format("pageSize must be greater than zero (was {0})", pageSize));

            // We only want to enumerate the source once.
            var sourceList = source.ToList();
            TotalCount = totalCount;
            PageSize = pageSize;
            PageIndex = Math.Max(pageIndex, 1);

            AddRange(sourceList);
        }

        public int TotalCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex * PageSize) < TotalCount;
            }
        }

        public IEnumerable<T> Items { get { return this; } }

        public static PagedList<T> FromExisting(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            if (pageSize <= 0) throw new ArgumentException(string.Format("pageSize must be greater than zero (was {0})", pageSize));

            var list = new PagedList<T>()
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                PageIndex = Math.Max(pageIndex, 1)
            };
            list.AddRange(source);
            return list;
        }

        public object Extended()
        {
            return new
            {
                TotalCount,
                PageSize,
                PageIndex,
                Items = Items.ToArray()
            };
        }
    }
}
