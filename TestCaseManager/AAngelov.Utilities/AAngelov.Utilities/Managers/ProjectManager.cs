// <copyright file="ProjectManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace AAngelov.Utilities.Managers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;
    using AAngelov.Utilities.Entities;

    /// <summary>
    /// Contains helper methods for getting all methods information in specified assembly
    /// </summary>
    public class ProjectManager
    {
        /// <summary>
        /// The service provider for hash algorithm SHA1
        /// </summary>
        private static HashAlgorithm cryptoServiceProvider = new SHA1CryptoServiceProvider();

        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets the test project test methods.
        /// </summary>
        /// <param name="assemblyFullPath">The assembly full path.</param>
        /// <returns>array of method info objects</returns>
        public static List<Test> GetProjectTestMethods(string assemblyFullPath)
        {
            List<Test> tests = null;
            try
            {
                AppDomainSetup setup = AppDomain.CurrentDomain.SetupInformation;
                AppDomain newDomain = AppDomain.CreateDomain("newDomain", AppDomain.CurrentDomain.Evidence, setup);
                System.Runtime.Remoting.ObjectHandle obj = newDomain.CreateInstance(typeof(AssemblyLoader).Assembly.FullName, typeof(AssemblyLoader).FullName);
 
                AssemblyLoader loader = (AssemblyLoader)obj.Unwrap();
                loader.LoadAssembly(assemblyFullPath);
                tests = loader.GetMethodsWithTestMethodAttribute();
 
                AppDomain.Unload(newDomain);
            }
            catch (ReflectionTypeLoadException ex)
            {
                log.Error(ex);
                StringBuilder sb = new StringBuilder();
                foreach (Exception subException in ex.LoaderExceptions)
                {
                    sb.AppendLine(subException.Message);
                    if (subException is FileNotFoundException)
                    {
                        FileNotFoundException fileNotFoundException = subException as FileNotFoundException;
                        if (!string.IsNullOrEmpty(fileNotFoundException.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(fileNotFoundException.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
            }

            return tests;
        }

        /// <summary>
        /// Generates the test method unique identifier.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>method unique identifier</returns>
        public static Guid GenerateTestMethodId(MethodInfo methodInfo)
        {
            string currentNameSpace = methodInfo.DeclaringType.FullName;
            string currentTestMethodShortName = methodInfo.Name;
            string currentTestMethodFullName = string.Concat(currentNameSpace, ".", currentTestMethodShortName);
            Guid testId = GuidFromString(currentTestMethodFullName);

            return testId;
        }

        /// <summary>
        ///  Calculates a hash of the string and copies the first 128 bits of the hash to a new Guid.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>the test method guid</returns>        
        private static Guid GuidFromString(string data)
        {
            byte[] hash = cryptoServiceProvider.ComputeHash(System.Text.Encoding.Unicode.GetBytes(data));

            byte[] toGuid = new byte[16];
            Array.Copy(hash, toGuid, 16);

            return new Guid(toGuid);
        }
    }
}