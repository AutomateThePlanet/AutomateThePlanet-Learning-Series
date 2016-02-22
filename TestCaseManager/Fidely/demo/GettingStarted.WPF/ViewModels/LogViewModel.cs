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
using System;

namespace Fidely.Demo.GettingStarted.WPF.ViewModels
{
    public class LogViewModel : BaseViewModel
    {
        private Log model;


        public string Category
        {
            get
            {
                return model.Category;
            }
        }

        public int Id
        {
            get
            {
                return model.Id;
            }
        }

        public DateTimeOffset Timestamp
        {
            get
            {
                return model.Timestamp;
            }
        }

        public string Type
        {
            get
            {
                return model.Type;
            }
        }

        public string Message
        {
            get
            {
                return model.Message;
            }
        }

        
        public LogViewModel(Log model)
        {
            this.model = model;
        }
    }
}
