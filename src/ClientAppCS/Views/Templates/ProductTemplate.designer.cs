using eShop.ClientApp.Controls;

namespace eShop.ClientApp.Views.Templates
{
    public partial class ProductTemplate : ContentView
    {
        private void InitializeComponent()
        {
            this.Height(380);
            Resources.Add("ProductNameStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("LargeSize") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.MarginProperty, Value = new Thickness(0, 12, 0, 6) },
                },
            });
            Resources.Add("ProductPriceStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold },
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("BigSize") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.MarginProperty, Value = new Thickness(0, 6, 0, 12) },
                },
            });
            Resources.Add("AddButtonStyle", new Style(typeof(Grid))
            {
                Setters =
                {
                    new() { Property = Grid.HeightRequestProperty, Value = 42 },
                    new() { Property = Grid.WidthRequestProperty, Value = 42 },
                    new() { Property = Grid.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Grid.VerticalOptionsProperty, Value = LayoutOptions.End },
                    new() { Property = Grid.MarginProperty, Value = new Thickness(0,0,0,24) },
                },
            });
            Resources.Add("AddImageStyle", new Style(typeof(Image))
            {
                Setters =
                {
                    new() { Property = Image.HeightRequestProperty, Value = 24 },
                    new() { Property = Image.WidthRequestProperty, Value = 24 },
                },
            });
            Content = new Grid()
            {
                RowDefinitions = Rows.Define(250,Auto,Auto),
                ColumnDefinitions = Columns.Define(Stars(.5)),
                Children =
                {
                    new Image()
                    {
                        Aspect = Aspect.AspectFit,
                    }.Row(0)
                     .Bind("PictureUri", BindingMode.OneTime, targetNullValue: "logo_header.png"),
                    new Grid()
                    {
                        Style = (Style)Resources["AddButtonStyle"],
                        Children =
                        {
                            new AddBasketButton(),
                        },
                    }.Row(0)
                     .End()
                     .Bottom(),
                    new Label()
                    {
                        Style = (Style)Resources["ProductNameStyle"],
                    }.Row(1)
                     .Bind("Name", BindingMode.OneTime, converter: (IValueConverter)AppResource("ToUpperConverter")),
                    new Label()
                    {
                        Style = (Style)Resources["ProductPriceStyle"],
                    }.Row(2)
                     .Bind("Price", BindingMode.OneTime, stringFormat: "${0:N}"),
                }
            }.Margin(0)
             .Padding(10);
        }
    }
}
