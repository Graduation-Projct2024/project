using courseProject.Core.IGenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Repository.GenericRepository
{
    public class Pagenation<T> 
    {
       
            public List<T> Items { get; set; }
          
            
        public int PageNumber { get; set; } = 1;
    
        public int PageSize { get; set; } = 3;
        public int maxPageSize { get; set; } = 5;
        public int TotalCount { get; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber*PageSize < TotalCount;



        public  Pagenation(List<T> items, int pageNumber,int pafeSize, int totalCount)
        {
            Items = items;
             PageNumber = pageNumber;
            TotalCount = totalCount;
            PageSize = pafeSize;
        }



        public static async Task<Pagenation<T>> CreateAsync(IEnumerable<T> data, int pageNumber, int pageSize)
        {
            var totalCount =  data.Count();
           // var page = pageNumber ?? PageNumber;
           // var size = pageSize ?? PageSize;
            var items =  data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return  new(items, pageNumber, pageSize, totalCount);
        }


        //public async Task<IReadOnlyList<T>> ApplayPagination(IQueryable<T>? data, int? pageNumber, int? pageSize, int count)
        //{
        //    PageNumber = pageNumber ?? PageNumber;
        //    TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        //    var size= pageSize ?? DefaultPageSize;
        //    var PageSize = size > maxPageSize ? maxPageSize : size;

        //    return await data.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
        //}


    }
}
