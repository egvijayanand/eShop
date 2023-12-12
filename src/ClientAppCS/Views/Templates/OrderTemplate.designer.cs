using Microsoft.Maui.Graphics;

namespace eShop.ClientApp.Views.Templates
{
    public partial class OrderTemplate : ContentView
    {
        private void InitializeComponent()
        {
            Resources.Add("OrderTitleStyle", new Style(typeof(Label))
            {
                BasedOn = AppStyle("MediumSizeFontStyle"),
                Setters =
                {
                    new() { Property = Label.TextColorProperty, Value = Gray },
                },
            });
            Content = new Grid()
            {
                ColumnDefinitions = Columns.Define(Star,Star),
                RowDefinitions = Rows.Define(Star,Star,1),
                Children =
                {
                    new StackLayout()
                    {
                        Children =
                        {
                            new Label()
                            {
                                Style = (Style)Resources["OrderTitleStyle"],
                                Text = "ORDER NUMBER",
                            },
                            new Label().Bind("OrderNumber"),
                        },
                    }.Row(0)
                     .Column(0)
                     .Margin(12),
                    new StackLayout()
                    {
                        Children =
                        {
                            new Label()
                            {
                                Style = (Style)Resources["OrderTitleStyle"],
                                Text = "TOTAL",
                            },
                            new Label().Bind("Total", stringFormat: "${0:N}"),
                        },
                    }.Row(1)
                     .Column(0)
                     .Margin(12),
                    new StackLayout()
                    {
                        Children =
                        {
                            new Label()
                            {
                                Style = (Style)Resources["OrderTitleStyle"],
                                Text = "DATE",
                            },
                            new Label().Bind("OrderDate", stringFormat: AppResource<string>("DateTimeFormat")),
                        },
                    }.Row(0)
                     .Column(1)
                     .Margin(12),
                    new StackLayout()
                    {
                        Children =
                        {
                            new Label()
                            {
                                Style = (Style)Resources["OrderTitleStyle"],
                                Text = "STATUS",
                            },
                            new Label().Bind("OrderStatus", converter: (IValueConverter)AppResource("OrderStatusToStringConverter")),
                        },
                    }.Row(1)
                     .Column(1)
                     .Margin(12),
                    new Grid()
                    {
                        BackgroundColor = Gray,
                    }.Row(2)
                     .Column(0)
                     .ColumnSpan(2),
                }
            };
        }
    }
}
