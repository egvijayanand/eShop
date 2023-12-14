using eShop.ClientApp.ViewModels;
using eShop.ClientApp.ViewModels.Base;
using eShop.ClientApp.Views.Templates;
using Microsoft.Maui.Devices;

namespace eShop.ClientApp.Views
{
    public partial class BasketView : ContentPageBase
    {
        private void InitializeComponent()
        {
            Title = "CART";
            Resources.Add("CartTotalStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("LargerSize") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.End },
                },
            });
            Resources.Add("CheckoutButtonStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.TextColorProperty, Value = AppColor("WhiteColor") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.Center },
                },
            });
            Resources.Add("ShoppingCartStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("MediumSize") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.MarginProperty, Value = new Thickness(0, 12) },
                },
            });
            Content = new Grid()
            {
                RowDefinitions = Rows.Define(Star),
                Children =
                {
                    new Grid()
                    {
                        RowDefinitions = Rows.Define(Star,60),
                        Children =
                        {
                            new CollectionView()
                            {
                                ItemTemplate = new DataTemplate(typeof(BasketItemTemplate)),
                                Footer = new HorizontalStackLayout()
                                {
                                    Children =
                                    {
                                        new Label()
                                        {
                                            Style = (Style)Resources["CartTotalStyle"],
                                            Text = "TOTAL",
                                            /*TextColor = Application.Current?.RequestedTheme switch
                                            {
                                                AppTheme.Dark => AppColor("DarkFontColor"),
                                                AppTheme.Light or _ => AppColor("LightFontColor"),
                                            },*/
                                            HorizontalOptions = LayoutOptions.EndAndExpand,
                                            VerticalOptions = LayoutOptions.CenterAndExpand,
                                        }.AppThemeColorBinding(Label.TextColorProperty, AppColor("LightFontColor"), AppColor("DarkFontColor")),
                                        new Label()
                                        {
                                            Style = (Style)Resources["CartTotalStyle"],
                                            TextColor = AppColor("GreenColor"),
                                            VerticalOptions = LayoutOptions.CenterAndExpand,
                                        }.End()
                                         .Bindv2(static (BasketViewModel vm) => vm.Total, stringFormat: "${0:N}"),
                                    },
                                }.Padding(8)
                                 .Row(0),
                            }.Row(0)
                             .Bindv2(static (BasketViewModel vm) => vm.BasketItems)
                             .Bindv2(CollectionView.SelectionChangedCommandProperty, static (BasketViewModel vm) => vm.AddCommand)
                             .Bind(CollectionView.SelectionChangedCommandParameterProperty, nameof(CollectionView.SelectedItem), source: RelativeBindingSource.Self),
                            new Grid()
                            {
                                BackgroundColor = AppColor("LightGreenColor"),
                                ColumnSpacing = 0,
                                RowSpacing = 0,
                                GestureRecognizers =
                                {
                                    new TapGestureRecognizer()
                                    {
                                        NumberOfTapsRequired = 1,
                                    }.BindCommand(static (BasketViewModel vm) => vm.CheckoutCommand),
                                },
                                Children =
                                {
                                    new Label()
                                    {
                                        Style = (Style)Resources["CheckoutButtonStyle"],
                                        Text = "[ CHECKOUT ]",
                                    },
                                },
                            }.Row(1)
                             .Padding(0),
                        },
                    }.Bindv2(Grid.IsVisibleProperty, static (BasketViewModel vm) => vm.BasketItems.Count, converter: (IValueConverter)AppResource("HasCountConverter")),
                    new Grid()
                    {
                        Children =
                        {
                            new Label()
                            {
                                Text = "EMPTY SHOPPING CART",
                            }.Center(),
                        },
                    }.Bindv2(Grid.IsVisibleProperty, static (BasketViewModel vm) => vm.BasketItems.Count, converter: (IValueConverter)AppResource("DoesNotHaveCountConverter")),
                }
            };
        }
    }
}
