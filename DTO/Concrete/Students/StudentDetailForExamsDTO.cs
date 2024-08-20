using ApplicationCore.Consts;
using DTO.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.Students
{
    public class StudentDetailForExamsDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public Guid AppUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string ClassroomName { get; set; }
        public Guid ClassroomId { get; set; }
        public double? Exam1 { get; set; }
        public double? Exam2 { get; set; }
        public double? ProjectExam { get; set; }
        public double? Average { get; set; }
        public StudentStatus StudentStatus { get; set; }
        public string? ProjectPath { get; set; }
        public string? ProjectName { get; set; }
        public string? ImagePath { get; set; }
    }
}
