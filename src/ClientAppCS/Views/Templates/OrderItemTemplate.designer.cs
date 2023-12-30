namespace eShop.ClientApp.Views.Templates
{
    public partial class OrderItemTemplate : ContentView
    {
        private void InitializeComponent()
        {
            #region Resources
            Resources.Add("OrderItemTitleStyle", new Style(typeof(Label))
            {
                BasedOn = AppStyle("MediumSizeFontStyle"),
                Setters =
                {
                    new() { Property = Label.LineBreakModeProperty, Value = LineBreakMode.HeadTruncation },
                },
            });
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
            #endregion
            Content = new Grid()
            {
                ColumnDefinitions = Columns.Define(Auto,Star),
                RowDefinitions = Rows.Define(Star,1),
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
                            new Label()
                            {
                                Style = (Style)Resources["OrderItemTitleStyle"],
                            }.Bind("ProductName", converter: AppConverter("ToUpperConverter"))
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
                                        Style = (Style)Resources["OrderItemQuantityStyle"],
                                    }.Bind("Quantity")
                                     .Column(1),
                                },
                            }.Row(1),
                            new Label()
                            {
                                Style = (Style)Resources["OrderTotalStyle"],
                            }.Bind("Total", stringFormat: "${0:N}")
                             .Row(2),
                        },
                    }.Column(1)
                     .Row(0)
                     .Margin(6),
                    new Grid().Column(0)
                     .ColumnSpan(2)
                     .Row(1)
                     .BackgroundColor(AppColor("SeparatorLineColor")),
                }
            }.Height(120);
        }
    }
}
