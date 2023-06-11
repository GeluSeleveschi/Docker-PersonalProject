using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalProject.BLL.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public Guid OrderGuid { get; set; }
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalCost { get; set; }
    }
}
