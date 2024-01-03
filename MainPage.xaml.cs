using Bislerium_Cafe;

namespace bilerium
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
            NavigateToMainPage();
        }
        private async void NavigateToMainPage()
        {

            await Task.Delay(6000);


            Application.Current.MainPage = new NavigationPage(new login());
        }



    }

}
