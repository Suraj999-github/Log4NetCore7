
using log4net;
using log4net.Config;
using Log4Net.LogUtility;
using System.Reflection;

var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("Log4Net.config"));
var demo = new Log4Logger();


demo.Info("Starting the console application");

try
{
    demo.Debug($"Starting {MethodBase.GetCurrentMethod()?.DeclaringType}");

    throw new Exception("Sample Error inside the try catch block code");
}
catch (Exception ex)
{

    demo.Error(ex.Message, ex.InnerException);
}

demo.Debug("Waiting for user input");
demo.Info("Ending application");
demo.Fatal("Ending Fatal application");
demo.Warn("Ending Warn application");
Console.ReadLine();

