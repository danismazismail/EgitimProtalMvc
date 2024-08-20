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
    public class GetStudentDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public Guid? ClassroomId { get; set; }
        public double? Exam1 { get; set; }
        public double? Exam2 { get; set; }
        public double? ProjectExam { get; set; }
        public double? Average { get; set; }
        public StudentStatus? StudentStatus { get; set; }

        //Öğrencinin Projeleri
        public string? ProjectPath { get; set; }
        public string? ProjectName { get; set; }
        public IFormFile? Project { get; set; }

        //Öğrencinin kullanıcı resmi
        public string? ImagePath { get; set; }
        public IFormFile? Image { get; set; }
    }
}
