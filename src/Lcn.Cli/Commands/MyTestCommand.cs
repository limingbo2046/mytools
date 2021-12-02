using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lcn.Cli.CoreBase.Args;
using Lcn.Cli.CoreBase.Commands;
using Microsoft.Extensions.Logging;

namespace Lcn.Cli.Commands
{
    public class MyTestCommand : IConsoleCommand
    {
        protected ILogger<MyTestCommand> Logger;

        public MyTestCommand(ILogger<MyTestCommand> logger)
        {
            Logger = logger;
        }

        public Task ExcuteAsync(CommandLineArgs commandLineArgs)
        {
            Logger.LogInformation("进入测试命令");
            return Task.CompletedTask;
        }

        public string GetShortDescription()
        {
            return "提示";
        }

        public string GetUsageInfo()
        {
            return "用例";
        }
    }
}
