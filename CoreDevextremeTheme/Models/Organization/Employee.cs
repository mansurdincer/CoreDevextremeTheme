namespace CoreDevextremeTheme.Models.Organization
{
    public class Employee : BaseEntity
    {
        [DisplayName("Sicil No")]
        [DisplayFormat(DataFormatString = "{0:0000}")]
        public int RegistrationNumber { get; set; }


        //[ForeignKey("ApplicationUser")]
        //public int? ApplicationUserRegistrationNumber { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }


        [DisplayName("Ad")]
        public string? Name { get; set; }


        [DisplayName("Soyad")]
        public string? Surname { get; set; }


        [DisplayName("E-mail Adres")]
        public string? Email { get; set; }


        [DisplayName("Telefon")]
        public string? Phone { get; set; }


        [DisplayName("Adres")]
        public string? Address { get; set; }


        [DisplayName("Şehir")]
        public string? City { get; set; }


        [DisplayName("Fotoğraf")]
        public string? PhotoPath { get; set; }


        [NotMapped]
        public IFormFile? Photo { get; set; }


        [DisplayName("Notlar")]
        public string? Notes { get; set; }


        [DisplayName("İşe Giriş Tarihi")]
        public DateTime? DateOfJoin { get; set; }


        [DisplayName("İşten Çıkış Tarihi")]
        public DateTime? DateOfResign { get; set; } = null;

        [DisplayName("Aktif")]
        public bool Active { get; set; } = true;


        [DisplayName("Ünvan")]
        public Guid? JobTitleId { get; set; }


        [DisplayName("Ünvan")]
        public virtual JobTitle? JobTitle { get; set; }


        [DisplayName("Departman")]
        public Guid? DepartmentId { get; set; }


        [DisplayName("Departman")]
        public virtual Department? Department { get; set; }

       
    }
}
