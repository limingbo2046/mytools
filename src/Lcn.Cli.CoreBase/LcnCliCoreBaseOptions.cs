using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lcn.Cli.CoreBase
{
   public class LcnCliCoreBaseOptions
    {
        public Dictionary<string, Type> Commands { get; }
        public bool CacheTemplates { get; set; } = true;
        public string ToolName { get; set; } = "CLI";//工具条的名称，一般指exe项目
        public string ToolDocUrl { get; set; } = "http://localhost";//工具帮助文档连接
        public LcnCliCoreBaseOptions()
        {
            Commands = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);//在初始化的时候，通过该选项保存其字典？
        }
    }
}
