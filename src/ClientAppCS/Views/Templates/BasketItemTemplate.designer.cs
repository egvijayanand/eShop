using eShop.ClientApp.ViewModels;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;

namespace eShop.ClientApp.Views.Templates
{
    public partial class BasketItemTemplate : SwipeView
    {
        private void InitializeComponent()
        {
            this.Height(88);
            #region Resources
            Resources.Add("OrderItemUnitPriceStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppDouble("MidMediumSize") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Start },
                },
            });
            Resources.Add("OrderItemQuantityStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppDouble("MidMediumSize") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.End },
                },
            });
            Resources.Add("OrderTotalStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppDouble("LargerSize") },
                    new() { Property = Label.TextColorProperty, Value = AppColor("GreenColor") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.End },
                },
            });
            Resources.Add("QuantityStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.End },
                    new() { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.Fill },
                },
            });
            #endregion
            RightItems = new SwipeItems()
            {
                new SwipeItem().Text("Delete")
                 .BackgroundColor(Red)
                 .BindCommandv2(static (BasketViewModel vm) => vm.DeleteCommand, source: new RelativeBindingSource(RelativeBindingSourceMode.FindAncestorBindingContext, typeof(BasketViewModel)), parameterPath: Binding.SelfPath),
            };
            Content = new Grid()
            {
#if (IOS || ANDROID)
                Padding = Thickness.Zero,
#endif
#if WINDOWS
                Padding = new Thickness(12, 0),
#endif
                ColumnDefinitions = Columns.Define(Auto,Star),
                RowDefinitions = Rows.Define(Star,Auto,1),
                Children =
                {
                    new Image()
                    {
                        Aspect = Aspect.AspectFit,
                    }.Column(0)
                     .Row(0)
                     .Height(108)
                     .Width(108)
                     .Margins(12,0,0,0)
                     .Top()
                     .Bind("PictureUrl"),
                    new Grid()
                    {
                        RowDefinitions = Rows.Define(Star,Star,Star),
                        Children =
                        {
                            new Label().Bind("ProductName", converter: AppConverter("ToUpperConverter"))
                             .Row(0),
                            new Grid()
                            {
                                ColumnDefinitions = Columns.Define(Star,Star),
                                Children =
                                {
                                    new Label()
                                    {
                                        Style = (Style)Resources["OrderItemUnitPriceStyle"],
                                    }.Bind("UnitPrice", stringFormat: "${0:N}")
                                     .Column(0),
                                    new Label()
                                    {
                                        Style = (Style)Resources["QuantityStyle"],
                                    }.Bind("Quantity")
                                     .Column(1),
                                },
                            }.Row(1),
                            new Label()
                            {
                                Style = (Style)Resources["OrderTotalStyle"],
                            }.Bind("Total", stringFormat: "${0:N}")
                             .Row(2),
                            new Button()
                            {
                                IsVisible = DeviceInfo.Idiom.ToString() switch
                                {
                                    nameof(DeviceIdiom.Desktop) => true,
                                    _ => false,
                                },
                            }.Text("Delete")
                             .Row(2)
                             .Start()
                             .BindCommandv2(static (BasketViewModel vm) => vm.DeleteCommand, source: new RelativeBindingSource(RelativeBindingSourceMode.FindAncestorBindingContext, typeof(BasketViewModel)), parameterPath: Binding.SelfPath),
                        },
                    }.Column(1)
                     .Row(0)
                     .Margin(6),
                    new Grid()
                    {
                        Children =
                        {
                            new Label().Bind("OldUnitPrice", stringFormat: "Note that the price of this article changed in our Catalog. The old price when you originally added it to the basket was $ {0:N2}")
                             .Height(60)
                             .Fill()
                             .TextCenter(),
                        },
                    }.Column(0)
                     .ColumnSpan(2)
                     .Row(1)
                     .BackgroundColor(Color.FromArgb("#F0AD4E"))
                     .Bind(Grid.IsVisibleProperty, "HasNewPrice"),
                    new Grid().Column(0)
                     .ColumnSpan(2)
                     .Row(2)
                     .BackgroundColor(Gray),
                }
            }.BackgroundColor(Application.Current?.RequestedTheme switch { AppTheme.Dark => AppColor("DarkBackgroundColor"), AppTheme.Light or _ => AppColor("LightBackgroundColor") });
        }
    }
}
