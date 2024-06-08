using CodeGenerator.Core.Dialog;
using CodeGenerator.Lib.Options;
using CodeGenerator.Modules.Common;
using CodeGenerator.Modules.ModuleName;
using CodeGenerator.Services.Interface;
using CodeGenerator.Views;
using NLog;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows;
using Unity;
using Unity.Injection;
using Unity.RegistrationByConvention;

namespace CodeGenerator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected override Window CreateShell()
        {
            // 初始化 NLog
            LogManager.Setup().LoadConfigurationFromFile("NLog.config");
            var commonSvc = Container.Resolve<ICommonSvc>();
            commonSvc.CodeFirstInitTables();
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var container = containerRegistry.GetContainer();

            AppSetting appSetting = GetAppSetting();
            MenuOptions menu = GetMenuOptions();
            container.RegisterInstance<AppSetting>(appSetting);
            container.RegisterInstance<MenuOptions>(menu);

            // 注入ISqlSugarClient
            if (!Enum.TryParse<DbType>(appSetting.SqlSugar.DbType, true, out DbType dbType))
            {
                Logger.Error("读取SqlSugar配置异常");
            }
            else
            {
                container.RegisterInstance<ISqlSugarClient>(new SqlSugarClient(new ConnectionConfig
                {
                    DbType = dbType,
                    ConnectionString = appSetting.SqlSugar.ConnectionString,
                    IsAutoCloseConnection = appSetting.SqlSugar.IsAutoCloseConnection,
                    MoreSettings = new ConnMoreSettings()
                    {
                        SqliteCodeFirstEnableDropColumn = true, //只支持.net core
                        SqliteCodeFirstEnableDefaultValue = true,
                        SqliteCodeFirstEnableDescription = true
                    }
                }));
            }

            // 批量注入主要服务，包括Service和Repository层
            Register(container);

            container.RegisterType<IDialogHostService, DialogHostService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleCodeModule>();
            moduleCatalog.AddModule<CommonModule>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.Info("App Init!");
            Mutex mutex = new Mutex(true, "CodeGenerator", out bool createNew);
            if (!createNew)
            {
                MessageBox.Show("程序正在运行中");
                this.Shutdown();
                return;
            }
            base.OnStartup(e);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Logger.Info("App Run!");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Logger.Info("App Exit!");
            LogManager.Shutdown();
            base.OnExit(e);
        }

        private IUnityContainer Register(IUnityContainer container)
        {
            var assemblies = GetAssemblies();
            foreach (var assembly in assemblies)
            {
                container.RegisterTypes(
                        assembly.GetTypes(),
                        WithMappings.FromMatchingInterface,
                        WithName.Default,
                        WithLifetime.Transient); 
            }


            return container;
        }

        private List<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();
            assemblies.Add(Assembly.LoadFrom("CodeGenerator.Repository.dll"));
            assemblies.Add(Assembly.LoadFrom("CodeGenerator.Services.dll"));
            return assemblies;
        }

        private AppSetting GetAppSetting()
        {
            AppSetting appSetting = new AppSetting()
            {
                Title = "代码生成器",
                SqlSugar = new DbClient 
                {
                    DbType = "Sqlite",
                    ConnectionString = "DataSource=db/default.db;",
                    IsAutoCloseConnection = true,
                }
            };

            return appSetting;
        }

        private MenuOptions GetMenuOptions()
        {
            MenuOptions menuOptions = new MenuOptions()
            {
                Menus = new List<Menu>
            {
                new Menu
                {
                    Header = "首页",
                    Icon = "HomeAccount",
                    IsSelected = true,
                    View = "HomeView"
                },
                new Menu
                {
                    Header = "数据源",
                    Icon = "Database",
                    IsSelected = false,
                    View = "CodeDbView"
                },
                new Menu
                {
                    Header = "模板",
                    Icon = "ApplicationBracketsOutline",
                    IsSelected = false,
                    View = "CodeTempView"
                },
                new Menu
                {
                    Header = "模板方案",
                    Icon = "Animation",
                    IsSelected = false,
                    View = "CodeGenSchemeView"
                },
                new Menu
                {
                    Header = "生成",
                    Icon = "GeneratorStationary",
                    IsSelected = false,
                    View = "GenView"
                },
                new Menu
                {
                    Header = "生成记录",
                    Icon = "History",
                    IsSelected = false,
                    View = "CodeHisView"
                }
            }
            };
            return menuOptions;
        }
    }
}
