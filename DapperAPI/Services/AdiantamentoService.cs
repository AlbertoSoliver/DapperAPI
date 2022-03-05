using Microsoft.Extensions.Configuration;
using DapperAPI.Entities;
using DapperAPI.DataServices;

namespace DapperAPI.Services
{
    public class AdiantamentoService : Db2Service<ADIANTAMENTO>, IAdiantamentoService
    {
        public AdiantamentoService(IConfiguration config) : base(config)
        {

        }

    }
}
