using Lcn.Cli.CoreBase.Args;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Lcn.Cli.CoreBase.Commands
{
    /// <summary>
    /// 命令全部继承该接口
    /// 控制台命令
    /// </summary>
    public interface IConsoleCommand : ITransientDependency
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="commandLineArgs">参数</param>
        /// <returns></returns>
        Task ExcuteAsync(CommandLineArgs commandLineArgs);
        /// <summary>
        /// 使用说明
        /// </summary>
        /// <returns></returns>
        string GetUsageInfo();
        /// <summary>
        /// 简短提示
        /// </summary>
        /// <returns></returns>
        string GetShortDescription();
    }
}
