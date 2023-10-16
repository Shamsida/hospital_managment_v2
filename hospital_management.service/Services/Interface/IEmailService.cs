
using hospital_management.DAL.Models;

namespace hospital_management.BLL.Services.Interface
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
