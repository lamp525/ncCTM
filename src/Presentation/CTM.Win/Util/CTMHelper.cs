using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Util;

namespace CTM.Win.Util
{
    public class CTMHelper
    {
        /// <summary>
        /// 取得部门对应的交易类别
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static EnumLibrary.TradeType GetTradeTypeByDepartment(int deptId)
        {
            EnumLibrary.TradeType tradeType;

            switch ((EnumLibrary.AccountingDepartment)deptId)
            {
                case EnumLibrary.AccountingDepartment.Day:
                    tradeType = EnumLibrary.TradeType.Day;
                    break;

                case EnumLibrary.AccountingDepartment.Band:
                    tradeType = EnumLibrary.TradeType.Band;
                    break;

                case EnumLibrary.AccountingDepartment.Target:
                    tradeType = EnumLibrary.TradeType.Target;
                    break;

                case EnumLibrary.AccountingDepartment.Independence:
                    tradeType = EnumLibrary.TradeType.All;
                    break;

                default:
                    tradeType = EnumLibrary.TradeType.All;
                    break;
            }

            return tradeType;
        }

        /// <summary>
        /// 取得交易类别名称
        /// </summary>
        /// <param name="tradeType"></param>
        /// <returns></returns>
        public static string GetTradeTypeName(int value)
        {
            string name = value.ToEnumDescriptionString(typeof(EnumLibrary.TradeType));

            return name;
        }

        /// <summary>
        /// 取得交易数据类别名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDataTypeName(int value)
        {
            string name = value.ToEnumDescriptionString(typeof(EnumLibrary.DataType));

            return name;
        }
    }
}