using System.ComponentModel.DataAnnotations;

namespace KGD.Domain.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
