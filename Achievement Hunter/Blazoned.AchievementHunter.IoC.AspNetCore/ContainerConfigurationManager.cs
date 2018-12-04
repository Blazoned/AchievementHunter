using System.Linq;
using System.Reflection;
using Autofac;

namespace Blazoned.AchievementHunter.IoC.AspNetCore
{
    public static class AchievementHunterContainerConfigurationManager
    {
        /// <summary>
        /// Creates a new container builder and adds the achievement hunter library to it.
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
        /// Configures an already existing container builder by adding the achievement hunter library to it.
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
        /// Create an autofac IoC container.
        /// </summary>
        /// <param name="databaseLibraryPath">The path of the database access library file.</param>
        /// <param name="dataAccessConfigurationLibraryPath">The path of the data access configuration library file.</param>
        /// <returns>Returns an autofac container.</returns>
        public static IContainer ConfigureContainer(string databaseLibraryPath, string dataAccessConfigurationLibraryPath)
        {
            return ConfigureBuilder(databaseLibraryPath, dataAccessConfigurationLibraryPath).Build();
        }
        /// <summary>
        /// Create an autofac IoC container.
        /// </summary>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="databaseLibraryPath">The path of the database access library file.</param>
        /// <param name="dataAccessConfigurationLibraryPath">The path of the data access configuration library file.</param>
        /// <returns>Returns an autofac container.</returns>
        public static IContainer ConfigureContainer(ContainerBuilder builder, string databaseLibraryPath, string dataAccessConfigurationLibraryPath)
        {
            ConfigureBuilder(ref builder, databaseLibraryPath, dataAccessConfigurationLibraryPath);

            return builder.Build();
        }
    }
}
