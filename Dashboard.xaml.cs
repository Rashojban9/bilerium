using bilerium;
using Microsoft.Maui.Controls;

namespace Bislerium_Cafe
{
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private async void OnMenuClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menupage());
        }

        private async void OnOrdersClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new orderdetail());

        }

        private void OnPromotionsClicked(object sender, EventArgs e)
        {

        }

        private async void Logout(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();


            await Navigation.PushModalAsync(new login());



        }
        private void OnJoinLoyaltyProgramClicked(object sender, EventArgs e)
        {

        }
    }
}