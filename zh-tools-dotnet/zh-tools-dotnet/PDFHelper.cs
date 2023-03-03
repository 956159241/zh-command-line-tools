using PuppeteerSharp;
using PuppeteerSharp.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace zh_tools_dotnet
{
	public class PDFHelper
	{
		public static async Task Generate()
		{
			//await ScreenShots();
			await Pdf();
			//var html = "Hello World";
			//await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
			//var browser = await Puppeteer.LaunchAsync(new LaunchOptions
			//{
			//	Headless = true
			//});
			//var page = await browser.NewPageAsync();
			//await page.SetContentAsync(html);
			//var opt = new PdfOptions();
			//opt.PrintBackground = true;

			////var navOpts = new NavigationOptions();

			////navOpts.WaitUntil = new WaitUntilNavigation[] { WaitUntilNavigation.Load };
			////await page.WaitForNavigationAsync(navOpts);
			////await page.WaitForSelectorAsync("#loaded");
			//await page.PdfAsync(@$"D:\{DateTime.Now.ToLongDateString()}.pdf", opt);
		}

		// generate pic.

		private static async Task ScreenShots()
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

		// generate pdf.

		private static async Task Pdf() 
		{
			using var browserFetcher = new BrowserFetcher();
			await browserFetcher.DownloadAsync();
			var browser = await Puppeteer.LaunchAsync(new LaunchOptions
			{
				IgnoreHTTPSErrors=true,
				Headless = true,
				LogProcess = true,
				DumpIO = false,
				Args = new string[] { "--disable-gpu", "--disable-dev-shm-usage", "--disable-setuid-sandbox", "--no-first-run", "--no-sandbox", "--no-zygote", "--single-process" },
			}) ;
			//var page = await browser.NewPageAsync();
			var page = await browser.NewPageAsync();
			await page.SetContentAsync("<div id=\"body-template\" style=\"font-size:10px !important; color:#808080; padding-left:500px; page-break-before:always;\">body Text<div class=\"pageNumber\"></div> <div>/</div><div class=\"totalPages\"></div></div><div id=\"body-template\" style=\"font-size:10px !important; color:#808080; padding-left:500px; page-break-before:always;\">body Text</div><div id=\"body-template\" style=\"font-size:10px !important; color:#808080; padding-left:500px; page-break-before:always;\">body Text</div>");

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
	}
}