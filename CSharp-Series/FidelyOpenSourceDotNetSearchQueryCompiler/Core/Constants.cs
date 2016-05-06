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

using System;

namespace Fidely.Framework
{
    /// <summary>
    /// Defines the constants that relates to Fidely Framework.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The product name of Fidely Framework.
        /// </summary>
        public const string ProductName = "Fidely Framework";

        /// <summary>
        /// The product version of Fidely Framework.
        /// </summary>
        public const string ProductVersion = "1.0.0";

        /// <summary>
        /// The copyright of Fidely Framework.
        /// </summary>
        public const string Copyright = "Copyright 2011 Shou Takenaka";

        /// <summary>
        /// The description of Fidely Framework.
        /// </summary>
        public const string Description = "Fidely is a framework to implement a search query compiler that parses a search query string and builds up an expression tree (Expression<Func<T, bool>>) to filter a collection object.";

        /// <summary>
        /// The assembly version of assemblies that are contained Fidely Framework.
        /// </summary>
        public const string AssemblyVersion = "1.0.*";

        /// <summary>
        /// The file version of assemblies that are contained Fidely Framework.
        /// </summary>
        public const string FileVersion = "1.0.0.0";
    }
}