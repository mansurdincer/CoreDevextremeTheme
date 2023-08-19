namespace CoreDevextremeTheme.Models.Common
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        int Sequence { get; set; }
        Guid CreatedById { get; set; }
        DateTime CreatedAt { get; set; }
        Guid? UpdatedById { get; set; }
        DateTime? UpdatedAt { get; set; }
        bool IsDeleted { get; set; }
        Guid? DeletedById { get; set; }
        DateTime? DeletedAt { get; set; }
        bool CanDelete(DbContext context, out string errorMessage);
    }
}
