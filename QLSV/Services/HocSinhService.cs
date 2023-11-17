using QLSV.Enums;
using QLSV.Extension;
using QLSV.Helpper;
using QLSV.Interfaces;
using QLSV.InterfacesService;
using QLSV.Models;
using QLSV.ModelViews;
using System;
using System.Net.Mail;

namespace QLSV.Services
{
    public class HocSinhService : IHocSinhService
    {
        private readonly IHocSinhRepository _HocSinhRepository;

        public HocSinhService(IHocSinhRepository HocSinhRepository)
        {
            _HocSinhRepository = HocSinhRepository;
        }

        public HocSinh updateBalance(int userID, decimal price, int type)
        {
            HocSinh user = _HocSinhRepository.GetById(userID);
            if (type == (int)marketType.buy)
            {
                user.Balance = user.Balance - price;
            }
            else if (type == (int)marketType.sell)
            {
                user.Balance = user.Balance + price;
            }
            return user;
        }

        /// <summary>
        /// Đăng nhập tài khoản
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// -1: Không tìm thấy tài khoản được đăng ký với địa chỉ email này
        ///  2: Đăng nhập thất bại
        ///  Các trường hợp còn lại: Đăng nhập thành công
        /// </returns>
        public int SignIn(LoginViewModel model)
        {
            HocSinh user = _HocSinhRepository.FindByEmail(model.Gmail);

            if (user == null)
                return -1;

            if (user.Password == (model.Password + user.Salt.Trim()).ToMD5())
                return user.Id;

            return 2;
        }

        /// <summary>
        /// Đăng ký tài khoản
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// -1: Địa chỉ email này đã được đăng ký tài khoản
        ///  0: Có lỗi trong quá trình đăng ký
        ///  1: Đăng ký thành công
        /// </returns>
        public int SignUp(RegisterViewModel model)
        {
            if (_HocSinhRepository.FindByEmail(model.Email) != null)
                return -1;

            string salt = Utilities.GetRandomKey();

            HocSinh account = new HocSinh()
            {
                Gmail = model.Email,
                HoTen = model.FullName,
                Password = (model.Password + salt.Trim()).ToMD5(),
                Salt = salt
            };

            try
            {
                _HocSinhRepository.Create(account);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Đổi mật khẩu tài khoản
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// -2: Mật khẩu hiện tại không trùng khớp
        /// -1: Không tìm thấy tài khoản được đăng ký với địa chỉ email này
        ///  0: Có lỗi trong quá trình đổi mật khẩu
        ///  1: Đổi mật khẩu thành công
        /// </returns>
        public int ChangePassword(ChangePasswordViewModel model, int userId)
        {
            HocSinh user = _HocSinhRepository.GetById(userId);

            if (user == null)
                return -1;

            string oldPassword = (model.PasswordNow.Trim() + user.Salt.Trim()).ToMD5();
            string newPassword = (model.Password.Trim() + user.Salt.Trim()).ToMD5();

            if (!oldPassword.Equals(user.Password))
                return -2;

            user.Password = newPassword;

            try
            {
                _HocSinhRepository.Update(user);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool SendVerification(int userId, string verifyCode)
        {
            HocSinh user = _HocSinhRepository.GetById(userId);

            if (user == null)
                return false;

            MailMessage message = new MailMessage()
            {
                Subject = "Account Verification",
                IsBodyHtml = true
            };

            message.Body += "<h1>Hello " + user.HoTen + "</h1>";
            message.Body += "<p>Your verification code is <strong>" + verifyCode + "</strong></p>";

            Utilities.SendEmail(message, user.Gmail);

            return true;
        }
    }
}
