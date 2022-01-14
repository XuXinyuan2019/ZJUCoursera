using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZJUCoursera1.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "学号：")]
        public string Number { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码：")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }
}