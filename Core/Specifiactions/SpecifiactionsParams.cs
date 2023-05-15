using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifiactions
{
    public class SpecifiactionsParams
    {
        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public string? Sort { get; set; }


        private int _pageSize = 6;

        private const int MAXPAGESIZE = 50;

        public int pageSize
        {
            get => _pageSize; 
            set => _pageSize = (value > MAXPAGESIZE)? MAXPAGESIZE : value;
        }

        public int pageIndex { get; set; } = 1;

        private string? _search;

        public string? Search
        {
            get => _search; 
            set => _search = value; 
        }

    }
}
