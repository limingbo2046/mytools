using Lcn.Cli.CoreBase.Args;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lcn.Cli.CoreBase.Commands
{
    public interface ICommandSelector
    {
        Type Select(CommandLineArgs commandLineArgs);
    }
}
