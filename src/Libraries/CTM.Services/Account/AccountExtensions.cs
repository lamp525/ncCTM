using System.Collections.Generic;
using System.Linq;
using CTM.Core.Domain.Account;

namespace CTM.Services.Account
{
    public static class AccountExtensions
    {
        /// <summary>
        /// Return all  operator names
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string CombineOperatorNames(this IList<AccountOperator> source)
        {
            var nameArray = source.Select(x => x.OperatorInfo.Name).ToArray();

            return string.Join("; ", nameArray);
        }
    }
}