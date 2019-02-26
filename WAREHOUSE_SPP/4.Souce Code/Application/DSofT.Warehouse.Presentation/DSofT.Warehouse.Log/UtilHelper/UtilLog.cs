using DSofT.Warehouse.Business;
using DSofT.Warehouse.Domain.Model.System;
using DSofT.Framework.UIControl;
using System;
using System.Net;
using System.Net.Sockets;

namespace DSofT.Warehouse.Log.UtilHelper
{
    public class UtilLog
    {
        /// <summary>
        /// GetLocalIPAddress
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        /// <summary>
        /// WriteLogToDB
        /// </summary>
        /// <param name="group"></param>
        /// <param name="contentError"></param>
        /// <param name="formName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool WriteLogToDB(LogGroupName group, string contentError, string formName, string action)
        {
            ISysLogsBussiness _sysLogsBussiness=new SysLogsBussiness(string.Empty);
            var ip = GetLocalIPAddress();
            var username = CommonDataHelper.UserName;
            string strFormat = string.Format("#{0} | IP: {1}| USER: {3} | MSG:{2}", group, ip, contentError, username);
            S_LogModel _sysLog = new S_LogModel
            {
                UserName = username,
                LogDate = DateTime.Now,
                LogAction = action,
                LogLevel = group.ToString(),
                Logger = formName,
                FormName = formName,
                LogContent = strFormat,
                Exception = contentError
            };
            _sysLogsBussiness.InsertSysLog(_sysLog);
            return true; ;
        }
    }
}
