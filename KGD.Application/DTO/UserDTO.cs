using KGD.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGD.Application.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static explicit operator UserDTO(List<User> v)
        {
            throw new NotImplementedException();
        }
    }
}
