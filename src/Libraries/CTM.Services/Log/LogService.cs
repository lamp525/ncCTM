using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTM.Core.Data;
using CTM.Core.Domain.Log;

namespace CTM.Services.Log
{
    public partial class LogService : ILogService
    {
        #region Fields
        private readonly IRepository<LoginLog> _loginLogRepo;
        #endregion

        public LogService (IRepository<LoginLog> loginLogRepo)
        {
            this._loginLogRepo = loginLogRepo;
        }

        public void SaveLoginLog(LoginLog entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _loginLogRepo.Insert(entity);
        }
    }
}
