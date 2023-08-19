using CoreDevextremeTheme.Models.Common;

namespace CoreDevextremeTheme.Models.Organization
{
    public class JobTitle : BaseEntity
    {
        [DisplayName("Ad")]
        public string Name { get; set; }

        [DisplayName("Açıklama")]
        public string? Description { get; set; }

        [DisplayName("Ünvana Sahip Çalışanlar")]
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}