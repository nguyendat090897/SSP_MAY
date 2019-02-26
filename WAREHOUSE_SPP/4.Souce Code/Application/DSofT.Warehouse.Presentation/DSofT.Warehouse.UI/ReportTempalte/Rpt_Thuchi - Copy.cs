using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace DSofT.Warehouse.UI
{
    public partial class Rpt_NhapXuat : DevExpress.XtraReports.UI.XtraReport
    {
        private XRControlStyle xrControlStyle;
        private XRLabel xrLabel;
        private TopMarginBand topMarginBand1;
        private DetailBand detailBand1;
        private BottomMarginBand bottomMarginBand1;
        private TopMarginBand topMarginBand2;
        private DetailBand detailBand2;
        private BottomMarginBand bottomMarginBand2;
        private DevExpress.XtraReports.Parameters.Parameter txtTieude1;


        public Rpt_NhapXuat()
        {
            InitializeComponent();
        }


        private void InitializeComponent()
        {
            this.topMarginBand2 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.detailBand2 = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand2 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand2
            // 
            this.topMarginBand2.HeightF = 100F;
            this.topMarginBand2.Name = "topMarginBand2";
            // 
            // detailBand2
            // 
            this.detailBand2.HeightF = 100F;
            this.detailBand2.Name = "detailBand2";
            // 
            // bottomMarginBand2
            // 
            this.bottomMarginBand2.HeightF = 100F;
            this.bottomMarginBand2.Name = "bottomMarginBand2";
            // 
            // Rpt_NhapXuat
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand2,
            this.detailBand2,
            this.bottomMarginBand2});
            this.Version = "14.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
