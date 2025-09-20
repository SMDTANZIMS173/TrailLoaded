using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrailLoaded.Models;

namespace TrailLoaded.Controllers
{
    public class AdminController : Controller
    {
        SchoolTrailContext sc = new SchoolTrailContext();

        // Approve admission
        public IActionResult Approve(int admissionId)
        {
            var admission = sc.NewAdmissions.Find(admissionId);
            if (admission != null)
            {
                admission.Status = "Approved";
                sc.SaveChanges();
            }
            return RedirectToAction("PendingAdmissions");
        }

        // Reject admission
        public IActionResult Reject(int admissionId)
        {
            var admission = sc.NewAdmissions.Find(admissionId);
            if (admission != null)
            {
                admission.Status = "Rejected";
                sc.SaveChanges();
            }
            return RedirectToAction("PendingAdmissions");
        }
       
        // Mark fee paid for approved admission
        [HttpPost]
        public IActionResult MarkFeePaid(int admissionId)
        {
            // Find student linked to this admission
            var student = sc.Students.FirstOrDefault(s => s.AdmissionId == admissionId);

            if (student != null)
            {
                student.AdmFeePaid = true; // update fee paid
            }
            else
            {
                // If student record doesn't exist, create one
                var newStudent = new Student
                {
                    AdmissionId = admissionId,
                    AdmFeePaid = true
                };
                sc.Students.Add(newStudent);
            }

            sc.SaveChanges();
            return RedirectToAction("PendingAdmissions");
        }

        // Optional: View pending admissions
        public IActionResult PendingAdmissions()
        {
            var admissions = sc.NewAdmissions
     .Select(na => new NewAdmissionViewModel
     {
         AdmissionId = na.AdmissionId,
         FirstName = na.FirstName,
         LastName = na.LastName,
         Status = na.Status ?? "Pending",
         FeePaid = sc.Students.Any(s => s.AdmissionId == na.AdmissionId && s.AdmFeePaid == true)
     }).ToList();

            return View(admissions); // now matches IEnumerable<NewAdmissionViewModel>
        }

    }
}

