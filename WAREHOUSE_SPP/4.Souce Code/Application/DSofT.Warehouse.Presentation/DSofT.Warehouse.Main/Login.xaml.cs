using DSofT.Framework.Metro.UIControls.Controls;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using DSofT.Warehouse.Business;
using DSofT.Warehouse.Domain.Model;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using DSofT.Framework.Helpers;
using DSofT.Warehouse.Resources;
using DSofT.Framework.Logging;

namespace DSofT.Warehouse.Main
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        RegistryKey regKey = null;
        BackgroundWorker bw = null;
        //string[] xParamater = null;
        LoginModel login;
        private UserModel _userModel = new UserModel();
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        public Login()
        {
            InitializeComponent();
            login = new LoginModel();
            string a = System.IO.Directory.GetCurrentDirectory();
            try
            {
                regKey = Registry.CurrentUser;
                regKey = regKey.CreateSubKey("Software\\DSofT\\DVSoft\\Warehouse\\LOGIN");
                CommonDataHelper.DonViQuanLy = "PE0800";
                this.txtUserName.Text = regKey.GetValue("USER_NAME") == null ? "" : regKey.GetValue("USER_NAME").ToString();
                Timer.Tick += new EventHandler(Timer_Click);
                Timer.Interval = new TimeSpan(0, 0, 1);
                Timer.Start();
            }
            catch (Exception ex)
            {
                File.WriteAllText(a + @"\logfileerror.txt", ex.Message);
                UIAgent.ShowMessage(MessageType.Information, "Lỗi kết nối tới server!\r\nError connection to Server!");
            }
            if (this.txtUserName.Text.Equals("") == false)
            {
                txtPassword.Focus();
            }
            else
            {
                txtUserName.Focus();
            }
        }

        private void Timer_Click(object sender, EventArgs e)
        {
            DateTime d;
            d = DateTime.Now;
            //clock.Content = d.Day + "." + d.Month + "." + d.Year + " - " + d.Hour + " : " + d.Minute + " : " + d.Second;
        }

        private bool Valid()
        {
            if (txtUserName.Text.Trim().Equals("") == true)
            {
                lblHienThi.Text = UtilLanguage.ApplyLanguage()["application_username_not_null"].ToString();
                lblHienThi.Visibility = Visibility.Visible;
                txtUserName.Focus();
                return false;
            }

            if (txtPassword.Text.Trim().Equals("") == true)
            {
                lblHienThi.Text = UtilLanguage.ApplyLanguage()["application_pass_not_null"].ToString();
                lblHienThi.Visibility = Visibility.Visible;
                txtPassword.Focus();
                return false;
            }
            return true;

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                if (Valid() == false)
                {
                    return;
                }
                login = new LoginModel()
                {
                    PASSWORD = txtPassword.Text.EncryptPassword(),/* "DquCtUTD6s40RVV+G/6U6A==",// CommonDataHelper.ENCRYPT(txtPassword.Text),*/
                    USER_NAME = txtUserName.Text
                };
                //xParamater = new string[4];
                //xParamater[0] = this.txtUserName.Text.ToLower();
                //xParamater[1] = this.txtPassword.Text.EncryptPassword();
                //xParamater[2] = CommonDataHelper.DonViQuanLy;
                transmitprotocol();
            }
            catch (Exception ex)
            {
                lblHienThi.Text = "Lỗi" + ex.ToString();
                lblHienThi.Visibility = Visibility.Visible;
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                    CommonLogger.WriteLog(ex, SystemLogSource.SystemRuntime);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }


        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(btnLogin, new RoutedEventArgs());
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(btnLogin, new RoutedEventArgs());
            }
        }

        #region Xử lý tác vụ nền nhưng ở đây chưa sử dụng
        // để ở đây khi nào cần thì dùng
        //frmWait msgForm;
        public void transmitprotocol()
        {
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.ProgressChanged += bw_ProgressChanged;
            //you can use progresschange in order change waiting message while background working
            //msgForm = new frmWait();//set lable and your waiting text in this form
            try
            {
                bw.RunWorkerAsync();
                this.grdLogin.Visibility = Visibility.Hidden;
                this.grdLoading.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                UIAgent.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                System.Data.DataSet userModel = e.UserState as System.Data.DataSet;
                if (userModel == null || userModel.Tables.Count == 0 || userModel.Tables[0].Rows.Count == 0)
                {
                    //lblHienThi.Text = UtilLanguage.ApplyLanguage()["application_login_false"].ToString();
                    lblHienThi.Visibility = Visibility.Visible;
                    this.grdLogin.Visibility = Visibility.Visible;
                    this.grdLoading.Visibility = Visibility.Hidden;
                }
                else
                {
                    try
                    {
                        regKey = Registry.CurrentUser;
                        regKey = regKey.CreateSubKey("Software\\DSofT\\DVSoft\\Warehouse\\LOGIN");
                        regKey.SetValue("USER_NAME", this.txtUserName.Text);
                    }
                    catch
                    {
                    }
                    if (userModel != null)
                    {
                        CommonDataHelper.UserName = userModel.Tables[0].Rows[0]["UserName"].NullToStringTrim();//.USER_NAME;
                        CommonDataHelper.UserId = userModel.Tables[0].Rows[0]["ID"].NullToStringTrim();
                        CommonDataHelper.FullName = userModel.Tables[0].Rows[0]["FullName"].NullToStringTrim();
                        this.Hide();
                        MainWindow frm = new MainWindow();
                        frm.Show();
                    }
                    else
                    {
                        UIAgent.ShowMessage(MessageType.Information, UtilLanguage.ApplyLanguage()["application_connect_sql_false"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                UIAgent.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // Transmitting protocol coding here. Takes around 2 minutes to finish. 
            //you have to write down your Transmitting codes here
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.               
                try
                {

                    IUserManagerBussiness _userManagerBussiness = new UserManagerBussiness(string.Empty);
                    var valueReturn = _userManagerBussiness.CheckLogin(login);
                    if (valueReturn != null && valueReturn.Tables.Count > 0 && valueReturn.Tables[0].Rows.Count > 0)
                    {
                        CommonDataHelper.UserInfo = valueReturn.Tables[0].Rows[0];
                        CommonDataHelper.UserId = CommonDataHelper.UserInfo["ID"].NullToStringTrim();
                    }

                    worker.ReportProgress(1, valueReturn);
                }
                catch (Exception ex)
                {
                    CommonLogger.WriteLog(ex, SystemLogSource.SystemRuntime);
                    worker.ReportProgress(0, null);
                }

                System.Threading.Thread.Sleep(200);
            }
            worker.Dispose();
            //The following code is just for loading time simulation and you can remove it later. 
            //System.Threading.Thread.Sleep(5 * 1000); //this code take 5 seconds to be passed
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //all background work has complete and we are going to close the waiting message
            bw.CancelAsync();
            // this.grdLogin.Visibility = Visibility.Visible;
            // this.grdLoading.Visibility = Visibility.Hidden;
            //msgForm.Close();
        }
        #endregion

        private void btnLanguageVN_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Resources.MergedDictionaries.Add()
        }

        private void btnLanguageEN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
