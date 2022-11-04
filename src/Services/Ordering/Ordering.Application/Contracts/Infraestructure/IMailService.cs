using Ordering.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Infraestructure
{
    public interface IMailService
    {
        Task<bool> SendEmail(Email email);
    }
}
