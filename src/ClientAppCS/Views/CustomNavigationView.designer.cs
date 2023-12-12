using Microsoft.Maui.Graphics;

namespace eShop.ClientApp.Views
{
    public partial class CustomNavigationView : NavigationPage
    {
        private void InitializeComponent()
        {
            BarBackgroundColor = AppColor("GreenColor");
            BarTextColor = AppColor("WhiteColor");
            BackgroundColor = Transparent;
        }
    }
}
