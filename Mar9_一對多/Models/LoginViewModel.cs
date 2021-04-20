using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mar9_一對多.Models
{
    public class LoginViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "人員名稱")]
        [StringLength(50, MinimumLength = 3)]
        public string customerName { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "人員密碼")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0}長度必須大於3", MinimumLength = 3)]
        public string password { get; set; }
    }
}