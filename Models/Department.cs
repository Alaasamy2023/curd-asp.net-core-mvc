using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace curd.Models
{
    [Table("Departments", Schema = "HR")]
    // كدا احنا اديناله اسكيما على الجدول ده 
    //Schema = "HR"    اسم الاسكيما اللى قبل الجدول 
    public class Department
    {
        //Key وهو من نوع  IDوهنديله اسم يظهر بيه  DepartmentId اول بروبيرتى هو 
        [Key]
        [Display(Name = "ID")]
        public int DepartmentId { get; set; }
        //..............................................................

        [Required]
        [Display(Name = "Department Name")]
        [Column(TypeName = "varchar(200)")]
        public string DepartmentName { get; set; } = string.Empty;
        // string.Empty لكن لازم تديله قيمه افتراضيه   DepartmentName البروبيرتى التانى هو 
        // TypeName = "varchar(200)"     نوعه 
        // Required لاوم يتحط 
        //..............................................................

    }
}
