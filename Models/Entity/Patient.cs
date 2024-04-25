namespace Patient_Portal.Models.Entity
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Age { get; set; }
        public string Department { get; set; }
    }
}
