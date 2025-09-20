namespace TrailLoaded.Models
{
    public class NewAdmissionViewModel
    {
        public int AdmissionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }

        // Comes from Student table
        public bool FeePaid { get; set; }
    }
}
