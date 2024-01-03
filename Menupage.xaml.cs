using OfficeOpenXml;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;


namespace Bislerium_Cafe
{
    public partial class Menupage : ContentPage
    {
        private List<Coffee> coffeeItems;

        public Menupage()
        {
            InitializeComponent();
            coffeeItems = new List<Coffee>();
            LoadMenuFromExcel();
            
        }

        public async void Order(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new order());
        }

        private void LoadMenuFromExcel()
        {
            string excelPath = "C:\\Users\\rasho\\Desktop\\Application development\\staff_login_data.xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(excelPath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[2];

                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    string name = worksheet.Cells[row, 1].Text;
                    string price = worksheet.Cells[row, 2].Text;
                    string description = worksheet.Cells[row, 3].Text;

                   

                    Coffee coffee = new Coffee { Name = name, Price = price, Description = description };
                    coffeeItems.Add(coffee);
                    coffeeListView.ItemsSource = coffeeItems;

                }
            }
        }


        public class Coffee
        {
            public string Name { get; set; }
            public string Price { get; set; }
            public string Description { get; set; }
        }
    }
}