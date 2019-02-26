using DevExpress.Xpf.Printing;
using DevExpress.XtraReports.UI;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DSofT.Warehouse.Business;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for PrintDialog.xaml
    /// </summary>
    public partial class PrintDialogModel : CompositeBase
    {
        //InventoryManagerBussiness _InventoryManagerBussiness;
        DataTable iGridDataSourceTest = null;
        public PrintDialogModel()
        {
            InitializeComponent();

        }
        public override void Communicate(string source, params object[] parameters)
        {
            try
            {
                object[] PARAMETER = parameters;
                if (PARAMETER != null)
                {
                    switch (PARAMETER[1].ToString())
                    {
                        case "WarehouseSupervision":
                            LoadPrintWarehouseSupervision(PARAMETER);
                            break;
                        case "Inventory":
                            LoadPrintInventory(PARAMETER);
                            break;
                        case "VesselDischarging":
                            LoadPrintVesselDischarging(PARAMETER);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        public void LoadPrintWarehouseSupervision(object[] PARAMETER)
        {
            try
            {
                //DataTable tbl = (DataTable)PARAMETER[0];
                //rpt_WarehouseSupervision rpt = new rpt_WarehouseSupervision();
                //rpt.DataSource = tbl;
                //XtraReportPreviewModel model = new XtraReportPreviewModel(rpt);
                //rpt.CreateDocument(true);
                //model.IsParametersPanelVisible = false;
                //DocumentPreview1.Model = model;
                //model.ParametersModel.SubmitParameters();
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        public void LoadPrintVesselDischarging(object[] PARAMETER)
        {
            try
            {
                //DataTable tbl = (DataTable)PARAMETER[0];
                //rpt_VesselDischarging rpt = new rpt_VesselDischarging();
                //rpt.DataSource = tbl;
                //XtraReportPreviewModel model = new XtraReportPreviewModel(rpt);
                //rpt.CreateDocument(true);
                //model.IsParametersPanelVisible = false;
                //DocumentPreview1.Model = model;
                //model.ParametersModel.SubmitParameters();
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        public void LoadPrintInventory(object[] PARAMETER)
        {
           try
            {
                //DataSet ds = (DataSet)PARAMETER[0];
                //DataTable tbl1 = ds.Tables[0];
                //DataTable tbl2 = ds.Tables[1];
                //DataTable tbl3 = ds.Tables[2];
                ////DataTable tbl3 = (DataTable)PARAMETER[3];
                //rpt_Inventory rpt = new rpt_Inventory();
                
                //DetailReportBand nhapkho =(DetailReportBand) rpt.Bands["NhapKho"];
                //nhapkho.DataSource = tbl1;
                //DetailReportBand xuatkho = (DetailReportBand)rpt.Bands["XuatKho"];
                //xuatkho.DataSource = tbl2;
                ////DetailReportBand chuyenkho = (DetailReportBand)rpt.Bands["ChuyenKho"];
                ////chuyenkho.DataSource = tbl3;
                //rpt.DataSource = tbl3;
                //XtraReportPreviewModel model = new XtraReportPreviewModel(rpt);
                //rpt.CreateDocument(true);
                //model.IsParametersPanelVisible = false;
                //DocumentPreview1.Model = model;
                //model.ParametersModel.SubmitParameters();
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
     
    }
}
