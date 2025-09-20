using Microsoft.AspNetCore.Mvc;
using TrailLoaded.Models;
using Microsoft.EntityFrameworkCore;

namespace TrailLoaded.Controllers
{
    public class StudentController : Controller
    {
        SchoolTrailContext sc = new SchoolTrailContext();

        //public StudentController(SchoolTrailContext sc)
        //{
        //    _context = context;
        //}

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(StudentLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var student = (from s in sc.Students
                           join na in sc.NewAdmissions
                           on s.AdmissionId equals na.AdmissionId
                           where s.AdmissionId == model.AdmissionNo
                                 && na.DateOfBirth == DateOnly.FromDateTime(model.DateOfBirth)
                                 && s.AdmFeePaid == true
                           select s).FirstOrDefault();

            if (student != null)
            {
                // Login successful
                HttpContext.Session.SetInt32("StudentId", student.StudentId);
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError("", "Invalid Admission No or DOB, or fee not paid.");
            return View(model);
        }

        public IActionResult Dashboard()
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");
            if (studentId == null) return RedirectToAction("Login");

            var student = (from s in sc.Students
                           join na in sc.NewAdmissions
                           on s.AdmissionId equals na.AdmissionId
                           where s.StudentId == studentId
                           select new StudentDashboardViewModel
                           {
                               StudentId = s.StudentId,
                               AdmissionId = s.AdmissionId,
                               AdmFeePaid = s.AdmFeePaid,
                               FirstName = na.FirstName,
                               LastName = na.LastName,
                               Standard = na.Standard

                           }).FirstOrDefault();

            if (student == null) return RedirectToAction("Login"); // safety check

            return View(student);
        }

    }

}
