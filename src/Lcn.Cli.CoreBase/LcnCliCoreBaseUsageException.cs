using System;
using System.Collections.Generic;
using System.Text;

namespace Lcn.Cli.CoreBase
{
    /// <summary>
    /// 用例错误
    /// 非致命错误
    /// </summary>
    public class LcnCliCoreBaseUsageException : Exception
    {
        public LcnCliCoreBaseUsageException(string message) : base(message)
        {

        }
        public LcnCliCoreBaseUsageException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
