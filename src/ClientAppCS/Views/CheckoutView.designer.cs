using eShop.ClientApp.ViewModels;
using eShop.ClientApp.ViewModels.Base;
using eShop.ClientApp.Views.Templates;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;

namespace eShop.ClientApp.Views
{
    public partial class CheckoutView : ContentPageBase
    {
        private void InitializeComponent()
        {
            Title = "CHECKOUT";
            Resources.Add("OrderTitleStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("MediumSize") },
                    new() { Property = Label.TextColorProperty, Value = Gray },
                },
            });
            Resources.Add("OrderContentStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("MediumSize") },
                },
            });
            Resources.Add("ShippingAddressStyle", new Style(typeof(Label))
            {
                BasedOn = (Style)Resources["OrderTitleStyle"],
                Setters =
                {
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("LargeSize") },
                },
            });
            Resources.Add("AddressStyle", new Style(typeof(Label))
            {
                BasedOn = (Style)Resources["OrderContentStyle"],
                Setters =
                {
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("LittleSize") },
                },
            });
            Resources.Add("OrderTotalStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("LargerSize") },
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
            Content = new Grid()
            {
                RowDefinitions = Rows.Define(Auto,Star,60),
                Children =
                {
                    new Grid()
                    {
                        ColumnDefinitions = Columns.Define(Star,Star),
                        RowDefinitions = Rows.Define(Auto,Auto,8,Auto,Auto,8,Auto,Auto),
                        Children =
                        {
                            new Label()
                            {
                                Style = (Style)Resources["OrderTitleStyle"],
                                Text = "ORDER NUMBER",
                            }.Row(0)
                             .Column(0),
                            new Label()
                            {
                                Style = (Style)Resources["OrderContentStyle"],
                            }.Row(1)
                             .Column(0)
                             .Bindv2(static (CheckoutViewModel vm) => vm.Order.OrderNumber),
                            new Label()
                            {
                                Style = (Style)Resources["OrderTitleStyle"],
                                Text = "TOTAL",
                            }.Row(3)
                             .Column(0),
                            new Label()
                            {
                                Style = (Style)Resources["OrderContentStyle"],
                            }.Row(4)
                             .Column(0)
                             .Bindv2(static (CheckoutViewModel vm) => vm.Order.Total, stringFormat: "${0:N}"),
                            new Label()
                            {
                                Style = (Style)Resources["OrderTitleStyle"],
                                Text = "DATE",
                            }.Row(0)
                             .Column(1),
                            new Label()
                            {
                                Style = (Style)Resources["OrderContentStyle"],
                            }.Row(1)
                             .Column(1)
                             .Bindv2(static (CheckoutViewModel vm) => vm.Order.OrderDate, stringFormat: AppResource<string>("DateTimeFormat")),
                            new Label()
                            {
                                Style = (Style)Resources["OrderTitleStyle"],
                                Text = "STATUS",
                            }.Row(3)
                             .Column(1),
                            new Label()
                            {
                                Style = (Style)Resources["OrderContentStyle"],
                            }.Row(4)
                             .Column(1)
                             .Bindv2(static (CheckoutViewModel vm) => vm.Order.OrderStatus, converter: (IValueConverter)AppResource("OrderStatusToStringConverter")),
                            new Label()
                            {
                                Style = (Style)Resources["ShippingAddressStyle"],
                                Text = "SHIPPING ADDRESS",
                            }.Row(6)
                             .Column(0)
                             .ColumnSpan(2),
                            new VerticalStackLayout()
                            {
                                Children =
                                {
                                    new Label()
                                    {
                                        Style = (Style)Resources["AddressStyle"],
                                    }.Bindv2(static (CheckoutViewModel vm) => vm.ShippingAddress.Street),
                                    new Label()
                                    {
                                        Style = (Style)Resources["AddressStyle"],
                                    }.Bindv2(static (CheckoutViewModel vm) => vm.ShippingAddress.ZipCode),
                                    new Label()
                                    {
                                        Style = (Style)Resources["AddressStyle"],
                                    }.Bindv2(static (CheckoutViewModel vm) => vm.ShippingAddress.State),
                                    new Label()
                                    {
                                        Style = (Style)Resources["AddressStyle"],
                                    }.Bindv2(static (CheckoutViewModel vm) => vm.ShippingAddress.Country),
                                },
                            }.Row(7)
                             .Column(0)
                             .ColumnSpan(2),
                        },
                    }.Row(0)
                     .Padding(8),
                    new CollectionView()
                    {
                        ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems,
                        ItemTemplate = new DataTemplate(typeof(OrderItemTemplate)),
                        Footer = new StackLayout()
                        {
                            Children =
                            {
                                new Label()
                                {
                                    Style = (Style)Resources["OrderTotalStyle"],
                                    Text = "TOTAL",
                                }.Row(0),
                                new Label()
                                {
                                    Style = (Style)Resources["OrderTotalStyle"],
                                    TextColor = AppColor("GreenColor"),
                                }.Row(1)
                                 .Bindv2(static (CheckoutViewModel vm) => vm.Order.Total, stringFormat: "${0:N}"),
                            },
                        }.Row(1)
                         .Padding(8)
                         .End(),
                    }.Row(1)
                     .Bindv2(static (CheckoutViewModel vm) => vm.Order.OrderItems),
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
                            }.BindCommandv2(static (CheckoutViewModel vm) => vm.CheckoutCommand),
                        },
                        Children =
                        {
                            new Label()
                            {
                                Style = (Style)Resources["CheckoutButtonStyle"],
                                Text = "[ PLACE ORDER ]",
                            },
                        },
                    }.Row(2)
                     .Padding(0),
                    new ActivityIndicator()
                    {
                        Color = AppColor("LightGreenColor"),
#if (IOS || ANDROID)
                        WidthRequest = 100d,
#endif
#if WINDOWS
                        WidthRequest = 400d,
#endif
                    }.Row(0)
                     .RowSpan(2)
                     .Center()
                     .Bindv2(static (CheckoutViewModel vm) => vm.IsBusy)
                     .Bindv2(ActivityIndicator.IsVisibleProperty, static (CheckoutViewModel vm) => vm.IsBusy),
                }
            };
        }
    }
}