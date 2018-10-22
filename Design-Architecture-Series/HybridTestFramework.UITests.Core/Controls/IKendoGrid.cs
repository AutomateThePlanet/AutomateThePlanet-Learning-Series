// <copyright file="IKendoGrid.cs" company="Automate The Planet Ltd.">
// Copyright 2018 Automate The Planet Ltd.
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

using HybridTestFramework.UITests.Core.Data;
using HybridTestFramework.UITests.Core.Enums;
using System.Collections.Generic;

namespace HybridTestFramework.UITests.Core.Controls
{
    public interface IKendoGrid : IElement
    {
        void RemoveFilters();

        int TotalNumberRows();

        void Reload();

        int GetPageSize();

        int GetCurrentPageNumber();

        void ChangePageSize(int newSize);

        void NavigateToPage(int pageNumber);

        void Sort(string columnName, SortType sortType);

        void Filter(
            string columnName, 
            FilterOperator filterOperator, 
            string filterValue);

        void Filter(params GridFilter[] gridFilters);

        List<T> GetItems<T>() where T : class;
    }
}