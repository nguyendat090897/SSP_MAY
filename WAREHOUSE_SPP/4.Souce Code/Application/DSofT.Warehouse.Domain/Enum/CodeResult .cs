using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSofT.Warehouse.Domain.Enum
{
    public enum CodeResult
    {
        Success = 100, //Request thành công
        Undefined = 99,// Lỗi không xác định, thử lại sau 
        LoginFail = 101,// Đăng nhập thất bại (api key hoặc secrect key không đúng) 
        AccountLocked = 102,// Tài khoản đã bị khóa 
        BalanceNotEnough = 103,// Số dư tài khoản không đủ dể gửi tin 
        BrandnameIncorrect = 104,// Mã Brandname không đúng 
        SMSTypeIncorrect = 118,// Loại tin nhắn không hợp lệ 
        BrandnameNotEnough = 119,// Brandname quảng cáo phải gửi ít nhất 20 số điện thoại 
        BrandnameLenghtMax = 131,// Tin nhắn brandname quảng cáo độ dài tối đa 422 kí tự 
        DoNotAllowed = 132,// Không có quyền gửi tin nhắn đầu số cố định 8755 
        MessageTokenNotFound = 105,// Message token không tồn tại trong hệ thống
    }
}
