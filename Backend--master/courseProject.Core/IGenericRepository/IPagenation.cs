using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IPagenation<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int maxPageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; }
        // public bool HasPreviousPage {  get; set; }
        //  public bool HasNextPage { get; set; }


      //  public  Task<IReadOnlyList<T>> CreateAsync(IQueryable<T> data, int? pageNumber, int? pageSize);
        //Task<IReadOnlyList<T>> ApplayPagination(IQueryable<T>? data, int? pageNumber, int? pageSize, int count);
        // Task<IEnumerable<T>> ApplayPaginationIEnumerable(IQueryable<T> data, int? pageNumber, int count);
    }
}
