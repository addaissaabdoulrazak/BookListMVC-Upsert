using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Author { get; set; }
        public string ISBN { get; set; }
    }
}
