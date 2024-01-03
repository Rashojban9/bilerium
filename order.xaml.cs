using OfficeOpenXml;
using System.ComponentModel;
using System.Text;

namespace Bislerium_Cafe
{
    public partial class order : ContentPage
    {
        private List<OrderInfo> orderList;
        private List<string> coffeeNames;
        private List<string> addIns;
        public order()
        {
            orderList = new List<OrderInfo>();
            coffeeNames = new List<string>();
            addIns = new List<string>();

            InitializeComponent();
            LoadDataFromExcel();
            InitializePickers();
        }

        private void LoadDataFromExcel()
        {
            string excelPath = "C:\\Users\\rasho\\Desktop\\Application development\\staff_login_data.xlsx";

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(excelPath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[2];

                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {

                    string coffeeName = worksheet.Cells[row, 1].Text;
                    string addIn = worksheet.Cells[row, 5].Text;

                    if (!string.IsNullOrEmpty(coffeeName))
                    {
                        coffeeNames.Add(coffeeName);
                    }

                    if (!string.IsNullOrEmpty(addIn))
                    {
                        addIns.Add(addIn);
                    }
                }
            }
        }
        private void InitializePickers()
        {
            CoffeeTypePicker.ItemsSource = coffeeNames;
            AddInsPicker.ItemsSource = addIns;
        }


        private void OnSizeCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;

            if (checkBox.IsChecked)
            {
                UpdateSummaryAsync();
                UncheckOtherSizeCheckBoxes(checkBox);

            }


        }


        private void OnExpressDeliveryCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            UpdateSummaryAsync();
        }

        private async void OnPlaceOrderClicked(object sender, EventArgs e)
        {
            

        }



        private async void UpdateSummaryAsync()
        {
            if (ValidateOrder())
            {
                OrderConfirmation.IsVisible = true;
                LoadingIndicator.IsRunning = true;
                LoadingIndicator.IsVisible = true;


                await Task.Delay(TimeSpan.FromSeconds(2));

                OrderConfirmation.IsVisible = false;
                LoadingIndicator.IsRunning = false;
                LoadingIndicator.IsVisible = false;
                var orderInfo = new OrderInfo
                {
                    CoffeeType = CoffeeTypePicker.SelectedItem.ToString(),
                    AddIns = AddInsPicker.SelectedItem?.ToString(),
                    Size = GetSelectedSize(),
                    quantity = QuantityEntry.Text,
                    ExpressDelivery = ExpressDeliveryCheckBox.IsChecked ? "Yes" : "No"
                };


                orderList.Add(orderInfo);





            }
            else
            {
                await DisplayAlert("Error", "Please select a coffee type before placing the order.", "OK");
            }

        }

        private string GetSelectedSize()
        {
            if (SmallCheckBox.IsChecked)
                return "Small";
            else if (MediumCheckBox.IsChecked)
                return "Medium";
            else if (LargeCheckBox.IsChecked)
                return "Large";
            else
                return "Not selected";
        }

        private void UncheckOtherSizeCheckBoxes(CheckBox checkedBox)
        {
            if (checkedBox == SmallCheckBox)
            {
                MediumCheckBox.IsChecked = false;
                LargeCheckBox.IsChecked = false;
            }
            else if (checkedBox == MediumCheckBox)
            {
                SmallCheckBox.IsChecked = false;
                LargeCheckBox.IsChecked = false;
            }
            else if (checkedBox == LargeCheckBox)
            {
                SmallCheckBox.IsChecked = false;
                MediumCheckBox.IsChecked = false;
            }
        }

        private bool ValidateOrder()
        {

            return CoffeeTypePicker.SelectedItem != null;
        }
        private string GenerateBill(OrderInfo orderInfo)
        {
            StringBuilder bill = new StringBuilder();
            bill.AppendLine("************ Coffee Order Bill ************");
            bill.AppendLine($"Coffee Type: {orderInfo.CoffeeType}");
            bill.AppendLine($"Add-ins: {orderInfo.AddIns}");
            bill.AppendLine($"Size: {orderInfo.Size}");
            bill.AppendLine($"Quantity: {orderInfo.quantity}");
            bill.AppendLine($"Express Delivery: {orderInfo.ExpressDelivery}");
            bill.AppendLine("**************************************");

            return bill.ToString();
        }
    }
    public class OrderInfo
    {
        public string CoffeeType { get; set; }
        public string AddIns { get; set; }
        public string Size { get; set; }
        public string quantity { get; set; }
        public string ExpressDelivery { get; set; }
    }
}
