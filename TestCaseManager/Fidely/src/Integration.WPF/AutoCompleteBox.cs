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

using Fidely.Framework;
using Fidely.Framework.Compilation;
using Fidely.Framework.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace Fidely.Framework.Integration.WPF
{
    /// <summary>
    /// The Fidely autocomplete box for WPF.
    /// </summary>
    public class AutoCompleteBox : Control
    {
        /// <summary>
        /// Occurs when search is executed.
        /// </summary>
        public event EventHandler<SearchExecutedEventArgs> SearchExecuted;


        /// <summary>
        /// Identifies the SearchCommand dependency property.
        /// </summary>
        public static readonly DependencyProperty SearchCommandProperty = DependencyProperty.Register("SearchCommand", typeof(ICommand), typeof(AutoCompleteBox));

        /// <summary>
        /// Identifies the AutoCompleteItems dependency property.
        /// </summary>
        public static readonly DependencyProperty AutoCompleteItemsProperty = DependencyProperty.Register("AutoCompleteItems", typeof(IEnumerable<IAutoCompleteItem>), typeof(AutoCompleteBox));

        /// <summary>
        /// Identifies the MatchingMode dependency property.
        /// </summary>
        public static readonly DependencyProperty MatchingModeProperty = DependencyProperty.Register("MatchingMode", typeof(MatchingMode), typeof(AutoCompleteBox));


        private bool queryChanged;


        /// <summary>
        /// The search command.
        /// </summary>
        public ICommand SearchCommand
        {
            get
            {
                return (ICommand)GetValue(SearchCommandProperty);
            }
            set
            {
                SetValue(SearchCommandProperty, value);
            }
        }

        /// <summary>
        /// The autocomplete items.
        /// </summary>
        public IEnumerable<IAutoCompleteItem> AutoCompleteItems
        {
            get
            {
                return (IEnumerable<IAutoCompleteItem>)GetValue(AutoCompleteItemsProperty);
            }
            set
            {
                SetValue(AutoCompleteItemsProperty, value);
            }
        }

        /// <summary>
        /// The matching mode that is used to determine whether or not show the autocomplete item.
        /// </summary>
        public MatchingMode MatchingMode
        {
            get
            {
                return (MatchingMode)GetValue(MatchingModeProperty);
            }
            set
            {
                SetValue(MatchingModeProperty, value);
            }
        }


        static AutoCompleteBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoCompleteBox), new FrameworkPropertyMetadata(typeof(AutoCompleteBox)));
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public AutoCompleteBox()
        {
            AutoCompleteItems = new List<IAutoCompleteItem>();
            AddHandler(TextBox.SelectionChangedEvent, new RoutedEventHandler(OnSelectionChanged));
            AddHandler(TextBox.TextChangedEvent, new RoutedEventHandler(OnTextChanged));
        }


        /// <summary>
        /// Registers event handlers to events of list box to show autocomplete items.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var listBox = Template.FindName("PART_AutocompleteListBox", this) as ListBox;
            listBox.MouseDoubleClick += OnListBoxMouseDoubleClicked;
            listBox.PreviewKeyDown += OnListBoxPreviewKeyDown;
        }

        /// <summary>
        /// Handles the key down event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            var textBox = Template.FindName("PART_TextBox", this) as TextBox;
            var popup = Template.FindName("PART_AutocompletePopup", this) as Popup;
            var listBox = Template.FindName("PART_AutocompleteListBox", this) as ListBox;

            switch (e.Key)
            {
                case Key.Down:
                    if (textBox != null && textBox.IsFocused)
                    {
                        var item = listBox.FindDescendents<ListBoxItem>().FirstOrDefault(o => o.IsSelected);
                        if (item != null)
                        {
                            item.Focus();
                            e.Handled = true;
                        }
                    }
                    break;
                case Key.Escape:
                    if (popup.IsOpen && textBox != null)
                    {
                        popup.IsOpen = false;
                        textBox.Focus();
                        e.Handled = true;
                    }
                    break;
                case Key.Enter:
                    if (popup.IsOpen && (e.KeyboardDevice.IsKeyDown(Key.LeftShift) || e.KeyboardDevice.IsKeyDown(Key.RightShift)))
                    {
                        listBox.SelectedIndex = 0;
                        Replase();
                        e.Handled = true;
                    }
                    else if (textBox != null && textBox.IsFocused)
                    {
                        if (SearchCommand != null && SearchCommand.CanExecute(textBox.Text))
                        {
                            SearchCommand.Execute(textBox.Text);
                        }
                        else if (SearchExecuted != null)
                        {
                            SearchExecuted(this, new SearchExecutedEventArgs(textBox.Text));
                        }
                        popup.IsOpen = false;
                    }
                    break;
            }
        }

        private void Replase()
        {
            var textBox = Template.FindName("PART_TextBox", this) as TextBox;
            if (textBox == null)
            {
                return;
            }

            var listBox = Template.FindName("PART_AutocompleteListBox", this) as ListBox;
            var procedure = listBox.SelectedValue as Func<string, MatchingOption, string>;
            if (procedure != null)
            {
                var prev = GetPrevIndex();
                var next = GetNextIndex();
                var context = textBox.Text.Substring(prev, next - prev + 1);
                var value = procedure(context, new MatchingOption { Mode = MatchingMode });
                textBox.Text = textBox.Text.Remove(prev, next - prev + 1).Insert(prev, value);
                textBox.Focus();
                textBox.CaretIndex = prev + value.Length;
            }
        }

        private bool IsInQuote()
        {
            var textBox = Template.FindName("PART_TextBox", this) as TextBox;
            if (textBox == null)
            {
                return true;
            }

            var singleCode = 0x01;
            var doubleCode = 0x02;
            var code = 0x00;

            var text = textBox.Text;
            var index = textBox.CaretIndex - 1;
            for (int i = 0; i < index; i++)
            {
                if (text[i] == '\'')
                {
                    code = ((code | singleCode) == singleCode) ? code ^ singleCode : code;
                }
                else if (text[i] == '"')
                {
                    code = ((code | doubleCode) == doubleCode) ? code ^ doubleCode : code;
                }
            }

            return code != 0x00;
        }

        private int GetPrevIndex()
        {
            var textBox = Template.FindName("PART_TextBox", this) as TextBox;
            if (textBox == null)
            {
                return -1;
            }

            var query = textBox.Text;
            var i = textBox.CaretIndex - 1;
            while (i >= 0 && query[i] != '(' && query[i] != ')' && !Char.IsWhiteSpace(query[i]))
            {
                i--;
            }
            return i + 1;
        }

        private int GetNextIndex()
        {
            var textBox = Template.FindName("PART_TextBox", this) as TextBox;
            if (textBox == null)
            {
                return -1;
            }

            var query = textBox.Text;
            var i = textBox.CaretIndex;
            while (i < query.Length && query[i] != '(' && query[i] != ')' && !Char.IsWhiteSpace(query[i]))
            {
                i++;
            }
            return i - 1;
        }

        private void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (queryChanged)
            {
                queryChanged = false;
            }
            else
            {
                var popup = Template.FindName("PART_AutocompletePopup", this) as Popup;
                popup.IsOpen = false;
            }
        }

        private void OnTextChanged(object sender, RoutedEventArgs e)
        {
            var textBox = Template.FindName("PART_TextBox", this) as TextBox;
            if (textBox == null || AutoCompleteItems.Count() == 0)
            {
                return;
            }

            if (!textBox.IsFocused)
            {
                return;
            }

            if (IsInQuote())
            {
                return;
            }

            var prev = GetPrevIndex();
            var next = GetNextIndex();
            if (next < prev)
            {
                return;
            }

            var popup = Template.FindName("PART_AutocompletePopup", this) as Popup;
            var listBox = Template.FindName("PART_AutocompleteListBox", this) as ListBox;

            var context = textBox.Text.Substring(prev, next - prev + 1);

            var option = new MatchingOption { Mode = MatchingMode };
            var items = AutoCompleteItems.Where(o => o.IsMatch(context, option))
                .Select((o, i) => new AutoCompleteItemWrapper(o.DisplayName, o.Description, o.Complete, i == 0));

            listBox.DataContext = items;
            listBox.SelectedIndex = 0;
            popup.IsOpen = items.Count() > 0;

            queryChanged = true;
        }

        private void OnListBoxMouseDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            Replase();
        }

        private void OnListBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Replase();
                e.Handled = true;
            }
        }


        private class AutoCompleteItemWrapper
        {
            public string DisplayName { get; private set; }

            public string Description { get; private set; }

            public Func<string, MatchingOption, string> CompleteProcedure { get; private set; }

            public bool IsFirst { get; private set; }


            public AutoCompleteItemWrapper(string displayName, string description, Func<string, MatchingOption, string> procedure, bool isFirst)
            {
                DisplayName = displayName;
                Description = description;
                CompleteProcedure = procedure;
                IsFirst = isFirst;
            }
        }
    }
}
