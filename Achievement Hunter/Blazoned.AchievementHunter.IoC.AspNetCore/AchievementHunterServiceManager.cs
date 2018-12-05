using System.Linq;
using System.Reflection;
using Autofac;
using Blazoned.AchievementHunter.DAL;
using Blazoned.AchievementHunter.DAL.Configuration;
using Blazoned.AchievementHunter.DAL.InMemory;
using Blazoned.AchievementHunter.DAL.MySQL;
using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;

namespace Blazoned.AchievementHunter.IoC.AspNetCore
{
    public static class AchievementHunterServiceManager
    {
        #region IoC Container
        /// <summary>
        /// Creates a new container builder and adds the achievement hunter library to it. If the data access layer has not yet been configured, you need to manually do so afterwards.
        /// </summary>
        /// <param name="databaseLibraryPath">The path of the database access library file.</param>
        /// <param name="dataAccessConfigurationLibraryPath">The path of the data access configuration library file.</param>
        /// <returns>Returns an autofac builder populated with the configuration container.</returns>
        public static ContainerBuilder ConfigureBuilder(string databaseLibraryPath, string dataAccessConfigurationLibraryPath)
        {
            ContainerBuilder builder = new ContainerBuilder();

            ConfigureBuilder(ref builder, databaseLibraryPath, dataAccessConfigurationLibraryPath);

            return builder;
        }
        /// <summary>
        /// Configures an already existing container builder by adding the achievement hunter library to it. If the data access layer has not yet been configured, you need to manually do so afterwards.
        /// </summary>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="databaseLibraryPath">The path of the database access library file.</param>
        /// <param name="dataAccessConfigurationLibraryPath">The path of the data access configuration library file.</param>
        public static void ConfigureBuilder(ref ContainerBuilder builder, string databaseLibraryPath, string dataAccessConfigurationLibraryPath)
        {
            if (builder == null)
                builder = new ContainerBuilder();

            // Register the achievement manager
            builder.RegisterType<AchievementManager>().InstancePerLifetimeScope();

            // Register the data access configuration
            builder.RegisterAssemblyTypes(Assembly.LoadFile(dataAccessConfigurationLibraryPath))
                .Where(t => t.GetInterfaces()
                             .Where(i => t.Name.Contains(i.Name.Substring(1)))
                                               .Count() >= 1)
                .As(t => t.GetInterfaces()
                          .FirstOrDefault(i => t.Name.Contains(i.Name.Substring(1))))
                .InstancePerLifetimeScope();

            // Register the data access layer
            builder.RegisterAssemblyTypes(Assembly.LoadFile(databaseLibraryPath))
                .Where(t => t.GetInterfaces()
                             .Where(i => t.Name.Contains(i.Name.Replace("DAL", "")
                                                               .Substring(1)))
                                               .Count() >= 1)
                .As(t => t.GetInterfaces()
                          .FirstOrDefault(i => t.Name.Contains(i.Name.Replace("DAL", "")
                                                                     .Substring(1))))
                .InstancePerLifetimeScope();
        }

        /// <summary>
        /// Create an autofac IoC container. This also configures the achievement hunter library.
        /// </summary>
        /// <param name="databaseLibraryPath">The path of the database access library file.</param>
        /// <param name="dataAccessConfigurationLibraryPath">The path of the data access configuration library file.</param>
        /// <returns>Returns an autofac container.</returns>
        public static IContainer BuildContainer(string databaseLibraryPath, string dataAccessConfigurationLibraryPath)
        {
            IContainer container = ConfigureBuilder(databaseLibraryPath, dataAccessConfigurationLibraryPath).Build();

            ConfigureAchievementHunter(container);

            return container;
        }
        /// <summary>
        /// Create an autofac IoC container. This also configures the achievement hunter library.
        /// </summary>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="databaseLibraryPath">The path of the database access library file.</param>
        /// <param name="dataAccessConfigurationLibraryPath">The path of the data access configuration library file.</param>
        /// <returns>Returns an autofac container.</returns>
        public static IContainer BuildContainer(ContainerBuilder builder, string databaseLibraryPath, string dataAccessConfigurationLibraryPath)
        {
            ConfigureBuilder(ref builder, databaseLibraryPath, dataAccessConfigurationLibraryPath);
            IContainer container = builder.Build();

            ConfigureAchievementHunter(container);

            return container;
        }
        #endregion

        #region Database Setup
        private static void ConfigureAchievementHunter(IContainer container)
        {
            IDBPrepDAL dBPrepDAL = container.Resolve<IDBPrepDAL>();
            dBPrepDAL.PrepareDatabase();

            IAchievementDAL achievementDAL = container.Resolve<IAchievementDAL>();
            achievementDAL.PopulateDatabase();
        }
        #endregion
    }
}
