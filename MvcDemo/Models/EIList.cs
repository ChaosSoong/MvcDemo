using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcDemo.Models
{
    public class EIList
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int StudentId { get; set; }
        [Display(Name = "学科")]
        public string DisciplineName { get; set; }
        [Display(Name = "考试时间")]
        [DataType(DataType.Date)]
        public System.DateTime ExaminationTime { get; set; }
        [Display(Name = "成绩")]
        public int Fraction { get; set; }
    }
}