/*
 * Copyright 2011 Shou Takenaka
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Fidely.Framework.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Fidely.Framework
{
    internal static class Logger
    {
        private static readonly TraceSource logger = new TraceSource(typeof(Logger).FullName);



        internal static void Verbose(string format, params object[] args)
        {
            Write(TraceEventType.Verbose, format, args);
        }

        internal static void Info(string format, params object[] args)
        {
            Write(TraceEventType.Information, format, args);
        }

        internal static void Info(IEnumerable<IToken> tokens)
        {
            Write(TraceEventType.Information, String.Join(" ", tokens.Select(o => o.ToString())));
        }

        internal static void Warn(string format, params object[] args)
        {
            Write(TraceEventType.Warning, format, args);
        }

        internal static void Error(string format, params object[] args)
        {
            Write(TraceEventType.Error, format, args);
        }

        private static void Write(TraceEventType category, string format, params object[] args)
        {
            var trace = new StackTrace();
            var frame = trace.GetFrame(2);
            var type = frame.GetMethod().ReflectedType;
            logger.TraceEvent(category, 1, String.Format(CultureInfo.CurrentUICulture, "[{0:yyyy-MM-dd hh:mm:ss.fff zzz}] [{1}] {2}", DateTimeOffset.Now, type.FullName, format), args);
        }
    }
}
