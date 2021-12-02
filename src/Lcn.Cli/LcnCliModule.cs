using Lcn.Cli.Commands;
using Lcn.Cli.CoreBase;
using System;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Lcn.Cli
{
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(LcnCliCoreBaseModule))]
    public class LcnCliModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<LcnCliCoreBaseOptions>(options =>
            {
                options.ToolName = AppDomain.CurrentDomain.FriendlyName;//工具名字
                options.Commands["test"] = typeof(MyTestCommand);//添加命令
            });

        }
    }
}
