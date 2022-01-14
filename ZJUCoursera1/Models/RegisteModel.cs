using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZJUCoursera1.Models
{
    public class RegisteModel
    {
        [Required]
        [Display(Name = "学号")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "入学年份")]
        public string Year { get; set; }

        [Display(Name = "专业")]
        public string Major { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }
}