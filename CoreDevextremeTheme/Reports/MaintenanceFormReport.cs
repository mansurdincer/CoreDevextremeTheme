using DevExpress.XtraReports.Parameters;

namespace CoreDevextremeTheme.Reports
{
    public partial class MaintenanceFormReport : DevExpress.XtraReports.UI.XtraReport
    {
        public Guid MaintenanceFormId { get; set; }

        public MaintenanceFormReport()
        {
            InitializeComponent();
        }

        // Raporun Parametrelerini Ayarla
        public void SetReportParameters()
        {
            // Id parametresini oluştur
            var idParameter = new Parameter
            {
                Name = "Id",
                Type = typeof(Guid),
                Value = MaintenanceFormId
            };

            // Rapor parametrelerine ekle
            this.Parameters.Clear();
            this.Parameters.Add(idParameter);
        }
    }
}
