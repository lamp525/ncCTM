using System;
using CTM.Core.Data;
using CTM.Core.Domain.Log;

namespace CTM.Services.Log
{
    public partial class LogService : ILogService
    {
        #region Fields

        private readonly IRepository<LoginLog> _loginLogRepo;

        #endregion Fields

        #region Constructors

        public LogService(IRepository<LoginLog> loginLogRepo)
        {
            this._loginLogRepo = loginLogRepo;
        }

        #endregion Constructors

        #region Methods

        public void SaveLoginLog(LoginLog entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _loginLogRepo.Insert(entity);
        }

        #endregion Methods
    }
}