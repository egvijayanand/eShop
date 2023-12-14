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
            Resources.Add("OrderItemUnitPriceStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("MidMediumSize") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Start },
                },
            });
            Resources.Add("OrderItemQuantityStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("MidMediumSize") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.End },
                },
            });
            Resources.Add("OrderTotalStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("LargerSize") },
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
            RightItems = new SwipeItems()
            {
                new SwipeItem()
                {
                    Text = "Delete",
                    BackgroundColor = Red,
                }.BindCommandv2(static (BasketViewModel vm) => vm.DeleteCommand, source: new RelativeBindingSource(typeof(BasketViewModel).IsSubclassOf(typeof(Element)) ? RelativeBindingSourceMode.FindAncestor : RelativeBindingSourceMode.FindAncestorBindingContext, typeof(BasketViewModel)), parameterPath: Binding.SelfPath),
            };
            Content = new Grid()
            {
                /*BackgroundColor = Application.Current?.RequestedTheme switch
                {
                    AppTheme.Dark => AppColor("DarkBackgroundColor"),
                    AppTheme.Light or _ => AppColor("LightBackgroundColor"),
                },*/
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
                            new Label().Row(0)
                             .Bind("ProductName", converter: (IValueConverter)AppResource("ToUpperConverter")),
                            new Grid()
                            {
                                ColumnDefinitions = Columns.Define(Star,Star),
                                Children =
                                {
                                    new Label()
                                    {
                                        Style = (Style)Resources["OrderItemUnitPriceStyle"],
                                    }.Column(0)
                                     .Bind("UnitPrice", stringFormat: "${0:N}"),
                                    new Label()
                                    {
                                        Style = (Style)Resources["QuantityStyle"],
                                    }.Column(1)
                                     .Bind("Quantity"),
                                },
                            }.Row(1),
                            new Label()
                            {
                                Style = (Style)Resources["OrderTotalStyle"],
                            }.Row(2)
                             .Bind("Total", stringFormat: "${0:N}"),
                            new Button()
                            {
                                IsVisible = DeviceInfo.Idiom.ToString() switch
                                {
                                    nameof(DeviceIdiom.Desktop) => true,
                                    _ => false,
                                },
                                Text = "Delete",
                            }.Row(2)
                             .Start()
                             .BindCommandv2(static (BasketViewModel vm) => vm.DeleteCommand, source: new RelativeBindingSource(typeof(BasketViewModel).IsSubclassOf(typeof(Element)) ? RelativeBindingSourceMode.FindAncestor : RelativeBindingSourceMode.FindAncestorBindingContext, typeof(BasketViewModel)), parameterPath: Binding.SelfPath),
                        },
                    }.Column(1)
                     .Row(0)
                     .Margin(6),
                    new Grid()
                    {
                        BackgroundColor = Color.FromArgb("#F0AD4E"),
                        Children =
                        {
                            new Label().Height(60)
                             .Fill()
                             .TextCenter()
                             .Bind("OldUnitPrice", stringFormat: "Note that the price of this article changed in our Catalog. The old price when you originally added it to the basket was $ {0:N2}"),
                        },
                    }.Column(0)
                     .ColumnSpan(2)
                     .Row(1)
                     .Bind(Grid.IsVisibleProperty, "HasNewPrice"),
                    new Grid()
                    {
                        BackgroundColor = Gray,
                    }.Column(0)
                     .ColumnSpan(2)
                     .Row(2),
                }
            }.AppThemeColorBinding(Grid.BackgroundColorProperty, AppColor("LightBackgroundColor"), AppColor("DarkBackgroundColor"));
        }
    }
}
