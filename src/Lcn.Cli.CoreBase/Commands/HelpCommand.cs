using Lcn.Cli.CoreBase.Args;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Lcn.Cli.CoreBase.Commands
{
    public class HelpCommand : IConsoleCommand, ITransientDependency
    {
        public ILogger<HelpCommand> Logger { get; set; }
        protected IHybridServiceScopeFactory ServiceScopeFactory { get; }
        protected LcnCliCoreBaseOptions LcnCliCoreBaseOptions { get; }
        public HelpCommand(IOptions<LcnCliCoreBaseOptions> options, IHybridServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
            LcnCliCoreBaseOptions = options.Value;
            Logger = NullLogger<HelpCommand>.Instance;//默认空日志
        }
        public Task ExcuteAsync(CommandLineArgs commandLineArgs)
        {
            if (string.IsNullOrWhiteSpace(commandLineArgs.Target))
            {
                Logger.LogInformation(GetUsageInfo());
                return Task.CompletedTask;
            }

            if (!LcnCliCoreBaseOptions.Commands.ContainsKey(commandLineArgs.Target))
            {
                Logger.LogWarning($"找不到命令 {commandLineArgs.Target}.是否输入错误？");
                Logger.LogInformation(GetUsageInfo());
                return Task.CompletedTask;
            }

            var commandType = LcnCliCoreBaseOptions.Commands[commandLineArgs.Target];

            using (var scope = ServiceScopeFactory.CreateScope())
            {
                var command = (IConsoleCommand)scope.ServiceProvider.GetRequiredService(commandType);
                Logger.LogInformation(command.GetUsageInfo());
            }
            return Task.CompletedTask;
        }

        public string GetShortDescription()
        {
            return $"显示控制台帮助命令. 输入 ` {LcnCliCoreBaseOptions.ToolName} help <command> ` 查看命令帮助！";
        }

        public string GetUsageInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("使用例子:");
            sb.AppendLine("");
            sb.AppendLine($"    {LcnCliCoreBaseOptions.ToolName} <command> <target> [options]");
            sb.AppendLine("");
            sb.AppendLine("命令列表:");
            sb.AppendLine("");

            foreach (var command in LcnCliCoreBaseOptions.Commands.ToArray())
            {
                string shortDescription;

                using (var scope = ServiceScopeFactory.CreateScope())
                {
                    shortDescription = ((IConsoleCommand)scope.ServiceProvider
                            .GetRequiredService(command.Value)).GetShortDescription();
                }

                sb.Append("    > ");
                sb.Append(command.Key);
                sb.Append(string.IsNullOrWhiteSpace(shortDescription) ? "" : ":");
                sb.Append(" ");
                sb.AppendLine(shortDescription);
            }

            sb.AppendLine("");
            sb.AppendLine("了解命令的详细使用帮助，使用以下命令:");
            sb.AppendLine("");
            sb.AppendLine($"    {LcnCliCoreBaseOptions.ToolName} help <command>");
            sb.AppendLine("");
            sb.AppendLine($"查阅详细文档获取更新信息: {LcnCliCoreBaseOptions.ToolDocUrl}");

            return sb.ToString();
        }
    }
}
