// <copyright file="ComboBoxDropdownExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
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
namespace AAngelov.Utilities.UI.ControlExtensions
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using AAngelov.Utilities.UI.Managers;

    /// <summary>
    /// Contains helper methods which add hover functionality to WPF combobox
    /// </summary>
    public static class ComboBoxDropdownExtensions
    {
        /// <summary>
        /// Gets/sets whether or not the ComboBox this behavior is applied to opens its items-popup
        /// when the mouse hovers over it and closes again when the mouse leaves.
        /// </summary>
        private static DependencyProperty openDropDownAutomaticallyProperty =
            DependencyProperty.RegisterAttached(
                "OpenDropDownAutomatically",
                typeof(bool),
                typeof(ComboBoxDropdownExtensions),
                new UIPropertyMetadata(false, OnOpenDropDownAutomatically_Changed));

        /// <summary>
        /// Gets the open drop down automatically.
        /// </summary>
        /// <param name="cbo">The combobox.</param>
        /// <returns>should open automatically</returns>
        public static bool GetOpenDropDownAutomatically(ComboBox cbo)
        {
            return (bool)cbo.GetValue(openDropDownAutomaticallyProperty);
        }

        /// <summary>
        /// Sets the open drop down automatically.
        /// </summary>
        /// <param name="cbo">The cbo.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetOpenDropDownAutomatically(ComboBox cbo, bool value)
        {
            cbo.SetValue(openDropDownAutomaticallyProperty, value);
        }

        /// <summary>
        /// Handles the MouseMove event of the cbo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        public static void cboMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;

            // Get a ref to the ComboBox'es popup (which is what displays the available items)
            Popup p = (Popup)cbo.Template.FindName("PART_Popup", cbo);

            // The DropDown/popup is to close when 
            // - it is still open
            // - the mouse is no longer over the popup
            // - the cbo's IsMouseDirectlyOver returns true (which, albeit strange, is true
            //   when the mouse is neither over the popup NOR the cbo itself
            if (cbo.IsDropDownOpen && !p.IsMouseOver && cbo.IsMouseDirectlyOver && UIRegistryManager.Instance.GetDropDownBehavior())
            {
                cbo.IsDropDownOpen = false;
            }
        }

        /// <summary>
        /// Open the DropDown/popup as soon as the mouse hovers over the control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private static void cboMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((ComboBox)sender).IsDropDownOpen = true;
        }

        /// <summary>
        /// Fired when the assignment of the behavior changes (IOW, is being turned on or off).
        /// </summary>
        /// <param name="doSource"> The document source.</param>
        /// <param name="e"> The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnOpenDropDownAutomatically_Changed(DependencyObject doSource, DependencyPropertyChangedEventArgs e)
        {
            // The ComboBox that is the target of the assignment
            ComboBox cbo = doSource as ComboBox;
            if (cbo == null)
            {
                return;
            }

            // Just to be safe ...
            if (e.NewValue is bool == false)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                // Attach
                cbo.MouseMove += cboMouseMove;
                cbo.MouseEnter += cboMouseEnter;
            }
            else
            {
                // Detach
                cbo.MouseMove -= cboMouseMove;
                cbo.MouseEnter -= cboMouseEnter;
            }
        }
    }
}