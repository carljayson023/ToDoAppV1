using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using ToDoApp_v1._2.Controllers;
using ToDoApp_v1._2.Model;
using ToDoApp_v1._2.Repository;

namespace ToDoApp_v1._2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ItemController>().As<IItemController>();
            builder.RegisterType<ListController>().As<IListController>();
            builder.RegisterType<DatalistRepository>().As<IDetalistRepository>();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<DataDbContext>().AsSelf();

            //builder.RegisterAssemblyTypes(Assembly.Load(nameof(DemoLibrary)))
            //    .Where(t => t.Namespace.Contains("Utilities"))
            //    .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));
            
            return builder.Build();


        }
        private void OnStartup(object sender, StartupEventArgs e)
        {

            var container = Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var mainWindows = scope.Resolve<MainWindow>();
                mainWindows.Show();
            }
        }

    }
}
