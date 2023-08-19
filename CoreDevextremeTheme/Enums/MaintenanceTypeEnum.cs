namespace CoreDevextremeTheme.Enums
{
    public enum MaintenanceTypeEnum
    {
        [Display(Name = "Periyodik Bakım")]
        Periodic = 1,
        [Display(Name = "Arıza Bildirimi")]
        Breakdown = 2,
        [Display(Name = "Temizlik")]
        Cleaning = 3,
        [Display(Name = "Parça Değişimi ve Ayar")]
        Adjustment = 4
    }
}
