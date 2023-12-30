using eShop.ClientApp.Animations;
using eShop.ClientApp.Controls;
using eShop.ClientApp.Converters;
using eShop.ClientApp.Triggers;
using eShop.ClientApp.ViewModels;
using eShop.ClientApp.ViewModels.Base;
using Microsoft.Maui.Devices;

namespace eShop.ClientApp.Views
{
    public partial class SettingsView : ContentPage
    {
        private void InitializeComponent()
        {
            Title = "SETTINGS";
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
            Shell.SetNavBarIsVisible(this, true);
            Shell.SetTabBarIsVisible(this, false);
            #region Resources
            Resources.Add("DoubleConverter", new DoubleConverter());
            Resources.Add("SettingsStackLayoutStyle", new Style(typeof(StackLayout))
            {
                Setters =
                {
                    new() { Property = StackLayout.MarginProperty, Value = new Thickness(6) },
                },
            });
            Resources.Add("SettingsTitleStyle", new Style(typeof(Label))
            {
                BasedOn = AppStyle("MediumSizeFontStyle"),
                Setters =
                {
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Start },
                    new() { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.MarginProperty, Value = new Thickness(12, 0) },
                    new() { Property = Label.TextColorProperty, Value = AppColor("GreenColor") },
                },
            });
            Resources.Add("SettingsDescriptionStyle", new Style(typeof(Label))
            {
                BasedOn = AppStyle("LittleSizeFontStyle"),
                Setters =
                {
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Start },
                    new() { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.MarginProperty, Value = new Thickness(12, 0) },
                },
            });
            Resources.Add("SettingsWarningMessageStyle", new Style(typeof(Label))
            {
                BasedOn = AppStyle("LittleSizeFontStyle"),
                Setters =
                {
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Start },
                    new() { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.MarginProperty, Value = new Thickness(12, 0) },
                    new() { Property = Label.TextColorProperty, Value = AppColor("ErrorColor") },
                },
            });
            Resources.Add("SettingsToggleButtonStyle", new Style(typeof(ToggleButton))
            {
                Setters =
                {
                    new() { Property = ToggleButton.HeightRequestProperty, Value = 48 },
                    new() { Property = ToggleButton.WidthRequestProperty, Value = 48 },
                    new() { Property = ToggleButton.VerticalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = ToggleButton.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = ToggleButton.MarginProperty, Value = new Thickness(12,0) },
                    new() { Property = ToggleButton.AnimateProperty, Value = true },
                },
            });
            Resources.Add("SettingsEntryStyle", new Style(typeof(Entry))
            {
                BasedOn = AppStyle("EntryStyle"),
                Setters =
                {
                    new() { Property = Entry.MarginProperty, Value = new Thickness(12, 0) },
                },
            });
            Resources.Add("SettingsWinUIEntryStyle", new Style(typeof(Entry))
            {
                BasedOn = AppStyle("WinUIEntryStyle"),
                Setters =
                {
                    new() { Property = Entry.MarginProperty, Value = new Thickness(12, 0) },
                },
            });
            Resources.Add("HeaderLabelStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppDouble("LittleSize") },
                    new() { Property = Label.TextColorProperty, Value = AppColor("GreenColor") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Start },
                    new() { Property = Label.MarginProperty, Value = new Thickness(12,0) },
                },
            });
            Resources.Add("MockServicesAnimation", new StoryBoard()
            {
                Target = MockServices,
                Animations =
                {
                    new FadeInAnimation()
                    {
                        Direction = FadeInAnimation.FadeDirection.Up,
#if IOS
                        Duration = "0",
#endif
#if (ANDROID || WINDOWS)
                        Duration = "500",
#endif
                    },
                }
            });
            #endregion
            Triggers.Add(new EventTrigger()
            {
                Event = nameof(ContentPage.Appearing),
                Actions =
                {
                    new BeginAnimation()
                    {
                        Animation = (StoryBoard)Resources["MockServicesAnimation"],
                    },
                },
            });
            Content = new Grid()
            {
                ColumnDefinitions = Columns.Define(Star,Auto),
                RowDefinitions = Rows.Define(Auto,Auto,Auto,Auto,Auto),
                Children =
                {
                    new StackLayout()
                    {
                        Style = (Style)Resources["SettingsStackLayoutStyle"],
                        Children =
                        {
                            new Label()
                            {
                                Style = (Style)Resources["SettingsTitleStyle"],
                            }.Bind(nameof(SettingsViewModel.TitleUseAzureServices)),
                            new Label()
                            {
                                Style = (Style)Resources["SettingsDescriptionStyle"],
                            }.Bindv2(static (SettingsViewModel vm) => vm.DescriptionUseAzureServices),
                        },
                    },
                    new ToggleButton()
                    {
                        Style = (Style)Resources["SettingsToggleButtonStyle"],
                        CheckedImage = AppResource<FontImageSource>("ToggleOnImageSource"),
                        UnCheckedImage = AppResource<FontImageSource>("ToggleOffImageSource"),
                    }.Column(1)
                     .Bindv2(ToggleButton.CheckedProperty, static (SettingsViewModel vm) => vm.UseAzureServices, BindingMode.TwoWay)
                     .BindCommandv2(static (SettingsViewModel vm) => vm.ToggleMockServicesCommand),
                    new StackLayout()
                    {
                        Style = (Style)Resources["SettingsStackLayoutStyle"],
                        Children =
                        {
                            new Label()
                            {
                                Style = (Style)Resources["HeaderLabelStyle"],
                            }.Text("Identity Url"),
                            new Entry()
                            {
#if (IOS || ANDROID)
                                Style = (Style)Resources["SettingsEntryStyle"],
#endif
#if WINDOWS
                                Style = (Style)Resources["SettingsWinUIEntryStyle"],
#endif
                            }.Bindv2(static (SettingsViewModel vm) => vm.IdentityEndpoint, BindingMode.TwoWay),
                            new Label()
                            {
                                Style = (Style)Resources["HeaderLabelStyle"],
                            }.Text("Gateway Shopping Url"),
                            new Entry()
                            {
#if (IOS || ANDROID)
                                Style = (Style)Resources["SettingsEntryStyle"],
#endif
#if WINDOWS
                                Style = (Style)Resources["SettingsWinUIEntryStyle"],
#endif
                            }.Bindv2(static (SettingsViewModel vm) => vm.GatewayShoppingEndpoint, BindingMode.TwoWay),
                            new Label()
                            {
                                Style = (Style)Resources["HeaderLabelStyle"],
                            }.Text("Gateway Marketing Url"),
                            new Entry()
                            {
#if (IOS || ANDROID)
                                Style = (Style)Resources["SettingsEntryStyle"],
#endif
#if WINDOWS
                                Style = (Style)Resources["SettingsWinUIEntryStyle"],
#endif
                            }.Bindv2(static (SettingsViewModel vm) => vm.GatewayMarketingEndpoint, BindingMode.TwoWay),
                        },
                    }.Row(1)
                     .ColumnSpan(2)
                     .Bindv2(StackLayout.IsVisibleProperty, static (SettingsViewModel vm) => vm.UseAzureServices),
                }
            }.Assign(out MockServices);
        }

        #region Variables
        private Grid MockServices;
        #endregion
    }
}
