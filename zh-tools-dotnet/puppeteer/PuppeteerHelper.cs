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
				Args = new string[] { "--disable-gpu", "--disable-dev-shm-usage", "--disable-setuid-sandbox", "--no-first-run", "--no-sandbox", "--no-zygote", "--single-process" },
			});
			//var page = await browser.NewPageAsync();
			var page = await browser.NewPageAsync();
			if (args.ContainsKey("input"))
			{
				var input = "";
				args.TryGetValue("input", out input);
				if (String.IsNullOrEmpty(input))
				{
					LogHelper.WriteLog("Please enter the correct command.", LogHelper.LogLevel.Info);
					return;
				}
				await setContent(page, input);
			}


			//await page.GoToAsync("https://www.iotzzh.com");
			await page.PdfAsync(@$"D:\{DateTime.Now.ToLongDateString()}.pdf", new PdfOptions
			{
				Format = PaperFormat.A4,
				DisplayHeaderFooter = true,
				MarginOptions = new MarginOptions
				{
					Top = "0px",
					Right = "0px",
					Bottom = "0px",
					Left = "0px"
				},
				//PrintBackground = true,
				HeaderTemplate = "<div id=\"header-template\" style=\"font-size:10px !important; color:#808080; padding-left:500px\">Header Text <span class=\"pageNumber\"></span> <span>/</span><span class=\"totalPages\"></span></div>",
				//PageRanges = "1-15",
				FooterTemplate = "<div id=\"footer-template\" style=\"font-size:10px !important; color:#808080; padding-left:50px\"><span class=\"pageNumber\"></span> <span>/</span><span class=\"totalPages\"></span></div>"
			}); // 生成PDF
		}

		private static async Task setContent(IPage page, String input) 
		{
			// 如果是url
			if (IsHelper.IsUrl(input))
			{
				await page.GoToAsync(input);
			}
			// 是否是文件
			else if (File.Exists(input))
			{

			}
			else 
			{
				await page.SetContentAsync(input);
			}
			//await page.SetContentAsync("<div id=\"body-template\" style=\"font-size:10px !important; color:#808080; padding-left:500px; page-break-before:always;\">body Text<div class=\"pageNumber\"></div> <div>/</div><div class=\"totalPages\"></div></div><div id=\"body-template\" style=\"font-size:10px !important; color:#808080; padding-left:500px; page-break-before:always;\">body Text</div><div id=\"body-template\" style=\"font-size:10px !important; color:#808080; padding-left:500px; page-break-before:always;\">body Text</div>");
		}
	}
}