namespace HospitalAutomation.Models
{
    public class UserUpdateViewModel
    {
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public string About { get; set; }
        public int ShipID { get; set; }
        public int DepartmentID { get; set; }
    }
}
