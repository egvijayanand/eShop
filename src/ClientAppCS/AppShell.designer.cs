using eShop.ClientApp.Views;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;

namespace eShop.ClientApp
{
    public partial class AppShell : Shell
    {
        private void InitializeComponent()
        {
            #region Resources
            Resources.Add("BaseStyle", new Style(typeof(Element))
            {
                Setters =
                {
                    new() { Property = Shell.BackgroundColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => AppColor("DarkBackgroundColor"), AppTheme.Light or _ => AppColor("LightBackgroundColor") } },
                    new() { Property = Shell.ForegroundColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => AppColor("DarkForegroundColor"), AppTheme.Light or _ => AppColor("LightForegroundColor") } },
                    new() { Property = Shell.TitleColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => AppColor("DarkForegroundColor"), AppTheme.Light or _ => AppColor("LightForegroundColor") } },
                    new() { Property = Shell.DisabledColorProperty, Value = Color.FromArgb("#B4FFFFFF") },
                    new() { Property = Shell.UnselectedColorProperty, Value = Color.FromArgb("#95FFFFFF") },
                    new() { Property = Shell.TabBarBackgroundColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => AppColor("DarkBackgroundColor"), AppTheme.Light or _ => AppColor("LightBackgroundColor") } },
                    new() { Property = Shell.TabBarForegroundColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => AppColor("DarkForegroundColor"), AppTheme.Light or _ => AppColor("LightForegroundColor") } },
                    new() { Property = Shell.TabBarUnselectedColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => Color.FromArgb("#E7E7E7"), AppTheme.Light or _ => Color.FromArgb("#CCCCCC") } },
                    new() { Property = Shell.TabBarTitleColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => AppColor("DarkForegroundColor"), AppTheme.Light or _ => AppColor("LightForegroundColor") } },
                },
            });
            Resources.Add(new Style(typeof(TabBar))
            {
                BasedOn = (Style)Resources["BaseStyle"],
            });
            Resources.Add(new Style(typeof(FlyoutItem))
            {
                BasedOn = (Style)Resources["BaseStyle"],
            });
            #endregion
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
            {
                TextOverride = "",
            });
            Items.Add(new FlyoutItem()
            {
                Items =
                {
                    new ShellContent()
                    {
                        ContentTemplate = new DataTemplate(typeof(LoginView)),
                        Route = "Login",
                    },
                }
            });
            Items.Add(new TabBar()
            {
                Route = "Main",
                Items =
                {
                    new ShellContent()
                    {
                        Title = "CATALOG",
                        Route = "Catalog",
                        Icon = AppResource<FontImageSource>("CatalogIconImageSource"),
                        ContentTemplate = new DataTemplate(typeof(CatalogView)),
                    },
                    new ShellContent()
                    {
                        Title = "MAP",
                        Route = "Map",
                        /*IsVisible = DeviceInfo.Platform.ToString() switch
                        {
                            nameof(DevicePlatform.Android) => true,
                            nameof(DevicePlatform.iOS) => true,
                            nameof(DevicePlatform.MacCatalyst) => true,
                            _ => false,
                        },*/
                        Icon = AppResource<FontImageSource>("MapIconImageSource"),
                        ContentTemplate = new DataTemplate(typeof(MapView)),
                    },
                    new ShellContent()
                    {
                        Title = "PROFILE",
                        Route = "Profile",
                        Icon = AppResource<FontImageSource>("ProfileIconImageSource"),
                        ContentTemplate = new DataTemplate(typeof(ProfileView)),
                    },
                }
            });
        }
    }
}
