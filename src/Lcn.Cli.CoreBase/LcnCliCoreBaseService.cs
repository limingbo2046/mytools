using Lcn.Cli.CoreBase.Args;
using Lcn.Cli.CoreBase.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp;

namespace Lcn.Cli.CoreBase
{
    /// <summary>
    ///执行命令和参数解析，根据命令选择对应的服务
    /// </summary>
    public class LcnCliCoreBaseService : ITransientDependency//因为需要通过注入使用一些服务，所以要继承这个接口
    {
        public ILogger<LcnCliCoreBaseService> Logger { get; set; }//日志组件由属性注入来使用
        protected ICommandLineArgumentParser _commandLineArgumentParser { get; }
        protected ICommandSelector _commandSelector { get; }
        protected IHybridServiceScopeFactory _serviceScopeFactory { get; }
        public LcnCliCoreBaseService(ICommandLineArgumentParser commandLineArgumentParser, ICommandSelector commandSelector, IHybridServiceScopeFactory serviceScopeFactory)
        {
            _commandLineArgumentParser = commandLineArgumentParser;
            _commandSelector = commandSelector;
            _serviceScopeFactory = serviceScopeFactory;
            Logger = NullLogger<LcnCliCoreBaseService>.Instance;//初始化时，是空的，如果不通过属性注入，则没有日志
        }
        public async Task RunAsync(string[] args)
        {
            //把字符参数转成类属性
            var commamdLineArgs = _commandLineArgumentParser.Parser(args);
            //根据参数给的命令类型选择命令
            var commandType = _commandSelector.Select(commamdLineArgs);//如果转换成类后，没有命令，则默认执行帮助命令
            //根据命令的类型，定位到命令类，传入参数后，执行命令
            using (var scope = _serviceScopeFactory.CreateScope())//服务混合作用域，
            {
                var command = (IConsoleCommand)scope.ServiceProvider.GetRequiredService(commandType);//通过类型获取已经在容器的服务（这些服务在cli.core的模块启动里面注入了）
                try
                {
                    await command.ExcuteAsync(commamdLineArgs);//执行命令
                }
                catch (LcnCliCoreBaseUsageException cliex)
                {
                    Logger.LogWarning(cliex.Message);//记录告警信息
                }
                catch (Exception er)
                {
                    Logger.LogException(er);//记录出错的日志
                }
            }

        }
    }
}
