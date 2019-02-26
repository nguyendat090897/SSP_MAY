
using DSofT.FoodWarehouse.Business;
using DSofT.Framework.Helpers;
using DSofT.Framework.Logging;
using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace DSofT.FoodWarehouse.DataSync
{
    partial class DataSync : ServiceBase
    {
        private string _logFilePath;
        private int _timeSleep;
        IDataSyncBussiness _dataSyncBussiness;

        public DataSync()
        {
            InitializeComponent();
            _timeSleep = 10;
            _logFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\log.txt";
            WriteLog(_logFilePath, "Init DataSync", true);
            _dataSyncBussiness = new DataSyncBussiness(string.Empty);
            //_messageClient = new MessageClient();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Thread t = new Thread(new ThreadStart(this.Process));
                WriteLog(_logFilePath, "Start Service", true);
                t.Start();
            }
            catch (Exception ex)
            {
                CommonLogger.WriteLog(ex, SystemLogSource.WinService);
                this.Stop();
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            WriteLog(_logFilePath, "Stop Service", true);
        }

        public void Process()
        {
            WriteLog(_logFilePath, "===================================================================");

            while (true)
            {
                if (_timeSleep == 10)
                    _timeSleep = Convert.ToInt32(AppHelpers.GetAppSetting("SleepTime"));
                WriteLog(_logFilePath, "===================================================================");
                try
                {
                    _dataSyncBussiness.DataAsync2System();
                    //var syncTimestamp = Task.Run(() => _messageClient.UpdateStatusSMSSend()).Result;
                    //syncTimestamp = Task.Run(() => _messageClient.UpdateStatusIDSMSSend()).Result;
                    //syncTimestamp = Task.Run(() => _messageClient.SendSMSDate()).Result;
                }
                catch (Exception ex)
                {
                    WriteLog(_logFilePath, "Auto DataSync Error - exception: " + ex.Message, true);
                    this.Stop();
                }
                // sleep
                WriteLog(_logFilePath, "Service sleep: " + _timeSleep + " milisecond", true);
                System.Threading.Thread.Sleep(_timeSleep);
            };
        }


        private void WriteLog(string filePath, string newContent, bool isAppendTime = false, bool saveErrorLog = false)
        {
            var dateTimeText = string.Empty;
            string currentContent = String.Empty;

            dateTimeText = isAppendTime ? DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss - ") : string.Empty;

            File.AppendAllText(filePath, dateTimeText + newContent + Environment.NewLine);
        }

    }
}
