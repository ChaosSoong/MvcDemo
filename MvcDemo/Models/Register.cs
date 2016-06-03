using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcDemo.Models
{
    public class Register
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
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("PassWord", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "性别")]
        public string Sex { get; set; }
    }
}