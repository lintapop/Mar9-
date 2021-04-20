using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mar9_一對多.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int DepID { get; set; }

        [Required]
        [Display(Name = "人員名稱")]
        [StringLength(50)]
        public string customerName { get; set; }

        [Required]
        [Display(Name = "人員密碼")]
        [StringLength(50, ErrorMessage = "請輸入密碼", MinimumLength = 3)]
        public string password { get; set; }

        [MaxLength(100)]
        public string passwordSalt { get; set; }

        [Required]
        [Display(Name = "人員電話號碼")]
        [StringLength(50, ErrorMessage = "請輸入電話號碼", MinimumLength = 9)]
        public string tel { get; set; }

        [Required]
        [Display(Name = "人員Email")]
        [StringLength(30, ErrorMessage = "請輸入Email")]
        [EmailAddress(ErrorMessage = "Email格式錯誤")]
        public string email { get; set; }

        [Display(Name = "權限")]
        public string Authority { get; set; }

        [ForeignKey("DepID")]
        public virtual Department Departments { get; set; }
    }
}