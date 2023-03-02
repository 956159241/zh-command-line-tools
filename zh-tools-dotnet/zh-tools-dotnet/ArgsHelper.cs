using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zh_tools_dotnet.model;

namespace zh_tools_dotnet
{
	internal static class ArgsHelper
	{
		internal static Dictionary<string, string>? ConvertArgs(string[] args) 
		{
			var result = new Dictionary<string, string>();
			for (var i = 0; i < args.Length; i++)
			{
				if (args[i].StartsWith("-"))
				{
					if (args[i + 1].StartsWith("-")) return null;
					result.Add(args[i], args[i + 1]);
					i++;
				}
				else 
				{
					if (!result.ContainsKey("body"))
					{
						result.Add("body", args[i]);
					}
					else if (!result.ContainsKey("output")) 
					{
						result.Add("output", args[i]);
					}
				}
			}
			return result;
		}
	}
}
 