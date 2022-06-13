using PáginaGym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PáginaGym.Servicios
{
    public interface IEnvioMail
    {
        Task SendEmailAsync(EnvioMail mailRequest);
    }
}
