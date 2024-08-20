using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.Users
{
    public class GetUserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
