using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectFacadeDesignPattern.Enums;

namespace TestProjectFacadeDesignPattern
{
    public class SearchData
    {
        public Sizes Size { get; set; }
        public Colors Color { get; set; }
        public Types Type { get; set; }
        public People People { get; set; }
        public Dates Date { get; set; }
        public Licenses License { get; set; }
        public int ResultNumber { get; set; }
        public string SearchTerm { get; set; }
    }
}
