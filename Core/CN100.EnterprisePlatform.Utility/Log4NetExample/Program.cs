
namespace Dotnet.Samples.Log4Net
{
    #region References
    using System;
    using System.Xml;
    using log4net;
    #endregion

    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("Log4NetConfig/Log4Net.config");
                XmlElement cfg = xml.DocumentElement;
                log4net.Config.XmlConfigurator.Configure(cfg);

                string msg = "Test Message.";
                if (log.IsDebugEnabled) log.Debug(msg);
                if (log.IsInfoEnabled) log.Info(msg);
                if (log.IsWarnEnabled) log.Warn(msg);
                if (log.IsErrorEnabled) log.Error(msg);
                if (log.IsFatalEnabled) log.Fatal(msg);
            }
            catch (Exception err)
            {
                Console.WriteLine("Exception caught: " + err.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey(true);
            }
        }
    }
}
