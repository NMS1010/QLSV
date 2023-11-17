using QLSV.Models;
using QLSV.ModelViews;

namespace QLSV.InterfacesService
{
    public interface IHocSinhService
    {
        HocSinh updateBalance(int userID, decimal price, int type);
        int SignIn(LoginViewModel model);
        int SignUp(RegisterViewModel model);
        int ChangePassword(ChangePasswordViewModel model, int accountId);
        bool SendVerification(int userId, string verifyCode);
    }
}
