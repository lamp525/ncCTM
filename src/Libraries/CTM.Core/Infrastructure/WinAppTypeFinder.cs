using System;
using System.Collections.Generic;
using System.Reflection;

namespace CTM.Core.Infrastructure
{
    /// <summary>
    /// Provides information about types in the current windows application.
    /// Optionally this class can look at all assemblies in the bin folder.
    /// </summary>
    public class WinAppTypeFinder : AppDomainTypeFinder
    {
        #region Fields

        private bool _ensureBinFolderAssembliesLoaded = true;
        private bool _binFolderAssembliesLoaded;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets whether assemblies in the bin folder of the web application should be specificly checked for beeing loaded on application load. This is need in situations where plugins need to be loaded in the AppDomain after the application been reloaded.
        /// </summary>
        public bool EnsureBinFolderAssembliesLoaded
        {
            get { return _ensureBinFolderAssembliesLoaded; }
            set { _ensureBinFolderAssembliesLoaded = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets a physical disk path of \Bin directory
        /// </summary>
        /// <returns>The physical path. </returns>
        public virtual string GetBinDirectory()
        {
            //if (HostingEnvironment.IsHosted)
            //{
            //    //hosted
            //    return HttpRuntime.BinDirectory;
            //}

            //not hosted. For example, run either in unit tests
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public override IList<Assembly> GetAssemblies()
        {
            if (this.EnsureBinFolderAssembliesLoaded && !_binFolderAssembliesLoaded)
            {
                _binFolderAssembliesLoaded = true;
                string binPath = GetBinDirectory();
                //binPath = _webHelper.MapPath("~/bin");
                LoadMatchingAssemblies(binPath);
            }

            return base.GetAssemblies();
        }

        #endregion Methods
    }
}