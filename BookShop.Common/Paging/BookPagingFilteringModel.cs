using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Common.Paging
{
    public class BookPagingFilteringModel
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 6;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }    
        public string QueryParams { get; set; }       
    }
}
