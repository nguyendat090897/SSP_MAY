using DVS.CS.Domain;
using DVS.Framework.Client.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVS.CS.ApiClient
{
    public interface ISalesOrderClient
    {
        Task<ApiResponse<List<SalesOrderElementList>>> GetAllSalesOrder(SalesOrderFilter SOFilter, decimal PAGESIZE, decimal PAGEINDEX);

        Task<ApiResponse<List<SalesOrderElementList>>> GetAllSalesOrderByCustomer(GetSalesOrderByCustomerReq salesOrderByCustomerReq);

        Task<ApiResponse<List<ScheduleSoDetails>>> GetSoDetailByStatus(ScheduleSoDetails model);

        Task<ApiResponse<List<SalesOrderDetails>>> GetSoDetailByKey(SalesOrderDetails model);

        Task<ApiResponse<SaleOrderReceiver>> GetOrderReceiverbyId(SaleOrderReceiver model);

        Task<ApiResponse<bool>> UpdateSaleOrderDeatailByStatus(SalesOrderDetails model);

        Task<ApiResponse<bool>> UpdateOrderReceiver(SaleOrderReceiver model);

        Task<ApiResponse<bool>> InsertSaleOrderDetailList(List<SalesOrderDetails> modelList);

        Task<ApiResponse<SalesOrderProcessing>> GetSODetailForProcessing(string SO_NUMBER);

        Task<ApiResponse<int>> InsertSalesOrderLog(SalesOrderLogInsertReq salesOrderLogInsertReq);

        Task<ApiResponse<bool>> DecreaseQuantitySODetailByID(decimal SO_DETAIL_ID, int MODIFIED_BY, int QUANTITY = 1);

        Task<ApiResponse<bool>> DeleteSODetailByID(decimal SO_DETAIL_ID, int MODIFIED_BY);

        Task<ApiResponse<bool>> DeleteSODetail(decimal SO_ID, decimal SKU_ID, decimal soldPrice, int MODIFIED_BY);

        Task<ApiResponse<bool>> DeleteSaleOrderByID(decimal SaleOrderId, int MODIFIED_BY);

        Task<ApiResponse<bool>> UpdateStatusSODetailByID(decimal SO_DETAIL_ID, int MODIFIED_BY, int SO_STATUS_ID);

        Task<ApiResponse<bool>> UpdateIsSplitSalesOrder(SplitSalesOrderModel model);

        Task<ApiResponse<List<SalesOrderPaymentModel>>> GetListSalesOrderPayment(int SoId);

        Task<ApiResponse<bool>> CancelSO(CancelSOReq model);

        Task<ApiResponse<List<GetSOBySoNumberRes>>> GetSalesOrderBySoNumber(string SoNumber);

        Task<ApiResponse<SalesOrderVAT>> GetSOVatBySoId(decimal SoId);

        Task<ApiResponse<bool>> UpdateSoVatInfo(UpdateSOVatReq updateSOVatReq);
        Task<ApiResponse<List<SalesOrderByUserIdRes>>> GetSalesOrderByUserId(SalesOrderByUserIdReq valueFilter);
    }

    public class SalesOrderClient : BaseClient, ISalesOrderClient
    {
        private string _postController = string.Concat(AppHostClient, UrlCommon.C_SalesOrder);

        public SalesOrderClient()
            : base()
        {
        }

        public async Task<ApiResponse<List<SalesOrderElementList>>> GetAllSalesOrder(SalesOrderFilter SOFilter, decimal PAGESIZE, decimal PAGEINDEX)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_GetAllSalesOrder);
            dicParams = new Dictionary<string, object>();
            var Obj = new SalesOrderReq
            {
                SOFilter = SOFilter,
                PAGESIZE = PAGESIZE,
                PAGEINDEX = PAGEINDEX
            };
            return await ExcuteRequestAsync<List<SalesOrderElementList>, SalesOrderReq>(urlSend, Obj);
        }

        public async Task<ApiResponse<List<SalesOrderElementList>>> GetAllSalesOrderByCustomer(GetSalesOrderByCustomerReq salesOrderByCustomerReq)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_GetAllSalesOrderByCustomer);
            dicParams = new Dictionary<string, object>();

            return await ExcuteRequestAsync<List<SalesOrderElementList>, GetSalesOrderByCustomerReq>(urlSend, salesOrderByCustomerReq);
        }

        public async Task<ApiResponse<List<ScheduleSoDetails>>> GetSoDetailByStatus(ScheduleSoDetails model)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_GetSoDetailByStatus);
            dicParams = new Dictionary<string, object>();

            return await ExcuteRequestAsync<List<ScheduleSoDetails>, ScheduleSoDetails>(urlSend, model);
        }

        public async Task<ApiResponse<List<SalesOrderDetails>>> GetSoDetailByKey(SalesOrderDetails model)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_GetSoDetailByKey);
            dicParams = new Dictionary<string, object>();

            return await ExcuteRequestAsync<List<SalesOrderDetails>, SalesOrderDetails>(urlSend, model);
        }

        public async Task<ApiResponse<SaleOrderReceiver>> GetOrderReceiverbyId(SaleOrderReceiver model)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_GetOrderReceiverbyId);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<SaleOrderReceiver, SaleOrderReceiver>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> UpdateSaleOrderDeatailByStatus(SalesOrderDetails model)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_UpdateSaleOrderByStatus);
            dicParams = new Dictionary<string, object>();

            return await ExcuteRequestAsync<bool, SalesOrderDetails>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> UpdateOrderReceiver(SaleOrderReceiver model)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_UpdateOrderReceiver);
            dicParams = new Dictionary<string, object>();

            return await ExcuteRequestAsync<bool, SaleOrderReceiver>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> InsertSaleOrderDetailList(List<SalesOrderDetails> modelList)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_InsertSaleOrderDetail);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, List<SalesOrderDetails>>(urlSend, modelList);
        }

        public async Task<ApiResponse<SalesOrderProcessing>> GetSODetailForProcessing(string SO_NUMBER)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_GetSODetailForProcessing);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<SalesOrderProcessing, string>(urlSend, SO_NUMBER);
        }

        public async Task<ApiResponse<int>> InsertSalesOrderLog(SalesOrderLogInsertReq salesOrderLogInsertReq)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_InsertSalesOrderLog);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<int, SalesOrderLogInsertReq>(urlSend, salesOrderLogInsertReq);
        }

        public async Task<ApiResponse<bool>> DecreaseQuantitySODetailByID(decimal SO_DETAIL_ID, int MODIFIED_BY, int QUANTITY = 1)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_DecreaseQuantitySODetailByID);
            dicParams = new Dictionary<string, object>();

            var dataReq = new SODecreaseQuantityReq()
            {
                SODetailId = SO_DETAIL_ID,
                ModifiedBy = MODIFIED_BY,
                Quantity = QUANTITY
            };

            return await ExcuteRequestAsync<bool, SODecreaseQuantityReq>(urlSend, dataReq);
        }

        public async Task<ApiResponse<bool>> DeleteSODetailByID(decimal SO_DETAIL_ID, int MODIFIED_BY)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_DeleteSODetailByID);
            dicParams = new Dictionary<string, object>();

            var dataReq = new SODetailDeleteReq()
            {
                SODetailId = SO_DETAIL_ID,
                ModifiedBy = MODIFIED_BY
            };

            return await ExcuteRequestAsync<bool, SODetailDeleteReq>(urlSend, dataReq);
        }

        public async Task<ApiResponse<bool>> DeleteSODetail(decimal SO_ID, decimal SKU_ID, decimal soldPrice, int MODIFIED_BY)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_DeleteSODetail);
            dicParams = new Dictionary<string, object>();

            var dataReq = new SODetailDelete2Req()
            {
                SoID = SO_ID,
                SkuID = SKU_ID,
                ModifiedBy = MODIFIED_BY,
                SoldPrice = soldPrice
            };

            return await ExcuteRequestAsync<bool, SODetailDelete2Req>(urlSend, dataReq);
        }

        public async Task<ApiResponse<bool>> DeleteSaleOrderByID(decimal SO_ID, int MODIFIED_BY)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_DeleteSaleOrderByID);
            dicParams = new Dictionary<string, object>();

            var dataReq = new SalesOrderDeleteReq()
            {
                SalesOrderId = SO_ID,
                ModifiedBy = MODIFIED_BY
            };

            return await ExcuteRequestAsync<bool, SalesOrderDeleteReq>(urlSend, dataReq);
        }

        public async Task<ApiResponse<bool>> UpdateStatusSODetailByID(decimal SO_DETAIL_ID, int MODIFIED_BY, int SO_STATUS_ID)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_UpdateStatusSODetailByID);
            dicParams = new Dictionary<string, object>();

            var dataReq = new SODetailUpdateStatusReq()
            {
                SODetailId = SO_DETAIL_ID,
                ModifiedBy = MODIFIED_BY,
                SOStatusId = SO_STATUS_ID
            };

            return await ExcuteRequestAsync<bool, SODetailUpdateStatusReq>(urlSend, dataReq);
        }

        public async Task<ApiResponse<bool>> UpdateIsSplitSalesOrder(SplitSalesOrderModel model)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_UpdateIsSplitSalesOrder);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, SplitSalesOrderModel>(urlSend, model);
        }

        public async Task<ApiResponse<List<SalesOrderPaymentModel>>> GetListSalesOrderPayment(int SoId)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_GetListSalesOrderPayment);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<SalesOrderPaymentModel>, int>(urlSend, SoId);
        }

        public async Task<ApiResponse<bool>> CancelSO(CancelSOReq model)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_CancelSO);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, CancelSOReq>(urlSend, model);
        }

        public async Task<ApiResponse<List<GetSOBySoNumberRes>>> GetSalesOrderBySoNumber(string SoNumber)
        {
            var model = new GetSOBySoNumberReq() { SoNumber = SoNumber };
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_GetSalesOrderBySoNumber);

            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<GetSOBySoNumberRes>, GetSOBySoNumberReq>(urlSend, model);
        }

        public async Task<ApiResponse<SalesOrderVAT>> GetSOVatBySoId(decimal SoId)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_GetSOVatBySoId);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<SalesOrderVAT, decimal>(urlSend, SoId);
        }

        public async Task<ApiResponse<bool>> UpdateSoVatInfo(UpdateSOVatReq updateSOVatReq)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_UpdateSoVatInfo);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, UpdateSOVatReq>(urlSend, updateSOVatReq);
        }

        public async Task<ApiResponse<List<SalesOrderByUserIdRes>>> GetSalesOrderByUserId(SalesOrderByUserIdReq valueFilter)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SalesOrder_GetSalesOrderByUserId);
            dicParams = new Dictionary<string, object>();

            return await ExcuteRequestAsync<List<SalesOrderByUserIdRes>, SalesOrderByUserIdReq>(urlSend, valueFilter);
        }
    }
}