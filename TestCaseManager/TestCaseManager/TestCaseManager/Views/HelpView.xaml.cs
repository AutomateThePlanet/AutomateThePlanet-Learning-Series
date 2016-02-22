// <copyright file="HelpView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerApp
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Contains logic related to the about page
    /// </summary>
    public partial class HelpView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpView"/> class.
        /// </summary>
        public HelpView()
        {
            this.InitializeComponent();
        }
    }
}