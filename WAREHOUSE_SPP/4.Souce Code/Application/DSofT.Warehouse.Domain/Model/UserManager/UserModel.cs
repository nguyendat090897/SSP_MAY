using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSofT.Warehouse.Domain.Model
{
    #region Web MOSMS
    [Serializable]
    public class UserRequestModel
    {
        public string apiKey { get; set; }
        public string seretKey { get; set; }
    }


    [Serializable]
    public partial class UserModel
    {
        
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string FULL_NAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }
        public string ADDRESS { get; set; }
        public string AVATAR { get; set; }
        public int ROLE_GROUP_ID { get; set; }
        public bool IS_DELETE { get; set; }
        public int IS_ADMIN { get; set; }
        public bool IS_AGENCY { get; set; }
        public int AGENCY_ID { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public int MODIFIED_BY { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
        public Guid TOKEN { get; set; }
        public decimal BALANCE { get; set; }
        public decimal GIFT_MONEY { get; set; }
        public string REFERRED_USER { get; set; }
        public string APIKEY { get; set; }
        public string SECRETKEY { get; set; }
        public int AreaID { get; set; }
        public int UserTypeID { get; set; }
        public List<BrandeNameApiModel> ListBrandName { get; set; }
    }

  





[Serializable]
    public partial class UserModelAPI
    {
        public List<BrandeNameApiModel> ListBrandName { get; set; }
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string FULL_NAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }
        public int IS_ADMIN { get; set; }
        public int IS_AGENCY { get; set; }
        public int AGENCY_ID { get; set; }       
        public decimal BALANCE { get; set; }
        public decimal GIFT_MONEY { get; set; }
        public string REFERRED_USER { get; set; }
        public string APIKEY { get; set; }
        public string SECRETKEY { get; set; }
    }
    [Serializable]
    public class UserRowModel : UserModel
    {
        public string ReferredUserEmail { get; set; }
        public int RowNumber { get; set; }
        public int TotalRows { get; set; }
    }

    [Serializable]
    public class LoginModel
    {
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
    }

    public class UpdatePasswordModel
    {
        public int USER_ID { get; set; }
        public string PASSWORD { get; set; }
    }

    public class HistoryRecharge
    {
        public int RECHARGE_ID { get; set; }
        public int USER_ID { get; set; }
        public decimal RECHARGE_MONEY { get; set; }
        public DateTime RECHARGE_DATE { get; set; }
        public int CHANGE_TYPE { get; set; }
        public int RECHARGE_TYPE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public int MODIFIED_BY { get; set; }
        public DateTime MODIFIED_DATE { get; set; }

    }
    #endregion Web MOSMS

    #region Publish Api
    [Serializable]
    public class UserApiModel
    {
        public decimal BALANCE { get; set; }
        public long USER_ID { get; set; }

        public long IS_DELETE { get; set; }
    }
    [Serializable]
    public class UserBalanceModel
    {
        public long BALANCE { get; set; }
    }


    [Serializable]
    public class BrandeNameApiModel
    {
        public string PHONE_PREFIX_NAME { get; set; }
        public long USER_ID { get; set; }
        public string GROUP_PHONE_PREFIX_NAME { get; set; }
        public long PHONE_PREFIX_ID { get; set; }
        public long GROUP_PHONE_PREFIX_ID { get; set; }
    }


    [Serializable]
    public class BrandeNameModel
    {
        public long MODIFIED_BY { get; set; }
        public long IS_DELETE { get; set; }
        public string PHONE_PREFIX_NAME { get; set; }
        public long USER_ID { get; set; }
        public long PHONE_PREFIX_ID { get; set; }
        public long GROUP_PHONE_PREFIX_ID { get; set; }
    }


    [Serializable]
    public class MoneySMSApiModel
    {
        public long GroupPhonePrefixId { get; set; }
        public long UnitPriceSmsViettel { get; set; }
        public decimal UnitPriceSmsVina { get; set; }
        public decimal UnitPriceSmsMobi { get; set; }
        public decimal UnitPriecSmsVietnam { get; set; }
        public decimal UnitPriceSmsGmobi { get; set; }
        public decimal UnitPriceSmsOrther { get; set; }
        public long UserId { get; set; }
    }

    [Serializable]
    public class UserRequestModelLogin
    {
        public string userName { get; set; }
        public string passWord { get; set; }
    }


    [Serializable]
    public class UserRequestAPI
    {
        public int userId { get; set; }
        public string groupId { get; set; }
        public string keyGroup { get; set; }
        public int? pageIndex { get; set; }
        public int numRow { get; set; }
    }
    #endregion Publish Api
}
