using PuppeteerSharp;

namespace zh_tools_dotnet
{
	public class PDFHelper
	{
		public static async Task Generate()
		{
			var html = "Hello World";
			await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
			var browser = await Puppeteer.LaunchAsync(new LaunchOptions
			{
				Headless = true
			});
			var page = await browser.NewPageAsync();
			await page.SetContentAsync(html);
			var opt = new PdfOptions();
			opt.PrintBackground = true;

			//var navOpts = new NavigationOptions();

			//navOpts.WaitUntil = new WaitUntilNavigation[] { WaitUntilNavigation.Load };
			//await page.WaitForNavigationAsync(navOpts);
			//await page.WaitForSelectorAsync("#loaded");
			await page.PdfAsync(@$"D:\{DateTime.Now.ToLongDateString()}.pdf", opt);
		}
	}
}