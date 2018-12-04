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
        /// <param name="databaseLibraryNamespace">The namespace of the database access library.</param>
        /// <param name="dataAccessConfigurationLibraryNamespace">The namespace of the data access configuration library.</param>
        /// <returns>Returns an autofac builder populated with the configuration container.</returns>
        public static ContainerBuilder ConfigureBuilder(string databaseLibraryNamespace, string dataAccessConfigurationLibraryNamespace)
        {
            ContainerBuilder builder = new ContainerBuilder();

            ConfigureBuilder(ref builder, databaseLibraryNamespace, dataAccessConfigurationLibraryNamespace);

            return builder;
        }
        /// <summary>
        /// Configures an already existing container builder by adding the achievement hunter library to it.
        /// </summary>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="databaseLibraryNamespace">The namespace of the database access library.</param>
        /// <param name="dataAccessConfigurationLibraryNamespace">The namespace of the data access configuration library.</param>
        public static void ConfigureBuilder(ref ContainerBuilder builder, string databaseLibraryNamespace, string dataAccessConfigurationLibraryNamespace)
        {
            if (builder == null)
                builder = new ContainerBuilder();

            // Register the achievement manager
            builder.RegisterType<AchievementManager>().InstancePerLifetimeScope();

            // Register the data access configuration
            builder.RegisterAssemblyTypes(Assembly.Load(dataAccessConfigurationLibraryNamespace))
                .Where(t => t.GetInterfaces()
                             .Where(i => t.Name.Contains(i.Name.Substring(1)))
                                               .Count() >= 1)
                .As(t => t.GetInterfaces()
                          .FirstOrDefault(i => t.Name.Contains(i.Name.Substring(1))))
                .InstancePerLifetimeScope();

            // Register the data access layer
            builder.RegisterAssemblyTypes(Assembly.Load(databaseLibraryNamespace))
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
        /// <param name="databaseLibraryNamespace">The namespace of the database access library.</param>
        /// <param name="dataAccessConfigurationLibraryNamespace">The namespace of the data access configuration library.</param>
        /// <returns>Returns an autofac container.</returns>
        public static IContainer ConfigureContainer(string databaseLibraryNamespace, string dataAccessConfigurationLibraryNamespace)
        {
            return ConfigureBuilder(databaseLibraryNamespace, dataAccessConfigurationLibraryNamespace).Build();
        }
        /// <summary>
        /// Create an autofac IoC container.
        /// </summary>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="databaseLibraryNamespace">The namespace of the database access library.</param>
        /// <param name="dataAccessConfigurationLibraryNamespace">The namespace of the data access configuration library.</param>
        /// <returns>Returns an autofac container.</returns>
        public static IContainer ConfigureContainer(ContainerBuilder builder, string databaseLibraryNamespace, string dataAccessConfigurationLibraryNamespace)
        {
            ConfigureBuilder(ref builder, databaseLibraryNamespace, dataAccessConfigurationLibraryNamespace);

            return builder.Build();
        }
    }
}
