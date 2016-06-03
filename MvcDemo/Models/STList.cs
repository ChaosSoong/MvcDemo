using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MvcDemo.Models
{
    public class STList
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "姓名")]
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "姓名长度在2位到20位之间。")]
        public string Name { get; set; }
        [Display(Name = "性别")]
        public string Sex { get; set; }
        [Display(Name = "系别")]
        [Required]
        public string DepartmentName { get; set; }
        [Display(Name = "入学时间")]
        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString="yyyy-MM-dd")]
        public System.DateTime EntranceTime { get; set; }
    }
}