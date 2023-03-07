using PuppeteerSharp;

namespace puppeteer
{
	public class PuppeteerHelper
	{
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
	}
}