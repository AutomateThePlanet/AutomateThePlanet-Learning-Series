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

using Fidely.Demo.GettingStarted.WPF.Views;
using Fidely.Framework;
using Fidely.Framework.Compilation.Objects;
using Fidely.Framework.Integration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Input;

namespace Fidely.Demo.GettingStarted.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public event EventHandler CloseRequired;


        private SearchQueryCompiler<ProductViewModel> compiler;

        private PreferencesViewModel preferences;


        public PreferencesViewModel Preferences
        {
            get
            {
                return preferences;
            }
            set
            {
                preferences = value;
                OnPropertyChanged("Preferences");
            }
        }

        public ICollection<ProductViewModel> Products { get; private set; }

        public ICollection<ProductViewModel> SearchResult { get; private set; }

        public ICollection<IAutoCompleteItem> AutoCompleteItems { get; private set; }

        public ICommand SearchCommand { get; private set; }

        public ICommand LogCommand { get; private set; }

        public ICommand PreferencesCommand { get; private set; }

        public ICommand AboutCommand { get; private set; }

        public ICommand CloseCommand { get; private set; }


        public MainViewModel()
        {
            var setting = SearchQueryCompilerBuilder.Instance.BuildUpDefaultObjectCompilerSetting<ProductViewModel>();
            compiler = SearchQueryCompilerBuilder.Instance.BuildUpCompiler<ProductViewModel>(setting);
            Preferences = new PreferencesViewModel(((App)Application.Current).Preferences);

            Products = new ObservableCollection<ProductViewModel>();
            SearchResult = new ObservableCollection<ProductViewModel>();
            AutoCompleteItems = new ObservableCollection<IAutoCompleteItem>(setting.ExtractAutoCompleteItems());

            SearchCommand = new RelayCommand(o => Search(o.ToString()));
            LogCommand = new RelayCommand(o => OpenDiagnosticsView());
            PreferencesCommand = new RelayCommand(o => OpenPreferencesView());
            AboutCommand = new RelayCommand(o => OpenAboutView());
            CloseCommand = new RelayCommand(o => Close());
        }


        public void Initialize()
        {
            SearchResult.Clear();
            foreach (ProductViewModel product in Products)
            {
                SearchResult.Add(product);
            }
        }

        private void Search(string query)
        {
            Expression<Func<ProductViewModel, bool>> filter = compiler.Compile(query);
            IEnumerable<ProductViewModel> result = Products.AsQueryable().Where(filter);

            SearchResult.Clear();
            foreach (ProductViewModel product in result)
            {
                SearchResult.Add(product);
            }
        }

        private void OpenDiagnosticsView()
        {
            DiagnosticsWindow view = new DiagnosticsWindow();
            DiagnosticsViewModel model = new DiagnosticsViewModel();
            view.DataContext = model;
            view.Show();
        }

        private void OpenPreferencesView()
        {
            PreferencesWindow view = new PreferencesWindow();
            PreferencesViewModel model = new PreferencesViewModel(((App)Application.Current).Preferences);

            model.CloseRequired += delegate
            {
                view.Close();
            };
            model.Committed += delegate
            {
                Preferences = model;
            };

            view.DataContext = model;
            view.ShowDialog();
        }

        private void OpenAboutView()
        {
            AboutWindow view = new AboutWindow();
            AboutViewModel model = new AboutViewModel();
            view.DataContext = model;
            view.ShowDialog();
        }

        private void Close()
        {
            if (CloseRequired != null)
            {
                CloseRequired(this, EventArgs.Empty);
            }
        }
    }
}
