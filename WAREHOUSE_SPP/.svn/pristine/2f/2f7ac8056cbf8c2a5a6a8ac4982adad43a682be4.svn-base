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
using DSofT.Framework.Helpers;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for PrintDialog.xaml
    /// </summary>
    public partial class PrintDialogShift : CompositeBase
    {
        DataTable iGridDataSourceTest = null;
        DataTable iGridDataSourceTest1 = null;
        DataTable iGridDataSourceTest2 = null;
        DataTable iDataSource = null;
        //IReportShiftBussiness _reportShiftManagerBussiness;
        public PrintDialogShift()
        {
            InitializeComponent();
            //_reportShiftManagerBussiness = new ReportShiftBussiness(string.Empty);
            this.iDataSource = TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            this.iDataSource.Rows[0]["nhanvien"] = CommonDataHelper.UserInfo["FullName"].ToString();
            this.iDataSource.Rows[0]["ca"] = CommonDataHelper.UserInfo["ShiftName"].ToString();
            this.iDataSource.Rows[0]["caucan"] = CommonDataHelper.UserInfo["AssignedValuesName"].ToString();
            LoadPrint();

        }

        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("nhanvien", typeof(string));
                xDicUser.Add("ca", typeof(string));
                xDicUser.Add("caucan", typeof(string));              
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }
        public void LoadPrint()
        {
            try
            {
                //DataSet ds = _reportShiftManagerBussiness.GetReportShift(CommonDataHelper.UserId.ToInt32(),CommonDataHelper.UserInfo["AssignedID"].ToInt32());
                //if (ds != null && ds.Tables.Count == 4)
                //{
                //    this.iGridDataSourceTest = ds.Tables[0];
                //    this.iGridDataSourceTest1 = ds.Tables[1];
                //    this.iGridDataSourceTest2 = ds.Tables[2];
                //    iDataSource = ds.Tables[3];
                //}
                //rptKho rpt = new rptKho();

                //rpt.DataSource = iDataSource;
                //DetailReportBand nhapkho = (DetailReportBand)rpt.Bands["DetailReport"];
                //nhapkho.DataSource = iGridDataSourceTest;
                //DetailReportBand xuatkho = (DetailReportBand)rpt.Bands["DetailReport1"];
                //xuatkho.DataSource = iGridDataSourceTest1;
                //DetailReportBand chuyenkho = (DetailReportBand)rpt.Bands["DetailReport2"];
                //chuyenkho.DataSource = iGridDataSourceTest2;               
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
