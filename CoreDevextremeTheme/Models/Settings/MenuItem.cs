namespace CoreDevextremeTheme.Models.Settings
{
    public class MenuItem : BaseEntity
    {        
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public Guid? ParentId { get; set; }
       
    }
}
