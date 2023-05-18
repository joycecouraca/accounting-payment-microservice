namespace AccountingPayment.Domain.Entities
{
    public class EmployeeEntity : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public Guid SectorId { get; set; }
        public double GrossSalary { get; set; }
        public DateTime AdmissionDate { get; set; }
        public bool HealthPlanDiscount { get; set; }
        public bool DentalPlanDiscount { get; set; }
        public bool TransportationVoucherDiscount { get; set; }
        public virtual SectorEntity Sector { get; set; }
    }
}
