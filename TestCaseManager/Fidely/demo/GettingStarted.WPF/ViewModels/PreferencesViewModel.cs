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

using Fidely.Demo.GettingStarted.WPF.Models;
using Fidely.Framework.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Fidely.Demo.GettingStarted.WPF.ViewModels
{
    public class PreferencesViewModel : BaseViewModel
    {
        public event EventHandler CloseRequired;

        public event EventHandler Committed;


        private Preferences model;

        private Preferences editing;


        public MatchingMode MatchingMode
        {
            get
            {
                return editing.MatchingMode;
            }
            set
            {
                editing.MatchingMode = value;
                OnPropertyChanged("MatchingMode");
            }
        }

        public IEnumerable<KeyValuePair<string, MatchingMode>> MatchingModes
        {
            get
            {
                return Enum.GetNames(typeof(MatchingMode)).ToDictionary(o => o, o => (MatchingMode)Enum.Parse(typeof(MatchingMode), o));
            }
        }


        public ICommand CommitCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }


        public PreferencesViewModel(Preferences model)
        {
            this.model = model;

            editing = new Preferences();
            model.CopyTo(editing);

            CommitCommand = new RelayCommand(o => Commit());
            CancelCommand = new RelayCommand(o => Cancel());
        }


        private void Commit()
        {
            editing.CopyTo(model);
            editing.SaveTo(App.PreferencePath);

            if (Committed != null)
            {
                Committed(this, EventArgs.Empty);
            }

            Close();
        }

        private void Cancel()
        {
            Close();
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
