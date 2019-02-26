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
using DevExpress.Utils;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using DSofT.Warehouse.Resources;
using DSofT.Warehouse.Log.UtilHelper;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_ChangePassword.xaml
    /// </summary>
    public partial class popup_ChangePassword : PopupBase
    {
        DataTable iDataSource = null;
        UserBussiness _UserBussiness;
        public popup_ChangePassword()
        {
            InitializeComponent();
            //CommonDataHelper.IdUser
            _UserBussiness = new UserBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
        }

        public void UserChangePassword()
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(iDataSource.Copy());
                if (txtMatKhauMoi.Text == txtNhapLaiMatKhauMoi.Text)
                {
                    bool result = _UserBussiness.User_ChangePassword(ds);
                    if (result == true)
                    {
                        base.ShowMessage(MessageType.Information, UtilLanguage.ApplyLanguage()["lblMessageSuccess"].ToString());
                    }
                    else
                    {
                        base.ShowMessage(MessageType.Information, UtilLanguage.ApplyLanguage()["lblMessageFailed"].ToString());
                    }
                }
                else
                    base.ShowMessage(MessageType.Information, UtilLanguage.ApplyLanguage()["lblMessageFailed"].ToString());
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "UserChangePassword()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

        }

        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("Passwordold", typeof(string));
                xDicUser.Add("Passwordnew", typeof(string));
                xDicUser.Add("ID", typeof(int));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            UserChangePassword();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
