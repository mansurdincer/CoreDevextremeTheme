namespace CoreDevextremeTheme.Models.Common
{
    public interface ISoftDeletable
    {
        Guid? DeletedById { get; set; }
        DateTime? DeletedAt { get; set; }
        bool IsDeleted { get; set; }
    }
}
