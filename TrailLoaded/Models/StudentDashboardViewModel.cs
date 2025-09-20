namespace TrailLoaded.Models
{
    public class StudentDashboardViewModel
    {
        public int StudentId { get; set; }
        public int AdmissionId { get; set; }
        public bool? AdmFeePaid { get; set; }

        // Admission details
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Standard { get; set; }
    }

}
