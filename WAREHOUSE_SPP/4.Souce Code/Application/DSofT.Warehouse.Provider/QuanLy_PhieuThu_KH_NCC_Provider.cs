﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface I_QuanLy_PhieuThu_KH_NCC_Provider
    {
        DataTable GetAll(string MaDVQLY,string Loai_phieu);
        DataTable GetKH(string MaDVQLY,string Loai_KHNCC);
        DataTable GetHD(string MaDVQLY ,int MA_KH);
        DataTable CheckPT(string MaDVQLY,string MA_PT, string Loai_phieu);
        bool Delete(string MaDVQLY, int ID);
        bool Insert(DataTable dt);
        bool Update(DataTable dt);
    }
    public class QuanLy_PhieuThu_KH_NCC_Provider:BaseSqlExecute,I_QuanLy_PhieuThu_KH_NCC_Provider
    {
        public QuanLy_PhieuThu_KH_NCC_Provider(string appId, string userId) : base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public DataTable GetAll(string MaDVQLY, string Loai_phieu)
        {
            var paramObj = new object[] { MaDVQLY, Loai_phieu };
            var result = base.ExecuteDataSet("[CN_PHIEUTHU_GetAll]", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetKH(string MaDVQLY, string Loai_KHNCC)
        {
            var paramObj = new object[] { MaDVQLY, Loai_KHNCC };
            var result = base.ExecuteDataSet("[CN_PHIEUTHU_GetKH]", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetHD(string MaDVQLY, int MA_KH)
        {
            var paramObj = new object[] { MaDVQLY, MA_KH };
            var result = base.ExecuteDataSet("[CN_PHIEUTHU_GetHD]", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public bool Delete(string MaDVQLY, int ID)
        {
            try
            {
                var paramObj = new object[] { MaDVQLY, ID };
                var result = base.ExecuteDataSet("[CN_PHIEUTHU_Delete]", paramObj);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           
            
        }
        public bool Insert(DataTable dt)
        {
            try
            {
                var paramObj = new object[]{
                           ConstCommon.DonViQuanLy,
                           dt.Rows[0]["KHACHHANG_NCC_ID"],
                           dt.Rows[0]["HOPDONG_ID"],
                           dt.Rows[0]["SO_PHIEUTHU"],
                           dt.Rows[0]["NGAY_PHIEU"],
                           dt.Rows[0]["SO_TIEN"],
                           CommonDataHelper.UserName,
                           dt.Rows[0]["LOAI_PHIEU"],
                           dt.Rows[0]["GHI_CHU"],
                           dt.Rows[0]["HO_TEN_NGUOINOP"],
                           dt.Rows[0]["DIA_CHI_NGUOINOP"],
                           dt.Rows[0]["KEM_THEO"],
                           dt.Rows[0]["CHUNG_TU"]
                             };
                var result = base.ExecScalar("CN_PHIEUTHU_Insert", paramObj);
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool Update(DataTable dt)
        {
            try
            {
                var paramObj = new object[]{
                           dt.Rows[0]["PHIEUTHU_ID"],
                           ConstCommon.DonViQuanLy,
                           dt.Rows[0]["KHACHHANG_NCC_ID"],
                           dt.Rows[0]["HOPDONG_ID"],
                           dt.Rows[0]["SO_PHIEUTHU"],
                           dt.Rows[0]["NGAY_PHIEU"],
                           dt.Rows[0]["SO_TIEN"],
                           CommonDataHelper.UserName,
                           dt.Rows[0]["GHI_CHU"],
                           dt.Rows[0]["HO_TEN_NGUOINOP"],
                           dt.Rows[0]["DIA_CHI_NGUOINOP"],
                           dt.Rows[0]["KEM_THEO"],
                           dt.Rows[0]["CHUNG_TU"]
                             };
                var result = base.ExecScalar("CN_PHIEUTHU_Update", paramObj);
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public DataTable CheckPT(string MaDVQLY, string MA_PT, string Loai_phieu)
        {
            var paramObj = new object[] { MaDVQLY, MA_PT, Loai_phieu };
            var result = base.ExecuteDataSet("[CN_PHIEUTHU_CheckPT]", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
    }
}
