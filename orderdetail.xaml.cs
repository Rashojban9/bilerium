namespace Bislerium_Cafe;

public partial class orderdetail : ContentPage
{
    public orderdetail()
    {
        InitializeComponent();
    }
    public async void OnSubmitClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new order());
    }
}