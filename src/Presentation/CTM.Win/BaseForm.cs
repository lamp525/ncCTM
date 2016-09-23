using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;

namespace CTM.Win
{
    public class BaseForm : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        /// <summary>
        /// 页面状态
        /// 0：默认
        /// 1：显示当前用户查询结果
        /// </summary>
        public EnumLibrary.PageMode PageMode { get; set; }

        #endregion Properties
    }
}