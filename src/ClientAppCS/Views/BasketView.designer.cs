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
            #region Resources
            Resources.Add("CartTotalStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppDouble("LargerSize") },
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
                    new() { Property = Label.FontSizeProperty, Value = AppDouble("MediumSize") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.MarginProperty, Value = new Thickness(0, 12) },
                },
            });
            #endregion
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
                                            HorizontalOptions = LayoutOptions.EndAndExpand,
                                            VerticalOptions = LayoutOptions.CenterAndExpand,
                                        }.Text("TOTAL")
                                         .AppThemeColorBinding(Label.TextColorProperty, AppColor("LightFontColor"), AppColor("DarkFontColor")),
                                        new Label()
                                        {
                                            Style = (Style)Resources["CartTotalStyle"],
                                            TextColor = AppColor("GreenColor"),
                                            VerticalOptions = LayoutOptions.CenterAndExpand,
                                        }.Bindv2(static (BasketViewModel vm) => vm.Total, stringFormat: "${0:N}")
                                         .End(),
                                    },
                                }.Padding(8)
                                 .Row(0),
                            }.Row(0)
                             .Bindv2(static (BasketViewModel vm) => vm.BasketItems)
                             .Bindv2(CollectionView.SelectionChangedCommandProperty, static (BasketViewModel vm) => vm.AddCommand)
                             .Bind(CollectionView.SelectionChangedCommandParameterProperty, nameof(CollectionView.SelectedItem), source: RelativeBindingSource.Self),
                            new Grid()
                            {
                                ColumnSpacing = 0d,
                                RowSpacing = 0d,
                                GestureRecognizers =
                                {
                                    new TapGestureRecognizer()
                                    {
                                        NumberOfTapsRequired = 1,
                                    }.BindCommandv2(static (BasketViewModel vm) => vm.CheckoutCommand),
                                },
                                Children =
                                {
                                    new Label()
                                    {
                                        Style = (Style)Resources["CheckoutButtonStyle"],
                                    }.Text("[ CHECKOUT ]"),
                                },
                            }.Row(1)
                             .Padding(0)
                             .BackgroundColor(AppColor("LightGreenColor")),
                        },
                    }.Bindv2(Grid.IsVisibleProperty, static (BasketViewModel vm) => vm.BasketItems.Count, converter: AppConverter("HasCountConverter")),
                    new Grid()
                    {
                        Children =
                        {
                            new Label().Text("EMPTY SHOPPING CART")
                             .Center(),
                        },
                    }.Bindv2(Grid.IsVisibleProperty, static (BasketViewModel vm) => vm.BasketItems.Count, converter: AppConverter("DoesNotHaveCountConverter")),
                }
            };
        }
    }
}
