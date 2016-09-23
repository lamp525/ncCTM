using System;

namespace CTM.Core.Domain.Log
{
    public class LoginLog : BaseEntity
    {
        public string UserCode { get; set; }

        public DateTime Date { get; set; }

        public string Time { get; set; }

        public string IP { get; set; }

        public string MACAddress { get; set; }

        public bool IsSuccced { get; set; }
    }
}