using HtmlAgilityPack;
using log;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using utils;

namespace puppeteer
{
	public class PuppeteerHelper
	{
		public static async Task ScreenShots()
		{
			using var browserFetcher = new BrowserFetcher();
			await browserFetcher.DownloadAsync();

			var browser = await Puppeteer.LaunchAsync(new LaunchOptions
			{
				Headless = true
			});

			var page = await browser.NewPageAsync();
			await page.GoToAsync("https://www.iotzzh.com");
			await page.ScreenshotAsync(@$"D:\{DateTime.Now.ToLongDateString()}.png"); // 生成图片
		}

		public static async Task Pdf(Dictionary<string, string> args)
		{
			using var browserFetcher = new BrowserFetcher();
			await browserFetcher.DownloadAsync();
			var browser = await Puppeteer.LaunchAsync(new LaunchOptions
			{
				IgnoreHTTPSErrors = true,
				Headless = true,
				LogProcess = true,
				DumpIO = false,
				//Args = new string[] { "--disable-gpu", "--disable-dev-shm-usage", "--disable-setuid-sandbox", "--no-first-run", "--no-sandbox", "--no-zygote", "--single-process" },
			});
			//var page = await browser.NewPageAsync();
			var page = await browser.NewPageAsync();
			if ((args.ContainsKey("input") && !args.ContainsKey("output")) || (!args.ContainsKey("input") && args.ContainsKey("output")))
			{
				LogHelper.WriteLog("Please enter the correct command.", LogHelper.LogLevel.Info);
				return;
			}

			if (args.ContainsKey("input"))
			{
				var input = "";
				args.TryGetValue("input", out input);
				if (String.IsNullOrEmpty(input))
				{
					LogHelper.WriteLog("Please enter the correct command. Not Found Input.", LogHelper.LogLevel.Info);
					return;
				}
				await setContent(page, input);
			}

			if (args.ContainsKey("output"))
			{
				var output = "";
				args.TryGetValue("output", out output);
				if (string.IsNullOrEmpty(output))
				{
					LogHelper.WriteLog("Please enter the correct command. Not Found output.", LogHelper.LogLevel.Info);
					return;
				}

				var headerTemplate = @$"
					<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      #header {{
        padding: 0;
clear:both;
      }}

      #head {{
        padding: 0;
      }}
      .content-header {{
        width: 100%;
        font-family:Arial, Helvetica, sans-serif;
        background-color: white;
        color: black;
        padding: 5px;
        -webkit-print-color-adjust: exact;
        vertical-align: middle;
        font-size: 12px;
        margin-top: 0;
        display: block;
        text-align: center;
        border-bottom: 1px solid lightgray;
clear:both;
      }}
    </style>
  </head>
  <body style=""font-size: 10px;color: #999; margin: 15px 0;clear:both; position: relative; top: 20px;"">
    <div class=""content-header"" style=""font-size: 10px;color: #999; margin: 15px 0;clear:both; position: relative; top: 20px;"">
		HEader Page <span class=""pageNumber""></span> of <span class=""totalPages""></span>
    </div>
  </body>
</html>
					";

				var footerTemplate = @$"
					<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      #footer {{
        padding: 0;
      }}
      .content-footer {{
        width: 100%;
        font-family:Arial, Helvetica, sans-serif;
        background-color: white;
        color: black;
        padding: 5px;
        -webkit-print-color-adjust: exact;
        vertical-align: middle;
        font-size: 12px;
        margin-top: 0;
        display: inline-block;
        text-align: center;
        border-top: 1px solid lightgray;
      }}
    </style>
  </head>
  <body>
    <div class=""content-footer"">
      Page <span class=""pageNumber""></span> of <span class=""totalPages""></span>
    </div>
  </body>
</html>
					";

				//var doc = new HtmlDocument();
				//doc.LoadHtml(headerTemplate);



				await page.PdfAsync(@$"{output}", new PdfOptions
				{
					Format = PaperFormat.A4,
					DisplayHeaderFooter = true,
					PrintBackground = true,
					MarginOptions = new MarginOptions() { Top = "10px", },
					Landscape = true,
					HeaderTemplate = headerTemplate,
					//PageRanges = "1-15",
					FooterTemplate = footerTemplate
				});
			}
		}

		private static async Task setContent(IPage page, String input)
		{
			// is url; is file
			if (IsHelper.IsUrl(input) || File.Exists($@"{input}"))
			{
				await page.GoToAsync(input);
			}
			else
			{
				await page.SetContentAsync(input);
			}
		}
	}
}