using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SportsStore.Domain.Entities
{
    public class User
    {
        [Key]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }
        [Required(ErrorMessage = "请输入用户名。")]
        public string Name { get; set; }
        [Required(ErrorMessage = "请输入密码。")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "请确认密码输入一致。")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "请输入邮箱。")]
        public string Email { get; set; }
        [Required(ErrorMessage = "请输入电话号码。")]
        public string PhoneNumber { get; set; }
    }
    
}
