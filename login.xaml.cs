using OfficeOpenXml;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Bislerium_Cafe
{
    public partial class login : ContentPage
    {
        private Entry AdditionalPasswordEntry;

        public login()
        {
            InitializeComponent();
        }

        private void showpasswordcheckchange(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            if (checkBox.IsChecked)
            {
                PasswordEntry.IsPassword = false;
            }
            else
            {
                PasswordEntry.IsPassword = true;
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                string username = UsernameEntry.Text;
                string password = PasswordEntry.Text;
                LoadingIndicator.IsRunning = true;
                LoadingIndicator.IsVisible = true;
                bool errorMessage = true;

                string excelPath = "C:\\Users\\rasho\\Desktop\\Application development\\staff_login_data.xlsx";

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(excelPath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 1; row <= rowCount; row++)
                    {
                        string username_ = worksheet.Cells[row, 6].Text;
                        string password_ = worksheet.Cells[row, 7].Text;
                        string auth = worksheet.Cells[row, 8].Text;

                        if (!string.IsNullOrEmpty(username_) && username_ == username && password_ == password)
                        {

                            bool isAuthorized = await DisplayAdditionalPasswordPrompt(auth);

                            if (isAuthorized)
                            {

                                LoadingIndicator.IsRunning = false;
                                LoadingIndicator.IsVisible = false;
                                errorMessage = false;

                                await Navigation.PushAsync(new Dashboard());
                                break;
                            }
                        }
                    }

                    if (errorMessage)
                    {
                        ErrorMessage.Text = "Username and password didn't match!!";
                        LoadingIndicator.IsRunning = false;

                        await Task.Delay(6000);
                        ErrorMessage.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }

        private async Task<bool> DisplayAdditionalPasswordPrompt(String auth)
        {


            AdditionalPasswordEntry = new Entry
            {
                Placeholder = "Enter additional password",
                IsPassword = true // Set IsPassword to true for password input
            };

            var result = await DisplayPromptAsync("Additional Password", null, "OK", "Cancel", "Type password here", 4, keyboard: Keyboard.Default);


            bool isAuthorized = result != null && result == auth;

            return isAuthorized;
        }

    }
}