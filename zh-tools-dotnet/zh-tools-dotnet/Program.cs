using log;


var stringArgs = String.Join(",", args);
Console.WriteLine("Args", stringArgs );

Console.WriteLine("Hello, World!");

LogHelper.WriteLog("Hello World", LogHelper.LogLevel.Info);
