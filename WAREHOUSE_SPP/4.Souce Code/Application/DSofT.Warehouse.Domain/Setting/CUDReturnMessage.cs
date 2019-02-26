using System;

namespace DSofT.Warehouse.Domain
{
    [Serializable]
    public class CUDReturnMessage
    {
        public CUDReturnMessage()
        {
            ID = 0;
            Message = string.Empty;
        }

        public CUDReturnMessage(int id, string msg)
        {
            this.ID = id;
            this.Message = msg;
        }

        public int ID { get; set; }

        private string _message;

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;// FunctionResource.GetResourceString(typeof(MessageResource), value.Trim());
            }
        }
    }
}
