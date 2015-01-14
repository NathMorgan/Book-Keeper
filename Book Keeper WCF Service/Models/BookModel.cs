using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Keeper_WCF_Service.Models
{
    public class BookModel
    {
        public int Bookid { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public List<AuthorModel> Authors { get; set; }
    }
}