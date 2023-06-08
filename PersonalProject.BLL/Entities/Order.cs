using System.ComponentModel.DataAnnotations;

namespace PersonalProject.BLL.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public Guid OrderGuid { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
