namespace AccountingPayment.Domain.Model.Employee
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public Guid SectorId { get; set; }
        public double GrossSalary { get; set; }
        public DateTime AdmissionDate { get; set; }
        public bool HealthPlanDiscount { get; set; }
        public bool DentalPlanDiscount { get; set; }
        public bool TransportationVoucherDiscount { get; set; }
        public DateTime? CreateAt
        {
            get { return CreateAt; }
            set
            {
                CreateAt = value == null ? DateTime.UtcNow : value;
            }
        }
        public DateTime? UpdateAt
        {
            get { return UpdateAt; }
            set { UpdateAt = value; }
        }
        public bool Deleted { get; set; }
    }
}
