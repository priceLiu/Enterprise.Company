using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using log4net;

namespace CN100.EnterprisePlatform.Utility
{
    public class Log4NetUtil
    {
        public static readonly Log4NetUtil Instance = new Log4NetUtil();

        private ILog log;
        private bool initLog;

        private Log4NetUtil()
        {
            InitLog();
        }

        private void InitLog()
        {
            try
            {
                log = LogManager.GetLogger(typeof(Log4NetUtil));
                XmlDocument xml = new XmlDocument();
                string strPath = AppDomain.CurrentDomain.BaseDirectory;
                xml.Load(System.IO.Path.Combine(strPath, "Log4NetUtil.config"));
                XmlElement cfg = xml.DocumentElement;
                log4net.Config.XmlConfigurator.Configure(cfg);

                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                initLog = true;

                string msg = "Log4Net Start";
                Info(msg);                
            }
            catch
            {
                initLog = false;
            }
        }

        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.IsTerminating)
            {
                Fatal("Application Fatal!", new Exception(string.Format("Exception Object:{0} UnhandledExceptionEventArgs:{1}",e.ExceptionObject.ToString(),e.ToString())));
            }
            else
            {
                Error("Application Error!", new Exception(string.Format("Exception Object:{0} UnhandledExceptionEventArgs:{1}", e.ExceptionObject.ToString(), e.ToString())));
            }
        }

        public void Debug(string msg)
        {
            if (initLog)
            {
                if (log.IsDebugEnabled)
                    log.Debug(msg);
            }
        }

        public void Debug(string msg, Exception ex)
        {
            if (initLog)
            {
                if (log.IsDebugEnabled)
                    log.Debug(msg, ex);
            }
        }

        public void Info(string msg)
        {
            if (initLog)
            {
                if (log.IsInfoEnabled)
                    log.Info(msg);
            }
        }

        public void Info(string msg, Exception ex)
        {
            if (initLog)
            {
                if (log.IsInfoEnabled)
                    log.Info(msg, ex);
            }
        }

        public void Warn(string msg)
        {
            if (initLog)
            {
                if (log.IsWarnEnabled)
                    log.Warn(msg);
            }
        }

        public void Warn(string msg, Exception ex)
        {
            if (initLog)
            {
                if (log.IsWarnEnabled)
                    log.Warn(msg, ex);
            }
        }

        public void Error(string msg)
        {
            if (initLog)
            {
                if (log.IsErrorEnabled)
                    log.Error(msg);
            }
        }

        public void Error(string msg, Exception ex)
        {
            if (initLog)
            {
                if (log.IsErrorEnabled)
                    log.Error(msg, ex);
            }
        }

        public void Fatal(string msg)
        {
            if (initLog)
            {
                if (log.IsFatalEnabled)
                    log.Fatal(msg);
            }
        }

        public void Fatal(string msg, Exception ex)
        {
            if (initLog)
            {
                if (log.IsFatalEnabled)
                    log.Fatal(msg, ex);
            }
        }
    }
}
