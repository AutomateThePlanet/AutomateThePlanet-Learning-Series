// <copyright file="TcpIpLogger.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>
using log4net.Repository.Hierarchy;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MSBuildTcpIPLogger
{
    public class TcpIpLogger : ILogger
    {
        #region Private Fields

        private IDictionary<string, string> _paramaterBag;
        private static NetworkStream networkStream;
        private TcpClient _clientSocketWriter;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        ////#region ILogger Members

        public void Initialize(IEventSource eventSource)
        {
            try
            {
                InitializeParameters();

                SubscribeToEvents(eventSource);

                log.Info("Initialize MS Build Logger!");

                var ipStr = GetParameterValue("ip");
                var ipServer = IPAddress.Parse(ipStr);
                var port = int.Parse(GetParameterValue("port"));
                log.InfoFormat("MS Build Logger port to write {0}", port);

                _clientSocketWriter = new TcpClient();
                _clientSocketWriter.Connect(ipServer, port);
                networkStream = _clientSocketWriter.GetStream();
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                log.Error("Exception in MS Build logger", ex);
            }
        }

        public void Shutdown()
        {
            _clientSocketWriter.GetStream().Close();
            _clientSocketWriter.Close();
        }

        ////#endregion

        protected virtual void InitializeParameters()
        {
            try
            {
                _paramaterBag = new Dictionary<string, string>();
                log.Info("Initialize Logger params");
                if (!string.IsNullOrEmpty(Parameters))
                {
                    foreach (var paramstring in Parameters.Split(";".ToCharArray()))
                    {
                        var keyValue = paramstring.Split("=".ToCharArray());
                        if (keyValue == null || keyValue.Length < 2)
                        {
                            continue;
                        }

                        ProcessParam(keyValue[0].ToLower(), keyValue[1]);
                    }
                }
            }
            catch (Exception e)
            {
                throw new LoggerException("Unable to initialize parameters; message=" + e.Message, e);
            }
        }

        /// <summary>
        /// Method that will process the parameter value. If either <code>name</code> or
        /// <code>value</code> is empty then this parameter will not be processed.
        /// </summary>
        /// <param name="name">name of the parameter</param>
        /// <param name="value">value of the parameter</param>
        protected virtual void ProcessParam(string name, string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                {
                    // add to param bag so subclasses have easy method to fetch other parameter values
                    log.Info("Process Logger params");
                    AddToParameters(name, value);
                }
            }
            catch (LoggerException)
            {
                throw;
            }
            ////catch (Exception e)
            ////{
            ////    string message = string.Concat("Unable to process parameters;[name=", name, ",value=", value, " message=", e.Message)
            ////    //throw new LoggerException(message, e);
            ////}
        }

        /// <summary>
        /// Adds the given name & value to the <code>_parameterBag</code>.
        /// If the bag already contains the name as a key, this value will replace the previous value.
        /// </summary>
        /// <param name="name">name of the parameter</param>
        /// <param name="value">value for the parameter</param>
        protected virtual void AddToParameters(string name, string value)
        {
            log.Info("Add new item to Logger params");
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (value == null)
            {
                throw new ArgumentException("value");
            }

            var paramKey = name.ToUpper();
            try
            {
                if (_paramaterBag.ContainsKey(paramKey))
                {
                    _paramaterBag.Remove(paramKey);
                }

                _paramaterBag.Add(paramKey, value);
            }
            catch (Exception e)
            {
                throw new LoggerException("Unable to add to parameters bag", e);
            }
        }

        /// <summary>
        /// This can be used to get the values of parameter that this class is not aware of.
        /// If the value is not present then string.Empty is returned.
        /// </summary>
        /// <param name="name">name of the parameter to fetch</param>
        /// <returns></returns>
        protected virtual string GetParameterValue(string name)
        {
            log.Info("Get parameter value from logger params");
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            var paramName = name.ToUpper();

            string value = null;
            if (_paramaterBag.ContainsKey(paramName))
            {
                value = _paramaterBag[paramName];
            }

            return value;
        }

        /// <summary>
        /// Will return a collection of parameters that have been defined.
        /// </summary>
        protected virtual ICollection<string> DefiniedParameters
        {
            get
            {
                ICollection<string> value = null;
                if (_paramaterBag != null)
                {
                    value = _paramaterBag.Keys;
                }

                return value;
            }
        }

        public LoggerVerbosity Verbosity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Parameters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private void SubscribeToEvents(IEventSource eventSource)
        {
            eventSource.BuildStarted +=
                new BuildStartedEventHandler(BuildStarted);
            eventSource.BuildFinished +=
                new BuildFinishedEventHandler(BuildFinished);
            eventSource.ProjectStarted +=
                new ProjectStartedEventHandler(ProjectStarted);
            eventSource.ProjectFinished +=
                new ProjectFinishedEventHandler(ProjectFinished);
            eventSource.TargetStarted +=
                new TargetStartedEventHandler(TargetStarted);
            eventSource.TargetFinished +=
                new TargetFinishedEventHandler(TargetFinished);
            eventSource.TaskStarted +=
                new TaskStartedEventHandler(TaskStarted);
            eventSource.TaskFinished +=
                new TaskFinishedEventHandler(TaskFinished);
            eventSource.ErrorRaised +=
                new BuildErrorEventHandler(BuildError);
            eventSource.WarningRaised +=
                new BuildWarningEventHandler(BuildWarning);
            eventSource.MessageRaised +=
                new BuildMessageEventHandler(BuildMessage);
        }

        #region Logging handlers

        private void BuildStarted(object sender, BuildStartedEventArgs e)
        {
            SendMessage(FormatMessage(e));
        }

        private void BuildFinished(object sender, BuildFinishedEventArgs e)
        {
            SendMessage(FormatMessage(e));
            SendMessage("END$$");
        }

        private void ProjectStarted(object sender, ProjectStartedEventArgs e)
        {
            SendMessage(FormatMessage(e));
        }

        private void ProjectFinished(object sender, ProjectFinishedEventArgs e)
        {
            SendMessage(FormatMessage(e));
            SendMessage("END$$");
        }

        private void TargetStarted(object sender, TargetStartedEventArgs e)
        {
            SendMessage(FormatMessage(e));
        }

        private void TargetFinished(object sender, TargetFinishedEventArgs e)
        {
            SendMessage(FormatMessage(e));
        }

        private void TaskStarted(object sender, TaskStartedEventArgs e)
        {
            SendMessage(FormatMessage(e));
        }

        private void TaskFinished(object sender, TaskFinishedEventArgs e)
        {
            SendMessage(FormatMessage(e));
        }

        private void BuildError(object sender, BuildErrorEventArgs e)
        {
            SendMessage(FormatMessage(e));
        }

        private void BuildWarning(object sender, BuildWarningEventArgs e)
        {
            SendMessage(FormatMessage(e));
        }

        private void BuildMessage(object sender, BuildMessageEventArgs e)
        {
            SendMessage(FormatMessage(e));
        }

        #endregion

        private void SendMessage(string line)
        {
            var sendBytes = Encoding.ASCII.GetBytes(line);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
            log.InfoFormat("MS Build logger send to server the message {0}", line);
        }

        private static string FormatMessage(BuildStatusEventArgs e)
        {
            return string.Format("{0}:{1}$$", e.HelpKeyword, e.Message);
        }

        private static string FormatMessage(LazyFormattedBuildEventArgs e)
        {
            return string.Format("{0}:{1}$$", e.HelpKeyword, e.Message);
        }
    }
}