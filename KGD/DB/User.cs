using System.ComponentModel.DataAnnotations;

namespace KGD
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}