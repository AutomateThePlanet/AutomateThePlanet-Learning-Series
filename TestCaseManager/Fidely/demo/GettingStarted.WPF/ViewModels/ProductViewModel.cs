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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fidely.Demo.GettingStarted.WPF.Models;
using Fidely.Framework;
using System.ComponentModel;

namespace Fidely.Demo.GettingStarted.WPF.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private Product model;


        [Alias("id", Description = "Product ID (alias of ProductID)")]
        [Description("Product ID")]
        public int ProductID
        {
            get
            {
                return model.ProductID;
            }
            set
            {
                model.ProductID = value;
                OnPropertyChanged("ProductID");
            }
        }

        [Description("Name of the product")]
        public string Name
        {
            get
            {
                return model.Name;
            }
            set
            {
                model.Name = value;
                OnPropertyChanged("Name");
            }
        }

        [Alias("price", Description = "Selling price (alias of ListPrice)")]
        [Description("Selling price")]
        public decimal ListPrice
        {
            get
            {
                return model.ListPrice;
            }
            set
            {
                model.ListPrice = value;
                OnPropertyChanged("ListPrice");
            }
        }

        [Alias("start", Description = "Date the product was available for sale (alias of SellStartDate)")]
        [Description("Date the product was available for sale")]
        public DateTime SellStartDate
        {
            get
            {
                return model.SellStartDate;
            }
            set
            {
                model.SellStartDate = value;
                OnPropertyChanged("SellStartDate");
            }
        }


        public ProductViewModel(Product model)
        {
            this.model = model;
        }
    }
}
