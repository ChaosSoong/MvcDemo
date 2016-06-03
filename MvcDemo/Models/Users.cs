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

    public partial class Users
    {
        [Display(Name = "用户姓名")]
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "用户名长度在2位到20位之间。")]
        public string UserName { get; set; }
        [Key]
        [Display(Name = "登陆名")]
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "登陆名长度在8位到20位之间。")]
        public string UserCode { get; set; }
        [Display(Name = "密码")]
        [Required]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "密码长度在8位到30位之间。")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Display(Name = "性别")]
        public string Sex { get; set; }
    }
}
