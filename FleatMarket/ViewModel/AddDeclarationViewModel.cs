using System;
using System.Collections.Generic;

namespace FleatMarket.Web.ViewModel
{
    public class AddDeclarationViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorMail { get; set; }
        public string AuthorPhone { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Id { get; set; }//для редактирования
    }
}
