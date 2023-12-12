using eShop.ClientApp.Controls;

namespace eShop.ClientApp.Views.Templates
{
    public partial class CampaignTemplate : ContentView
    {
        private void InitializeComponent()
        {
            Resources.Add("CampaignNameStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("LargeSize") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.MarginProperty, Value = new Thickness(0, 12, 0, 6) },
                },
            });
            Resources.Add("MoreDetailsButtonStyle", new Style(typeof(Grid))
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
                RowDefinitions = Rows.Define(250,Auto),
                Children =
                {
                    new Image()
                    {
                        Aspect = Aspect.AspectFill,
                    }.Row(0)
                     .Bind("PictureUri", targetNullValue: "logo_header.png"),
                    new Label()
                    {
                        Style = (Style)Resources["CampaignNameStyle"],
                    }.Row(1)
                     .Bind("Name", converter: (IValueConverter)AppResource("ToUpperConverter")),
                }
            }.Margin(0)
             .Padding(0);
        }
    }
}
