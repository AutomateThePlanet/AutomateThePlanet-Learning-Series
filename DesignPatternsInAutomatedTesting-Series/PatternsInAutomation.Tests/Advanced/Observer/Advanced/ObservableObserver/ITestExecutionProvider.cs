﻿using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomatedTests.Advanced.Observer.Advanced.ObservableObserver
{
    public interface ITestExecutionProvider
    {
        void PreTestInit(TestContext context, MemberInfo memberInfo);

        void PostTestInit(TestContext context, MemberInfo memberInfo);

        void PreTestCleanup(TestContext context, MemberInfo memberInfo);
        
        void PostTestCleanup(TestContext context, MemberInfo memberInfo);

        void TestInstantiated(MemberInfo memberInfo);
    }
}