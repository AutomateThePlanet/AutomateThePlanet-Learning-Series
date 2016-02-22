using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Fidely.Demo.DBViewer.Models;
using System.Collections;
using Fidely.Framework;

namespace Fidely.Demo.DBViewer.ViewModels
{
    public class DatabaseViewModel : BaseViewModel
    {
        private IEnumerable dataSource;

        private CompilerSetting setting;


        public ICollection<TableViewModel> Tables { get; private set; }

        public CompilerSetting CompilerSetting
        {
            get
            {
                return setting;
            }
            set
            {
                setting = value;
                OnPropertyChanged("CompilerSetting");
            }
        }

        public IEnumerable DataSource
        {
            get
            {
                return dataSource;
            }
            set
            {
                dataSource = value;
                OnPropertyChanged("DataSource");
            }
        }


        public DatabaseViewModel()
        {
            Tables = new ObservableCollection<TableViewModel>
            {
                new TableViewModel { Name = "Categories" },
                new TableViewModel { Name = "CustomerDemographics" },
                new TableViewModel { Name = "Customers" },
                new TableViewModel { Name = "Employees" },
                new TableViewModel { Name = "Order Details" },
                new TableViewModel { Name = "Orders" },
                new TableViewModel { Name = "Products" },
                new TableViewModel { Name = "Regions" },
                new TableViewModel { Name = "Shippers" },
                new TableViewModel { Name = "Suppliers" },
                new TableViewModel { Name = "Territories" },
            };

        }


        public void Search(string tableName, string query)
        {
            using (var context = new NorthwindEntities())
            {

                switch (tableName)
                {
                    case "Categories":
                        {
                            var compiler = SearchQueryCompilerBuilder.Instance.CreateDefaultCompilerForEntity<Category>();
                            DataSource = context.Categories.Where(compiler.Compile(query)).ToList();
                            CompilerSetting = SearchQueryCompilerFactory.GetDefaultCompilerSettingForEntity<Category>();
                            break;
                        }
                    case "CustomerDemographics":
                        {
                            var compiler = SearchQueryCompilerFactory.CreateDefaultCompilerForEntity<CustomerDemographic>();
                            DataSource = context.CustomerDemographics.Where(compiler.Compile(query)).ToList();
                            CompilerSetting = SearchQueryCompilerFactory.GetDefaultCompilerSettingForEntity<CustomerDemographic>();
                            break;
                        }
                    case "Customers":
                        {
                            var compiler = SearchQueryCompilerFactory.CreateDefaultCompilerForEntity<Customer>();
                            DataSource = context.Customers.Where(compiler.Compile(query)).ToList();
                            CompilerSetting = SearchQueryCompilerFactory.GetDefaultCompilerSettingForEntity<Customer>();
                            break;
                        }
                    case "Employees":
                        {
                            var compiler = SearchQueryCompilerFactory.CreateDefaultCompilerForEntity<Employee>();
                            DataSource = context.Employees.Where(compiler.Compile(query)).ToList();
                            CompilerSetting = SearchQueryCompilerFactory.GetDefaultCompilerSettingForEntity<Employee>();
                            break;
                        }
                    case "Order Details":
                        {
                            var compiler = SearchQueryCompilerFactory.CreateDefaultCompilerForEntity<Order_Detail>();
                            DataSource = context.Order_Details.Where(compiler.Compile(query)).ToList();
                            CompilerSetting = SearchQueryCompilerFactory.GetDefaultCompilerSettingForEntity<Order_Detail>();
                            break;
                        }
                    case "Orders":
                        {
                            var compiler = SearchQueryCompilerFactory.CreateDefaultCompilerForEntity<Order>();
                            DataSource = context.Orders.Where(compiler.Compile(query)).ToList();
                            CompilerSetting = SearchQueryCompilerFactory.GetDefaultCompilerSettingForEntity<Order>();
                            break;
                        }
                    case "Products":
                        {
                            var compiler = SearchQueryCompilerFactory.CreateDefaultCompilerForEntity<Product>();
                            DataSource = context.Products.Where(compiler.Compile(query)).ToList();
                            CompilerSetting = SearchQueryCompilerFactory.GetDefaultCompilerSettingForEntity<Product>();
                            break;
                        }
                    case "Regions":
                        {
                            var compiler = SearchQueryCompilerFactory.CreateDefaultCompilerForEntity<Region>();
                            DataSource = context.Regions.Where(compiler.Compile(query)).ToList();
                            CompilerSetting = SearchQueryCompilerFactory.GetDefaultCompilerSettingForEntity<Region>();
                            break;
                        }
                    case "Shippers":
                        {
                            var compiler = SearchQueryCompilerFactory.CreateDefaultCompilerForEntity<Shipper>();
                            DataSource = context.Shippers.Where(compiler.Compile(query)).ToList();
                            CompilerSetting = SearchQueryCompilerFactory.GetDefaultCompilerSettingForEntity<Shipper>();
                            break;
                        }
                    case "Suppliers":
                        {
                            var compiler = SearchQueryCompilerFactory.CreateDefaultCompilerForEntity<Supplier>();
                            DataSource = context.Suppliers.Where(compiler.Compile(query)).ToList();
                            CompilerSetting = SearchQueryCompilerFactory.GetDefaultCompilerSettingForEntity<Supplier>();
                            break;
                        }
                    case "Territories":
                        {
                            var compiler = SearchQueryCompilerFactory.CreateDefaultCompilerForEntity<Territory>();
                            DataSource = context.Territories.Where(compiler.Compile(query)).ToList();
                            CompilerSetting = SearchQueryCompilerFactory.GetDefaultCompilerSettingForEntity<Territory>();
                            break;
                        }
                }
            }
        }
    }
}
