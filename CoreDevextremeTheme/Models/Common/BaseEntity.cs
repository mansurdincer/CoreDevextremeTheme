namespace CoreDevextremeTheme.Models.Common
{
    public abstract class BaseEntity :IBaseEntity, ISoftDeletable
    {        
        public Guid Id { get; set; }

        [DisplayName("Sira")]
        public int Sequence { get; set; }

        [DisplayName("Kaydeden")]
        public Guid CreatedById { get; set; }
        
        [NotMapped]
        public virtual ApplicationUser? CreatedBy { get; set; }

        [DisplayName("Kayit Tarihi")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DisplayName("Güncelleyen")]
        public Guid? UpdatedById { get; set; }

        [NotMapped]
        public virtual ApplicationUser? UpdatedBy { get; set; }

        [DisplayName("Güncelleme Tarihi")]
        public DateTime? UpdatedAt { get; set; }

        [DisplayName("Silinme")]
        public bool IsDeleted { get; set; } = false;
        
        [DisplayName("Silen")]
        public Guid? DeletedById { get; set; }

        [NotMapped]
        public virtual ApplicationUser? DeletedBy { get; set; }

        [DisplayName("Silme Tarihi")]
        public DateTime? DeletedAt { get; set; }

        //[Timestamp]
        //public byte[]? Version { get; set; }

        public virtual bool CanDelete(DbContext context, out string errorMessage)
        {
            errorMessage = string.Empty;
            bool canDelete = true;

            // BaseEntity sınıfından türeyen diğer Entity sınıflarında kontrol edilecek koşullar burada yer alabilir.


            return canDelete;
        }
    }
}
