using DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.Clasrooms
{
    public class CreateClassroomDTO : BaseDTO
    {
        public string ClassroomName { get; set; }
        public string Description { get; set; }
        public Guid TeacherId { get; set; }
    }
}
