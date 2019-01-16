using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Xml.Linq;
using log4net;
using System.Xml.Serialization;
using System.Collections.Generic;
using Talent.Core;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Talent.Diagnostics
{
    /// <summary>
    /// Represents diagnostics logging
    /// </summary>
    /// 20/04/2009
    public static class Logging
    {
        private const string STR_Unknown = "Unknown";
        private static readonly ILog _log;

        /// <summary>
        /// static constructor
        /// </summary>
        /// 20/05/2009
        static Logging()
        {
            _log = LogManager.GetLogger(GetProperLoggerName());
        }

        /// <summary>
        /// Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.
        /// </value>
        /// 21/05/2009
        public static bool IsDebugEnabled
        {
            get
            {
                return _log.IsDebugEnabled;
            }
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Debug(string message)
        {
            if (_log.IsDebugEnabled)
            {
                PopulateCallContext(null);
                _log.Debug(message);
            }
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        /// 11/11/2009
        public static void DebugFormat(string message, params object[] args)
        {
            if (_log.IsDebugEnabled)
            {
                PopulateCallContext(null);
                try
                {
                    _log.DebugFormat(message, args);
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.Message);
                    System.Diagnostics.Debug.WriteLine(x.Message);
                }
            }
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void Debug(string message, Exception exception)
        {
            if (_log.IsDebugEnabled)
            {
                PopulateCallContext(null);
                _log.Debug(message, GetExceptionThrown(exception));
            }
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="xml">The XML.</param>
        /// 22/06/2009
        public static void Debug(string message, Exception exception, XElement xml)
        {
            if (_log.IsDebugEnabled)
            {
                PopulateCallContext(xml);
                _log.Debug(message, GetExceptionThrown(exception));
            }
        }

        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Info(string message)
        {
            if (_log.IsInfoEnabled)
            {
                PopulateCallContext(null);
                _log.Info(message);
            }
        }

        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        /// 11/11/2009
        public static void InfoFormat(string message, params object[] args)
        {
            if (_log.IsInfoEnabled)
            {
                PopulateCallContext(null);
                try
                {
                    _log.InfoFormat(message, args);
                }
                catch (Exception x)
                {

                    Console.WriteLine(x.Message);
                    System.Diagnostics.Debug.WriteLine(x.Message);
                }
            }
        }

        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void Info(string message, Exception exception)
        {
            if (_log.IsInfoEnabled)
            {
                PopulateCallContext(null);
                _log.Info(message, GetExceptionThrown(exception));
            }
        }

        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="xml">The XML.</param>
        public static void Info(string message, Exception exception, XElement xml)
        {
            if (_log.IsInfoEnabled)
            {
                PopulateCallContext(xml);
                _log.Info(message, GetExceptionThrown(exception));
            }
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Warn(string message)
        {
            if (_log.IsWarnEnabled)
            {
                PopulateCallContext(null);
                _log.Warn(message);
            }
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        /// 11/11/2009
        public static void WarnFormat(string message, params object[] args)
        {
            if (_log.IsWarnEnabled)
            {
                PopulateCallContext(null);

                try
                {
                    _log.WarnFormat(message, args);
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.Message);
                    System.Diagnostics.Debug.WriteLine(x.Message);
                }
            }
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void Warn(string message, Exception exception)
        {
            if (_log.IsWarnEnabled)
            {
                PopulateCallContext(null);
                _log.Warn(message, GetExceptionThrown(exception));
            }
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="xml">The XML.</param>
        public static void Warn(string message, Exception exception, XElement xml)
        {
            if (_log.IsWarnEnabled)
            {
                PopulateCallContext(xml);
                _log.Warn(message, GetExceptionThrown(exception));
            }
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Error(string message)
        {
            if (_log.IsErrorEnabled)
            {
                PopulateCallContext(null);
                _log.Error(message);
            }
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// 11/11/2009
        public static void ErrorFormat(string message, params object[] args)
        {
            if (_log.IsErrorEnabled)
            {
                PopulateCallContext(null);
                try
                {
                    _log.ErrorFormat(message, args);
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.Message);
                    System.Diagnostics.Debug.WriteLine(x.Message);
                }
            }
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void Error(string message, Exception exception)
        {
            if (_log.IsErrorEnabled)
            {
                PopulateCallContext(null);
                _log.Error(message, GetExceptionThrown(exception));
            }
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="xml">The XML.</param>
        public static void Error(string message, Exception exception, XElement xml)
        {
            if (_log.IsErrorEnabled)
            {
                PopulateCallContext(xml);
                _log.Error(message, GetExceptionThrown(exception));
            }
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Fatal(string message)
        {
            if (_log.IsFatalEnabled)
            {
                PopulateCallContext(null);
                _log.Fatal(message);
            }
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        /// 11/11/2009
        public static void FatalFormat(string message, params object[] args)
        {
            if (_log.IsFatalEnabled)
            {
                PopulateCallContext(null);
                try
                {
                    _log.FatalFormat(message, args);
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.Message);
                    System.Diagnostics.Debug.WriteLine(x.Message);
                }
            }
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void Fatal(string message, Exception exception)
        {
            if (_log.IsFatalEnabled)
            {
                PopulateCallContext(null);
                _log.Fatal(message, GetExceptionThrown(exception));
            }
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="xml">The XML.</param>
        public static void Fatal(string message, Exception exception, XElement xml)
        {
            if (_log.IsFatalEnabled)
            {
                PopulateCallContext(xml);
                _log.Fatal(message, GetExceptionThrown(exception));
            }
        }

        /// <summary>
        /// Populates the call context.
        /// </summary>
        /// <param name="xml">The XML.</param>
        public static void PopulateCallContext(XElement xml)
        {
            var stackFrame = new StackFrame(2, false);

            //LogicalThreadContext.Properties["loggedOnTime"] = DateTime.Now;

            var methodBase = stackFrame.GetMethod();

            if (methodBase != null)
            {
                LogicalThreadContext.Properties["module"] = methodBase.Module.ToString();
                LogicalThreadContext.Properties["class"] = methodBase.DeclaringType.ToString();
            }


            LogicalThreadContext.Properties["server"] = Environment.MachineName;
            LogicalThreadContext.Properties["xml"] = xml == null ? null : xml.ToString();

            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.User != null &&
                    HttpContext.Current.User.Identity != null &&
                    !string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                {
                    LogicalThreadContext.Properties["user"] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    LogicalThreadContext.Properties["user"] = STR_Unknown;
                }

                LogicalThreadContext.Properties["webInfo"] = WebInfo.Get();
            }
            else if (Thread.CurrentPrincipal != null)
            {
                if (Thread.CurrentPrincipal.Identity != null &&
                    !string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name))
                {
                    LogicalThreadContext.Properties["user"] = Thread.CurrentPrincipal.Identity.Name;
                }
                else
                {
                    LogicalThreadContext.Properties["user"] = STR_Unknown;
                }

                LogicalThreadContext.Properties["webInfo"] = null;
            }
            else if (WindowsIdentity.GetCurrent() != null)
            {
                if (string.IsNullOrEmpty(WindowsIdentity.GetCurrent().Name))
                {
                    LogicalThreadContext.Properties["user"] = WindowsIdentity.GetCurrent().Name;
                }
                else
                {
                    LogicalThreadContext.Properties["user"] = STR_Unknown;
                }

                LogicalThreadContext.Properties["webInfo"] = null;
            }
        }

        /// <summary>
        /// Gets the name of the proper logger.
        /// </summary>
        /// <returns>The object of <see cref="String"/>
        /// </returns>
        /// 19/05/2009
        private static string GetProperLoggerName()
        {
            return Assembly.GetEntryAssembly() != null ? Assembly.GetEntryAssembly().GetName().Name
                    : new StackFrame(3, false).GetMethod().Module.Assembly.GetName().Name;
        }

        /// <summary>
        /// Gets the exception thrown.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private static Exception GetExceptionThrown(Exception exception)
        {
            if (exception == null)
            {
                return null;
            }

            if (exception is TargetInvocationException && exception.InnerException != null)
            {
                return GetExceptionThrown(exception.InnerException);
            }

            return exception;
        }
    }


    /// <summary>
    /// Represents web info
    /// </summary>
    /// 23/06/2009
    [XmlType("Info")]
    public class WebInfo
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [XmlElement("Url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the IP.
        /// </summary>
        /// <value>The IP.</value>
        [XmlElement("IP")]
        public string IP { get; set; }

        /// <summary>
        /// Gets or sets the referer.
        /// </summary>
        /// <value>The referer.</value>
        [XmlElement("Referer")]
        public string Referer { get; set; }

        /// <summary>
        /// Gets or sets the langs.
        /// </summary>
        /// <value>The langs.</value>
        [XmlElement("Langs")]
        public string Langs { get; set; }

        /// <summary>
        /// Gets or sets the agent.
        /// </summary>
        /// <value>The agent.</value>
        [XmlElement("Agent")]
        public string Agent { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        /// <value>The headers.</value>
        [XmlArray("Headers")]
        public Field[] Fields { get; set; }


        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>The object of <see cref="WebInfo"/>
        /// </returns>
        public static WebInfo Get()
        {
            try
            {
                if (HttpContext.Current == null || HttpContext.Current.Request == null)
                {
                    return null;
                }

                var fields = new List<Field>();
                var headers = HttpContext.Current.Request.Headers;

                if (!headers.IsNullOrEmpty())
                {
                    for (int i = 0; i < headers.Count; i++)
                    {
                        fields.Add(new Field { Name = headers.GetKey(i), Value = headers.Get(i) });
                    }
                }

                return new WebInfo
                {
                    IP = HttpContext.Current.Request.UserHostAddress,
                    Referer = HttpContext.Current.Request.UrlReferrer == null ? string.Empty : HttpContext.Current.Request.UrlReferrer.AbsolutePath,
                    Agent = HttpContext.Current.Request.UserAgent,
                    Url = HttpContext.Current.Request.Url == null ?
                            string.Empty : HttpContext.Current.Request.Url.ToString(),
                    Langs = HttpContext.Current.Request.UserLanguages.IsNullOrEmpty() ?
                            string.Empty : HttpContext.Current.Request.UserLanguages.ToString(","),
                    Fields = fields.ToArray()
                };
            }
            catch
            {
                // exception may be caused by HideRequestResponse set to false
                // within HttpContext
                return null;
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return XmlUtil.ObjectToXml(this, true, true, true);
        }
    }

    [XmlType("H")]
    public class Field
    {
        [XmlAttribute("n")]
        public string Name { get; set; }
        [XmlAttribute("v")]
        public string Value { get; set; }
    }
}
