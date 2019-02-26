using System;

namespace DVS.CS.Domain
{
    [Serializable]
    public class SettingModel
    {

        public decimal SettingId { get; set; }
        public string SettingKey { get; set; }
        public string SettingTitle { get; set; }
        public string SettingValue { get; set; }

    }
     [Serializable]
    public class SettingUpdateModel
    {

        public decimal SettingId { get; set; }
        public string SettingValue { get; set; }
        public decimal ModifiedBy { get; set; }
    }
}
