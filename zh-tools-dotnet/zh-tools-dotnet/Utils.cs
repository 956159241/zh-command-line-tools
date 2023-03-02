using System.Text;

namespace zh_tools_dotnet
{
	public static class Utils
	{
		public static string LineToHump(string name)
		{
			StringBuilder builder = new StringBuilder();
			foreach (var s in name.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries))
			{
				builder.Append(Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s));
			}

			return builder.ToString();
		}
	}
}