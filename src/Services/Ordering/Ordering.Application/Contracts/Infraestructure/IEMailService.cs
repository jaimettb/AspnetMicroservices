using Ordering.Application.Models;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Infraestructure
{
    public interface IEMailService
    {
        Task<bool> SendEmail(Email email);
    }
}
