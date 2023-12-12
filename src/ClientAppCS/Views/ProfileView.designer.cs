using eShop.ClientApp.ViewModels;
using eShop.ClientApp.ViewModels.Base;
using eShop.ClientApp.Views.Templates;

namespace eShop.ClientApp.Views
{
    public partial class ProfileView : ContentPageBase
    {
        private void InitializeComponent()
        {
            Title = "MY PROFILE";
            Resources.Add("LoginButtonStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.TextColorProperty, Value = AppColor("WhiteColor") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.Center },
                },
            });
            Resources.Add("MyOrdersLabelStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("MediumSize") },
                    new() { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                    new() { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
                    new() { Property = Label.MarginProperty, Value = new Thickness(0,12) },
                },
            });
            ToolbarItems.Add(new ToolbarItem().Title("LOGOUT")
             .BindCommand(static (ProfileViewModel vm) => vm.LogoutCommand));
            Content = new Grid()
            {
                RowDefinitions = Rows.Define(Star, Auto),
                Children =
                {
                    new Grid()
                    {
                        Children =
                        {
                            new Label()
                            {
                                Text = "NO ORDERS",
                            }.Center()
                             .Bindv2(Label.IsVisibleProperty, static (ProfileViewModel vm) => vm.Orders.Count, converter: (IValueConverter)AppResource("DoesNotHaveCountConverter")),
                        },
                    }.Bindv2(Grid.IsVisibleProperty, static (ProfileViewModel vm) => vm.IsBusy, converter: (IValueConverter)AppResource("InverseBoolConverter")),
                    new CollectionView()
                    {
                        ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems,
                        SelectionMode = SelectionMode.Single,
                        Header = new Label()
                        {
                            Style = (Style)Resources["MyOrdersLabelStyle"],
                            Text = "MY ORDERS",
                        },
                        ItemTemplate = new DataTemplate(typeof(OrderTemplate)),
                    }.Bindv2(CollectionView.SelectedItemProperty, static (ProfileViewModel vm) => vm.SelectedOrder, BindingMode.TwoWay)
                     .Bindv2(static (ProfileViewModel vm) => vm.Orders)
                     .Bindv2(CollectionView.SelectionChangedCommandProperty, static (ProfileViewModel vm) => vm.OrderDetailCommand)
                     .Bind(CollectionView.SelectionChangedCommandParameterProperty, nameof(CollectionView.SelectedItem), source: RelativeBindingSource.Self),
                }
            };
        }
    }
}
