using System.Text.RegularExpressions;

namespace utils
{
	public class IsHelper
	{
		/// <summary>
		/// 判断一个字符串是否为url
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool IsUrl(string str)
		{
			try
			{
				string Url = @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
				return Regex.IsMatch(str, Url);
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}