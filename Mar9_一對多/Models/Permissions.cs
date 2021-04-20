using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mar9_一對多.Models
{
    public class Permissions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "名稱")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(20)]
        public string Name { get; set; }

        public int? Pid { get; set; }

        //自己關聯自己(遞迴)
        [Display(Name = "父類別")]
        [ForeignKey("Pid")]
        public virtual Permissions Permissionss { get; set; }

        [Display(Name = "子類別")]
        public virtual ICollection<Permissions> PremissionsSon { get; set; }

        [Display(Name = "權限代號")]
        public string PValue { get; set; }

        [Display(Name = "連結")]
        [Required(ErrorMessage = "{0}必填")]
        public string Url { get; set; }
    }
}