using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mar9_一對多.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }

        [Display(Name = "部門名稱")]
        [StringLength(50)]
        public string DepartmentName { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}