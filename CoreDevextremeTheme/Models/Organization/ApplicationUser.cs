namespace CoreDevextremeTheme.Models.Organization
{
    public class ApplicationUser : IdentityUser
    {        
        [DisplayName("Sicil No")]
        [DisplayFormat(DataFormatString = "{0:0000}")]
        public int RegistrationNumber { get; set; }

        //public virtual Employee Employee { get; set; }

        //[ForeignKey("Employee")]
        //public int? EmployeeRegistrationNumber { get; set; }
    }
}
