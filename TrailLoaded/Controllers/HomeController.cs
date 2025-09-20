using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrailLoaded.Models;
using System.Linq;



namespace YourNamespace.Controllers
{
    public class HomeController : Controller
    {
        SchoolTrailContext sc = new SchoolTrailContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactForm contact)
        {
            if (ModelState.IsValid)
            {
                sc.ContactForms.Add(contact);
                sc.SaveChanges();
                ViewBag.Message = "Your message has been sent successfully!";
                return View(); // stays on the same page with success msg
            }

            return View(contact); // if validation fails, reload with entered data
        }
        public IActionResult NewAdmission()
        {
            return View();
        }

        // POST: Admission/NewAdmission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewAdmission(NewAdmission admission)
        {
            if (ModelState.IsValid)
            {
                // Generate AdmissionCode (6-digit unique random)
                admission.AdmissionCode = Math.Abs(Guid.NewGuid().GetHashCode() % 1000000).ToString("D6");

                sc.Add(admission);
                await sc.SaveChangesAsync();

                TempData["Success"] = "Admission submitted successfully!";
                return RedirectToAction(nameof(Success));
            }
            return View(admission);
        }

        // Success Page
        public IActionResult Success()
        {
            return View();
        }

    }
}
