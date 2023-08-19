namespace CoreDevextremeTheme.Models.Organization;

public class Department : BaseEntity
{
    [DisplayName("Ad")]
    public string Name { get; set; }

    [DisplayName("Açıklama")]
    public string? Description { get; set; }

    [DisplayName("Üst Kategori")]
    public Guid? ParentId { get; set; }

    [DisplayName("Üst Kategori")]
    public virtual Department? Parent { get; set; }

    [DisplayName("Alt Kategoriler")]
    public virtual ICollection<Department>? Children { get; set; }

    [DisplayName("Çalışanlar")]
    public virtual ICollection<Employee>? Employees { get; set; }
}