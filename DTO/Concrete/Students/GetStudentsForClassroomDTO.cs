using ApplicationCore.Consts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Abstract;

namespace DTO.Concrete.Students
{
    public class GetStudentsForClassroomDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public double? Exam1 { get; set; }
        public double? Exam2 { get; set; }
        public double? ProjectExam { get; set; }
        public double? Average { get; set; }
        public StudentStatus StudentStatus { get; set; }
        public string? ProjectPath { get; set; }
        public string? ProjectName { get; set; }

    }
}
