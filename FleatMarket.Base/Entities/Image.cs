using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleatMarket.Base.Entities
{
    public class Image
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }

        public List<User> Users { get; set; }
        public List<Declaration> Declarations { get; set; }
    }
}
