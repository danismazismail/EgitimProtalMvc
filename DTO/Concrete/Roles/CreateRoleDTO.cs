using DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.Roles
{
    public class CreateRoleDTO : BaseDTO
    {
        public string Name { get; set; }
    }
}
