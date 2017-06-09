using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Common
{
    public class PagedData<T>
    {
        public List<T> Data { get; set; }
        public int TotalItems { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public PagedData()
        {

        }
        public PagedData(List<T> data, int totalItems, int currentPageIndex, int pageSize)
        {
            this.Data = data;
            this.TotalItems = totalItems;
            this.PageIndex = currentPageIndex;
            this.PageSize = pageSize;
        }
    }
}
