using Lcn.Cli.CoreBase.Args;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Lcn.Cli.CoreBase.Commands
{
    public class CommandSelector : ICommandSelector, ITransientDependency
    {
        protected LcnCliCoreBaseOptions Options { get; }
        public CommandSelector(IOptions<LcnCliCoreBaseOptions> options)//这个构造器是干嘛用的，新语法吗？
        {
            Options = options.Value;
        }
        public Type Select(CommandLineArgs commandLineArgs)
        {
            if (commandLineArgs.Command.IsNullOrWhiteSpace())
            {
                return typeof(HelpCommand);//如果命令是空，则返回帮助命令，提示
            }
            return Options.Commands.GetOrDefault(commandLineArgs.Command) ?? typeof(HelpCommand);//如果不是字典中的命令，也返回帮助命令，提示
        }
    }
}
