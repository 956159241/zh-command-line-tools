using System.Text;

namespace utils
{
	public static class StringHelper
	{
		/// <summary>
		/// 命名方式转换：横杠转驼峰命名
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
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