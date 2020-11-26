using System;
using System.Collections.Generic;
using System.Text;

namespace KAPIAPP.Services.Entity
{
    public abstract class QueryStringParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > 50) ? maxPageSize : value;
            }
        }

        public string OrderBy { get; set; }
    }
}
