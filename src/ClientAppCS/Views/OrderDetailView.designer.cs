using eShop.ClientApp.Animations;
using eShop.ClientApp.Triggers;
using eShop.ClientApp.ViewModels;
using eShop.ClientApp.ViewModels.Base;
using eShop.ClientApp.Views.Templates;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;

namespace eShop.ClientApp.Views
{
    public partial class OrderDetailView : ContentPageBase
    {
        private void InitializeComponent()
        {
            SetBinding(TitleProperty, new Binding(PropertyName(static (OrderDetailViewModel vm) => vm.Order.OrderNumber), stringFormat: "ORDER {0}"));
            #region Resources
            Resources.Add("TitleStyle", new Style(typeof(Label))
            {
                BasedOn = AppStyle("MediumSizeFontStyle"),
                Setters =
                {
                    new() { Property = Label.TextColorProperty, Value = Gray },
                },
            });
            Resources.Add("OrderContentStyle", new Style(typeof(Label)));
            Resources.Add("AddressStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontSizeProperty, Value = AppDouble("LittleSize") },
                },
            });
            Resources.Add("OrderTotalStyle", new Style(typeof(Label))
            {
                BasedOn = AppStyle("MediumSizeFontStyle"),
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppDouble("LargerSize") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.End },
                },
            });
            Resources.Add("CancelOrderButtonStyle", new Style(typeof(Button))
            {
                Setters =
                {
                    new() { Property = Button.TextColorProperty, Value = AppColor("WhiteColor") },
                    new() { Property = Button.BackgroundColorProperty, Value = AppColor("LightGreenColor") },
                },
            });
            Resources.Add("OrderInfoAnimation", new StoryBoard()
            {
                Target = OrderInfo,
                Animations =
                {
                    new FadeToAnimation()
                    {
                        Delay = 100,
                        Opacity = 1f,
                        Duration = "500",
                    },
                }
            });
            Resources.Add("OrderItemsAnimation", new StoryBoard()
            {
                Target = OrderItems,
                Animations =
                {
                    new FadeToAnimation()
                    {
                        Delay = 300,
                        Opacity = 1f,
                        Duration = "1500",
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
                        Animation = (StoryBoard)Resources["OrderInfoAnimation"],
                    },
                    new BeginAnimation()
                    {
                        Animation = (StoryBoard)Resources["OrderItemsAnimation"],
                    },
                },
            });
            Content = new Grid()
            {
                RowDefinitions = Rows.Define(Auto,Auto,Star),
                Children =
                {
                    new StackLayout()
                    {
                        Children =
                        {
                            new Button()
                            {
                                Style = (Style)Resources["CancelOrderButtonStyle"],
                            }.Text("CANCEL ORDER")
                             .BindCommandv2(static (OrderDetailViewModel vm) => vm.ToggleCancelOrderCommand),
                        },
                    }.Row(0)
                     .Height(50)
                     .Bindv2(StackLayout.IsVisibleProperty, static (OrderDetailViewModel vm) => vm.IsSubmittedOrder),
                    new Grid()
                    {
                        RowDefinitions = Rows.Define(Auto,Auto),
                        Children =
                        {
                            new Grid()
                            {
                                Opacity = 0f,
                                ColumnDefinitions = Columns.Define(Star,Star),
                                RowDefinitions = Rows.Define(Auto,Auto,8,Auto,Auto,8,Auto,Auto),
                                Children =
                                {
                                    new Label()
                                    {
                                        Style = (Style)Resources["TitleStyle"],
                                    }.Text("ORDER NUMBER")
                                     .Row(0)
                                     .Column(0),
                                    new Label()
                                    {
                                        Style = (Style)Resources["OrderContentStyle"],
                                    }.Bindv2(static (OrderDetailViewModel vm) => vm.Order.OrderNumber)
                                     .Row(1)
                                     .Column(0),
                                    new Label()
                                    {
                                        Style = (Style)Resources["TitleStyle"],
                                    }.Text("TOTAL")
                                     .Row(3)
                                     .Column(0),
                                    new Label()
                                    {
                                        Style = (Style)Resources["OrderContentStyle"],
                                    }.Bindv2(static (OrderDetailViewModel vm) => vm.Order.Total, stringFormat: "${0:N}")
                                     .Row(4)
                                     .Column(0),
                                    new Label()
                                    {
                                        Style = (Style)Resources["TitleStyle"],
                                    }.Text("DATE")
                                     .Row(0)
                                     .Column(1),
                                    new Label()
                                    {
                                        Style = (Style)Resources["OrderContentStyle"],
                                    }.Bindv2(static (OrderDetailViewModel vm) => vm.Order.OrderDate, stringFormat: AppString("DateTimeFormat"))
                                     .Row(1)
                                     .Column(1),
                                    new Label()
                                    {
                                        Style = (Style)Resources["TitleStyle"],
                                    }.Text("STATUS")
                                     .Row(3)
                                     .Column(1),
                                    new Label()
                                    {
                                        Style = (Style)Resources["OrderContentStyle"],
                                    }.Bindv2(static (OrderDetailViewModel vm) => vm.Order.OrderStatus, converter: AppConverter("OrderStatusToStringConverter"))
                                     .Row(4)
                                     .Column(1),
                                    new Label()
                                    {
                                        Style = (Style)Resources["TitleStyle"],
                                    }.Text("SHIPPING ADDRESS")
                                     .Row(6)
                                     .Column(0)
                                     .ColumnSpan(2),
                                    new VerticalStackLayout()
                                    {
                                        Children =
                                        {
                                            new Label()
                                            {
                                                Style = (Style)Resources["AddressStyle"],
                                            }.Bindv2(static (OrderDetailViewModel vm) => vm.Order.ShippingStreet),
                                            new Label()
                                            {
                                                Style = (Style)Resources["AddressStyle"],
                                            }.Bindv2(static (OrderDetailViewModel vm) => vm.Order.ShippingCity),
                                            new Label()
                                            {
                                                Style = (Style)Resources["AddressStyle"],
                                            }.Bindv2(static (OrderDetailViewModel vm) => vm.Order.ShippingState),
                                            new Label()
                                            {
                                                Style = (Style)Resources["AddressStyle"],
                                            }.Bindv2(static (OrderDetailViewModel vm) => vm.Order.ShippingCountry),
                                        },
                                    }.Row(7)
                                     .Column(0)
                                     .ColumnSpan(2),
                                },
                            }.Padding(8)
                             .Assign(out OrderInfo),
                        },
                    }.Row(1),
                    new CollectionView()
                    {
                        ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems,
                        ItemTemplate = new DataTemplate(typeof(OrderItemTemplate)),
                        Footer = new VerticalStackLayout()
                        {
                            Children =
                            {
                                new Label()
                                {
                                    Style = (Style)Resources["OrderTotalStyle"],
                                }.Text("TOTAL"),
                                new Label()
                                {
                                    Style = (Style)Resources["OrderTotalStyle"],
                                    TextColor = AppColor("GreenColor"),
                                }.Bindv2(static (OrderDetailViewModel vm) => vm.Order.Total, stringFormat: "${0:N}"),
                            },
                        }.Padding(8),
                    }.Row(2)
                     .Bindv2(static (OrderDetailViewModel vm) => vm.Order.OrderItems)
                     .Assign(out OrderItems),
                    new ActivityIndicator()
                    {
                        Color = AppColor("LightGreenColor"),
#if (IOS || ANDROID)
                        WidthRequest = 100d,
#endif
#if WINDOWS
                        WidthRequest = 400d,
#endif
                    }.Row(1)
                     .Center()
                     .Bindv2(static (OrderDetailViewModel vm) => vm.IsBusy)
                     .Bindv2(ActivityIndicator.IsVisibleProperty, static (OrderDetailViewModel vm) => vm.IsBusy),
                }
            };
        }

        #region Variables
        private Grid OrderInfo;
        private CollectionView OrderItems;
        #endregion
    }
}
