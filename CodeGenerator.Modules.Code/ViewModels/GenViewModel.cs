using CodeGenerator.Core.Dialog;
using CodeGenerator.Core.Event;
using CodeGenerator.Core.Mvvm;
using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using CodeGenerator.Data.Entity.DatabaseEntity;
using CodeGenerator.Services;
using CodeGenerator.Services.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jt.Common.Tool.Extension;
using Jt.Common.Tool.Helper;
using Prism.Events;
using Prism.Regions;
using SqlSugar;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;

namespace CodeGenerator.Modules.Code.ViewModels
{
    public partial class GenViewModel : RegionViewModelBase
    {
        [ObservableProperty]
        private CodeDb _selectCodeDb;

        [ObservableProperty]
        private ObservableCollection<CodeDb> _codeDbs;

        [ObservableProperty]
        private string _selectDatabase;

        [ObservableProperty]
        private ObservableCollection<string> _databases;

        [ObservableProperty]
        private string _selectTable;

        [ObservableProperty]
        private string _entityName;

        [ObservableProperty]
        private ObservableCollection<string> _tables;

        [ObservableProperty]
        private ObservableCollection<DbFieldInfoDto> _dbFields;

        [ObservableProperty]
        private CodeGenScheme _selectScheme;

        [ObservableProperty]
        private ObservableCollection<CodeGenScheme> _schemes;

        [ObservableProperty]
        private ObservableCollection<CodeSchemeDetialsDto> _schemesDetials;

        [ObservableProperty]
        private bool _genBtnIndeterminate = false;

        [ObservableProperty]
        private string _genBtnText = "生成";

        [ObservableProperty]
        private bool _genBtnEnable = true;

        [ObservableProperty]
        private bool isConnected;

        [ObservableProperty]
        private string connectBtnToolTips = "点击连接数据源";

        [ObservableProperty]
        private bool connectEnable = false;

        [ObservableProperty]
        private string connectBtnText = "连接";

        [ObservableProperty]
        private bool _connectBtnIndeterminate;

        private string _savePath = "";

        private ICodeDbSvc _codeDbSvc;
        private ICodeGenSchemeSvc _codegenSchemeSvc;
        private IEventAggregator _eventAggregator;
        private ICodeGeneratorSvc _codegenSvc;
        private IDialogHostService _dialogHostService;
        private ICodeSchemeDetialsSvc _codeSchemeDetialsSvc;
        private ICodeTempSvc _codeTempSvc;
        private ICodeHisSvc _codeHisSvc;

        public GenViewModel(IRegionManager regionManager, ICodeDbSvc codeDbSvc, ICodeGenSchemeSvc codegenSchemeSvc, IEventAggregator eventAggregator, IDialogHostService dialogHostService, ICodeSchemeDetialsSvc codeSchemeDetialsSvc, ICodeTempSvc codeTempSvc, ICodeHisSvc codeHisSvc) : base(regionManager)
        {
            _codeDbSvc = codeDbSvc;
            _codegenSchemeSvc = codegenSchemeSvc;
            _eventAggregator = eventAggregator;
            _dialogHostService = dialogHostService;
            _codeSchemeDetialsSvc = codeSchemeDetialsSvc;
            _codeTempSvc = codeTempSvc;
            _codeHisSvc = codeHisSvc;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            await Load();
        }

        private async Task Load()
        {
            CodeDbs = new ObservableCollection<CodeDb>();
            var data = await _codeDbSvc.GetListAsync(X => X.IsDel == 0);

            if (data.IsNotNullOrEmpty())
            {
                for (int i = 0; i < data.Count; i++)
                {
                    CodeDbs.Add(data[i]);
                }

                SelectCodeDb = CodeDbs[0];

                ConnectEnable = true;
            }

        }

        [RelayCommand]
        private async Task Connect()
        {
            try
            {
                ConnectBtnIndeterminate = true;
                if (SelectCodeDb == null)
                {
                    throw new Exception("请先选择数据源");
                }

                if (!IsConnected)
                {
                    if (!Enum.TryParse<DbType>(SelectCodeDb.Type, true, out DbType dbType))
                    {
                        throw new Exception("数据源类型有误");
                    }
                    _codegenSvc = new CodeGeneratorSvc(dbType, SelectCodeDb.ConStr);

                    var dbs = await _codegenSvc.GetDataBasesAsync();
                    Databases = new ObservableCollection<string>();
                    foreach (var item in dbs)
                    {
                        Databases.Add(item.DataBase);
                    }

                    await RefreshScheme();

                    ConnectBtnText = "已连接";
                    ConnectBtnToolTips = "点击断开连接";
                    IsConnected = true;
                }
                else
                {
                    ConnectBtnToolTips = "点击连接数据源";
                    ConnectBtnText = "连接";
                    IsConnected = false;
                    Databases = new ObservableCollection<string>();
                    SelectDatabase = null;
                    Tables = new ObservableCollection<string>();
                    SelectTable = null;
                    DbFields = new ObservableCollection<DbFieldInfoDto>();
                    EntityName = null;

                    SelectScheme = null;
                    Schemes = new ObservableCollection<CodeGenScheme>();
                    SchemesDetials = new ObservableCollection<CodeSchemeDetialsDto>();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Connect");
                _eventAggregator.SendMessage(ex.ToString());
            }
            finally
            {
                ConnectBtnIndeterminate = false;
            }
        }

        [RelayCommand]
        private async Task SelectedDatabase()
        {
            try
            {
                if (!IsConnected)
                {
                    return;
                }

                if (SelectDatabase == null)
                {
                    _eventAggregator.SendMessage("请选择数据库");
                    return;
                }

                var tables = await _codegenSvc.GetTableNamesAsync(SelectDatabase);
                Tables = new ObservableCollection<string>();
                foreach (var item in tables)
                {
                    Tables.Add(item.TableName);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }
        }

        [RelayCommand]
        private async Task SelectedTable()
        {
            try
            {
                if (!IsConnected)
                {
                    return;
                }
                if (SelectDatabase == null)
                {
                    _eventAggregator.SendMessage("请选择数据库");
                    return;
                }

                if (SelectTable == null)
                {
                    _eventAggregator.SendMessage("请选择数据表");
                    return;
                }

                EntityName = NamedHelper.ToPascal(SelectTable);

                var fiedies = await _codegenSvc.GetDbFieldsAsync(SelectDatabase, SelectTable);
                DbFields = new ObservableCollection<DbFieldInfoDto>();
                foreach (var item in fiedies)
                {
                    var addItem = item.CopyValue<DbFieldInfo, DbFieldInfoDto>();
                    addItem.IsSelected = true;
                    DbFields.Add(addItem);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }
        }

        [RelayCommand]
        private async Task SelectedScheme()
        {
            try
            {
                if (!IsConnected)
                {
                    return;
                }

                if (SelectScheme == null)
                {
                    return;
                }

                if (EntityName.IsNullOrWhiteSpace())
                {
                    _eventAggregator.SendMessage("请填写实体类名");
                }

                if (SelectTable.IsNullOrWhiteSpace())
                {
                    _eventAggregator.SendMessage("请选择数据表");
                }

                SchemesDetials = new ObservableCollection<CodeSchemeDetialsDto>();
                var schemeDetials = await _codeSchemeDetialsSvc.GetDetialsBySchemeId(SelectScheme.Id);
                foreach (var item in schemeDetials)
                {
                    item.FileName = item.FileName?.Replace("{ClassName}", EntityName).Replace("{TableName}", SelectTable);
                    item.IsSelected = true;
                    SchemesDetials.Add(item);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }
        }

        [RelayCommand]
        private async Task GenBtn()
        {
            if (!GenBtnIndeterminate)
            {
                try
                {
                    GenBtnIndeterminate = true;
                    GenBtnText = "生成中...";
                    GenBtnEnable = false;
                    if (SelectDatabase.IsNullOrWhiteSpace())
                    {
                        _eventAggregator.SendMessage("请选择数据库");
                        return;
                    }
                    if (SelectTable.IsNullOrWhiteSpace())
                    {
                        _eventAggregator.SendMessage("请选择数据表");
                        return;
                    }
                    if (EntityName.IsNullOrWhiteSpace())
                    {
                        _eventAggregator.SendMessage("请填写实体类名");
                        return;
                    }
                    if (SelectScheme == null)
                    {
                        _eventAggregator.SendMessage("请选择方案");
                        return;
                    }


                    var selectSchemeDetials = SchemesDetials.Where(x => x.IsSelected).ToList();
                    if (!selectSchemeDetials.Any())
                    {
                        _eventAggregator.SendMessage("请选择模板");
                        return;
                    }
                    var dbfields = DbFields.Where(x => x.IsSelected).ToList();
                    var temps = await _codeTempSvc.GetCodeTempsByIdsAsync(selectSchemeDetials.Select(x => x.TempId).ToArray());
                    CodeTempParamsDto codeTempParamsDto = new CodeTempParamsDto();
                    codeTempParamsDto.ClassName = EntityName;
                    codeTempParamsDto.TableName = SelectTable;
                    codeTempParamsDto.Temps = selectSchemeDetials;
                    codeTempParamsDto.CodeSchemeId = SelectScheme.Id;
                    codeTempParamsDto.CodeSchemeName = SelectScheme.Name;
                    codeTempParamsDto.DbFieldInfos = dbfields;
                    var paths = await _codegenSvc.CodeGenerateAsync(temps, codeTempParamsDto);
                    _savePath = paths.AbsolutePath;


                    CodeHis codeHis = new CodeHis()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = paths.RelativePath,
                        FilePath = paths.RelativePath + ".zip",
                        IsDel = 0,
                        Creater = "sys",
                        CreateTime = DateTime.Now,
                        Updater = "sys",
                        UpTime = DateTime.Now,
                    };

                    await _codeHisSvc.InsertAsync(codeHis);

                    _eventAggregator.SendMessage("生成成功！");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "GenBtn");
                    _eventAggregator.SendMessage("生成失败");
                }
                finally
                {
                    GenBtnIndeterminate = false;
                    GenBtnText = "生成";
                    GenBtnEnable = true;
                }
            }
            else
            {
                GenBtnIndeterminate = false;

            }
        }

        [RelayCommand]
        private async Task RefreshScheme()
        {
            try
            {
                Schemes = new ObservableCollection<CodeGenScheme>();
                var scheme = await _codegenSchemeSvc.GetListAsync(x => x.IsDel == 0);
                foreach (var item in scheme)
                {
                    Schemes.Add(item);
                }

                SchemesDetials = new ObservableCollection<CodeSchemeDetialsDto>();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }
        }

        [RelayCommand]
        private void Export()
        {
            try
            {
                if (_savePath.IsNullOrWhiteSpace())
                {
                    _eventAggregator.SendMessage("请先生成代码");
                    return;
                }

                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.ShowNewFolderButton = true;
                var result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(_savePath);
                    string desFilepath = Path.Combine(folderBrowserDialog.SelectedPath, fileInfo.Name);
                    fileInfo.CopyTo(desFilepath);
                    _eventAggregator.SendMessage("导出成功");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }
        }



    }
}