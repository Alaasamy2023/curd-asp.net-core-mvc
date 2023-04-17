using curd;
using curd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace curd.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CRUDOperationsDemo _context;   // declaration
        private readonly IWebHostEnvironment env;       // declaration


        //ctor tab  اول خطه الكونستراكتور 
        //CRUDOperationsDemo context    عشان نقدر نتعامل مع الداتا بيز بتاعتى

        public EmployeesController(CRUDOperationsDemo context, IWebHostEnvironment env)
        {
            _context = context;  // inatialition
            this.env = env;      // inatialition
        }
        //........................................................

        // Index داله ال 
        /*
      _context.Employees هى بتعرض ليست من   Index فى داله 
      Employees     ده الموديل 
      _context    DBcontext  او اللى كنا بنسميه  CRUDOperationsDemo ده اوبجيكت من 
         */
        public IActionResult Index()
        {
            var Result = _context.Employees.Include(x => x.Department)
                 .OrderBy(x => x.EmployeeName).ToList();
            return View(Result);
        }
//........................................................
/*
 فى الكرييت 
كل اللى هنعرضه من داتا غير الفورم 
هو الدروبدون ليست اللى هتعرض الادارات والبافى الفورم مش هتاخد داتا من حنه 
 */
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            //تفاصيل العرض فى الدروب فى الفيو مش هنا 
            return View();
        }


//................

        [HttpPost]//post هنا هنبدا نعمل الكود الى هياخد الداتا من الفورم ويحفظها وده لازم ب    
        [ValidateAntiForgeryToken]  // لمنع اى هجوم على الداتا بيز
        public IActionResult Create(Employee model)   // هيبعت الموديل
        {
            /*
             داله رفع الصور دى هتاخد الموديل وترفع الصوره او لو مرفعتش صوره هتحط الصوره الافتراضيه ولو فى حاله تعديل الصوره هتعدل
             */
            UploadImage(model);
            if (ModelState.IsValid)  //  لو الموديل تمام
            {
                _context.Employees.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            return View();
        }





        //........................................................
        /*
         تذكر حاجه مهمه اوى 
        احنا استخدمنا نفس الفيو والفورم بتاعت الكرييت فى التعديل برضو 
        فروحنا فى الفيو 
        وعدلنا عنوان الصفحه وايضا الصفحه اللى الفورم بيخدها ب 
        الموديل 

         
         */
        public IActionResult Edit(int? Id)
        {
            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            var result = _context.Employees.Find(Id);
            return View("Create", result);
        }


//................


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee model)
        {
            UploadImage(model);
            if (model != null)
            {
                _context.Employees.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            return View(model);
        }







//........................................................



        private void UploadImage(Employee model)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {//@"wwwroot/"
                /*
                 Guid هو نوع من الاى دي مكون من 14 رقم مش بيتكرر 
                وهنا بحطه فى اسم الصوره 
                 */
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(env.WebRootPath, "Images", ImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                model.ImageUser = ImageName;
            }
            //فى حاله لم يتم رفع الصوره خد الافتراضيه 
            else if (model.ImageUser == null && model.EmployeeId == null)
            {
                model.ImageUser = "DefultImage.jpg";
            }
            // فى حاله لتعديل 
            else
            {
                model.ImageUser = model.ImageUser;
            }
        }
//........................................................


        public IActionResult Delete(int? Id)
        {
            var result = _context.Employees.Find(Id);
            if (result != null)
            {
                _context.Employees.Remove(result);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
//........................................................


    }
}
