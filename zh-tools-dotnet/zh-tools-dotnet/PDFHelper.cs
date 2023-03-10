using PuppeteerSharp;
using PuppeteerSharp.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using puppeteer;

namespace zh_tools_dotnet
{
	public class PDFHelper
	{
		public static async Task Generate(Dictionary<string, string> args)
		{
			await PuppeteerHelper.Pdf(args);
		}
	}
}