// <copyright file="AssemblyLoader.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace AAngelov.Utilities.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AAngelov.Utilities.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Assemly Loader Helper class
    /// </summary>
    public class AssemblyLoader : MarshalByRefObject
    {
        /// <summary>
        /// The _assembly
        /// </summary>
        private Assembly assembly;

        /// <summary>
        /// Obtains a lifetime service object to control the lifetime policy for this instance.
        /// </summary>
        /// <returns>
        /// An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to the value of the <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime" /> property.
        /// </returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
        ///   </PermissionSet>
        public override object InitializeLifetimeService()
        {
            return null;
        }

        /// <summary>
        /// Loads the assembly.
        /// </summary>
        /// <param name="path">The path.</param>
        public void LoadAssembly(string path)
        {
            this.assembly = Assembly.Load(AssemblyName.GetAssemblyName(path));
        }

        /// <summary>
        /// Gets the methods with test method attribute.
        /// </summary>
        /// <returns></returns>
        public List<Test> GetMethodsWithTestMethodAttribute()
        {
            Type[] types;
            try
            {
                types = this.assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types;
            }
            types = types.Where(t => t != null).ToArray();
            MethodInfo[] methods = types.SelectMany(t => t.GetMethods().Where(y =>
            {
                var attributes = y.GetCustomAttributes(true).ToArray();
                if (attributes.Length == 0)
                {
                    return false;
                }
                else
                {
                    bool result = false;
                    foreach (var cAttribute in attributes)
                    {
                        result = cAttribute.GetType().FullName.Equals(typeof(TestMethodAttribute).ToString());
                        if (result)
                        {
                            break;
                        }
                    }

                    return result;
                }
            })).ToArray();

            List<Test> tests = GetTestsByMethodInfos(methods);

            return tests;
        }

        /// <summary>
        /// Gets the tests by method infos.
        /// </summary>
        /// <param name="methods">The methods.</param>
        /// <returns>list of tests</returns>
        private static List<Test> GetTestsByMethodInfos(MethodInfo[] methods)
        {
            List<Test> tests = new List<Test>();
            foreach (var cM in methods)
            {
                tests.Add(new Test(string.Format("{0}.{1}", cM.DeclaringType.FullName, cM.Name), cM.DeclaringType.Name, ProjectManager.GenerateTestMethodId(cM)));
            }
            return tests;
        }
    }
}