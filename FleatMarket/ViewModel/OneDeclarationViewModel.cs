using System;
using System.Collections.Generic;

namespace FleatMarket.Web.ViewModel
{
    public class OneDeclarationViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string AuthorId { get; set; }
        public string AuthorMail { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string StatusName { get; set; }
        public int StatusId { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }

        public UserViewModel User { get; set; }
    }
}
