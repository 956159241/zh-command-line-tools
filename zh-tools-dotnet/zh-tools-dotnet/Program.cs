using log;
using zh_tools_dotnet;

LogHelper.WriteLog("Start", LogHelper.LogLevel.Info);
var dArgs = ArgsHelper.ConvertArgs(args);
if (dArgs is null)
{
	LogHelper.WriteLog("The parameter is invalid", LogHelper.LogLevel.Info);
}

await PDFHelper.Generate(dArgs);
LogHelper.WriteLog("End", LogHelper.LogLevel.Info);
Console.ReadLine();