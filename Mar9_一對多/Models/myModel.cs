using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Mar9_一對多.Models
{
    public partial class myModel : DbContext
    {
        public myModel()
            : base("name=myModel")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Permissions> Permissionss { get; set; }

        //public virtual DbSet<LoginViewModel> LoginViewModels { get; set; }
    }
}