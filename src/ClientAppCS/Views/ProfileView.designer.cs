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
            #region Resources
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
                    new() { Property = Label.FontSizeProperty, Value = AppDouble("MediumSize") },
                    new() { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                    new() { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
                    new() { Property = Label.MarginProperty, Value = new Thickness(0,12) },
                },
            });
            #endregion
            #region Toolbar
            ToolbarItems.Add(new ToolbarItem().Text("LOGOUT")
             .BindCommandv2(static (ProfileViewModel vm) => vm.LogoutCommand));
            #endregion
            Content = new Grid()
            {
                RowDefinitions = Rows.Define(Star, Auto),
                Children =
                {
                    new Grid()
                    {
                        Children =
                        {
                            new Label().Text("NO ORDERS")
                             .Center()
                             .Bindv2(Label.IsVisibleProperty, static (ProfileViewModel vm) => vm.Orders.Count, BindingMode.OneWay, converter: AppConverter("DoesNotHaveCountConverter")),
                        },
                    }.Bindv2(Grid.IsVisibleProperty, static (ProfileViewModel vm) => vm.IsBusy, converter: AppConverter("InverseBoolConverter")),
                    new CollectionView()
                    {
                        ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems,
                        SelectionMode = SelectionMode.Single,
                        Header = new Label()
                        {
                            Style = (Style)Resources["MyOrdersLabelStyle"],
                        }.Text("MY ORDERS"),
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
