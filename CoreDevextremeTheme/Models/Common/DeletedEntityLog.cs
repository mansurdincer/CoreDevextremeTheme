namespace CoreDevextremeTheme.Models.Common
{
    public class DeletedEntityLog
    {
        public Guid Id { get; set; }

        public Guid EntityId { get; set; }
        public string EntityName { get; set; }

        public string EntityType { get; set; }

        public string? EntityJson { get; set; }

        public Guid DeletedById { get; set; }

        public DateTime DeletedAt { get; set; }
    }
}
