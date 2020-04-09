using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleatMarket.Base.Entities
{
    public class Declaration
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public int DeclarationStatusId { get; set; }
        [ForeignKey("DeclarationStatusId")]
        public DeclarationStatus DeclarationStatus { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int? ImageId { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }
    }
}
