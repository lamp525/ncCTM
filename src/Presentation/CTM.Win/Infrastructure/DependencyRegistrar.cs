using System.Linq;
using Autofac;
using CTM.Core.Data;
using CTM.Core.Infrastructure;
using CTM.Data;
using CTM.Services;

namespace CTM.Win.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        #region Methods

        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //register windows forms
            var drTypes = typeFinder.FindClassesOfType<System.Windows.Forms.Form>().ToArray();
            builder.RegisterTypes(drTypes).InstancePerLifetimeScope();

            //data layer
            builder.Register<IDbContext>(c => new CTMContext()).SingleInstance();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //register services
            drTypes = typeFinder.FindClassesOfType<IBaseService>().ToArray();
            builder.RegisterTypes(drTypes).AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 2; }
        }

        #endregion Methods
    }
}