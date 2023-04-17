using curd.Models;
using Microsoft.EntityFrameworkCore;

namespace curd
{

    //CRUDOperationsDemo اسمه  DbContext  لتنظيم العمل سننشئ   


    public class CRUDOperationsDemo : DbContext
    {
        //  اول خطوه هي انشاء كونستراكتور 
        // ctor  --tab+tab   اختصار عمل كونستراكتور لاى كلاس هو  
        // دور على شرح للبراميتر اللى اخده ده 
        public CRUDOperationsDemo(DbContextOptions<CRUDOperationsDemo> options) : base(options)
        {

        }

        // دى داله اوفريت على الكلاس ثابته والتعليق هو انشاء اسكيمه بطريقه مختلفه 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Employee>().ToTable("Employees", "HR");
        }

        // لكل جدول عندنا DbSet هننشئ 
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
