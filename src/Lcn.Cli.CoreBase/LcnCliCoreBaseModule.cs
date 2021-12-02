using System;
using Volo.Abp.Modularity;
using Volo.Abp.Autofac;

namespace Lcn.Cli.CoreBase
{
    [DependsOn(typeof(AbpAutofacModule))]
    public class LcnCliCoreBaseModule : AbpModule
    {

    }
}
