﻿using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Repository.GenericRepository
{
    public class Pagination<T> 
    {

        public IReadOnlyList<T> Items { get; set; }


        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 3;
        public static int maxPageSize { get; set; } = 15;
        public int TotalCount { get; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber * PageSize < TotalCount;

       

        public  Pagination(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalCount , int totalPages)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = totalPages;
        }
        public static async Task<Pagination<T>> CreateAsync(IReadOnlyList<T> data, int? pageNumber=null , int? pageSize=null , int defaultPageNumber = 1, int defaultPageSize = 10)
        {
            var totalCount = data.Count;
            var page = pageNumber ?? defaultPageNumber;
            var size = pageSize ?? defaultPageSize;
            size = size> maxPageSize ? maxPageSize : size;
            var totalPages = (int)Math.Ceiling(totalCount / (double)size);
            var items = data.Skip((page - 1) * size).Take(size).ToList();

            return new Pagination<T>(items, page, size, totalCount , totalPages);
        }

        //public static async Task<Pagination<T>> CreateAsync1(IReadOnlyList<T> data, int? pageNumber, int? pageSize)
        //{
        //    var totalCount = data.Count;
        //    var page = pageNumber ?? PageNumber;
        //    var size = pageSize ?? PageSize;

        //    var items = data.Skip((page - 1) * size).Take(size).ToList();

        //    return new Pagination<T>(items, page, size, totalCount);
        //}
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
