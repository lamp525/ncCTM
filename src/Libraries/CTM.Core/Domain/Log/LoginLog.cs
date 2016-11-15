using System;

namespace CTM.Core.Domain.Log
{
    public class LoginLog : BaseEntity
    {
        public string UserCode { get; set; }

        public string UserName { get; set; }

        public string IP { get; set; }

        public string MAC { get; set; }

        public DateTime Time { get; set; }     

        public string Remark { get; set; }
    }
}