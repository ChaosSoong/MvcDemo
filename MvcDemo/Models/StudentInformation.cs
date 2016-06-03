//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcDemo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    public partial class StudentInformation
    {
        public StudentInformation()
        {
            this.ExaminationInformation = new HashSet<ExaminationInformation>();
        }

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
        public int Department { get; set; }
        [Display(Name = "入学时间")]
        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString="yyyy-MM-dd")]
        public System.DateTime EntranceTime { get; set; }

        public virtual ICollection<ExaminationInformation> ExaminationInformation { get; set; }
    }
}