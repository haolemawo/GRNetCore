using System;
using System.Collections.Generic;

namespace GR.Core
{
    /// <summary>
    /// Paged list interface
    /// </summary>
    /// <remarks>
    /// 此处代码来自nopcommerce 
    /// https://github.com/nopSolutions/nopCommerce
    /// </remarks>
    public interface IPagedList<T> : IList<T>
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
