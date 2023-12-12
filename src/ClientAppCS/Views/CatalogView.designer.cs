using eShop.ClientApp.Animations;
using eShop.ClientApp.Triggers;
using eShop.ClientApp.ViewModels;
using eShop.ClientApp.Views.Templates;
using Microsoft.Maui.Devices;

namespace eShop.ClientApp.Views
{
    public partial class CatalogView : ContentPageBase
    {
        private void InitializeComponent()
        {
            Title = "CATALOG";
            Resources.Add("FilterLabelStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.TextColorProperty, Value = AppColor("WhiteColor") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.Center },
                },
            });
            Resources.Add("ProductsAnimation", new StoryBoard()
            {
                Target = Products,
                Animations =
                {
                    new FadeInAnimation()
                    {
                        Delay = 250,
                        Direction = FadeInAnimation.FadeDirection.Up,
                        Duration = "1500",
                    },
                }
            });
            Triggers.Add(new EventTrigger()
            {
                Event = "Appearing",
                Actions =
                {
                    new BeginAnimation()
                    {
                        Animation = (StoryBoard)Resources["ProductsAnimation"],
                    },
                },
            });
            ToolbarItems.Add(new ToolbarItem().BindCommandv2(static (CatalogViewModel vm) => vm.ShowFilterCommand, BindingMode.OneTime)
             .DynamicResource(ToolbarItem.IconImageSourceProperty, "FilterIconForTitleImageSource"));
            Content = new Grid()
            {
                ColumnSpacing = 0,
                RowSpacing = 0,
                Children =
                {
                    new Label()
                    {
                        Text = "NO PRODUCTS FOUND",
                    }.Center()
                     .Bindv2(Label.IsVisibleProperty, static (CatalogViewModel vm) => vm.Products.Count, converter: (IValueConverter)AppResource("DoesNotHaveCountConverter")),
                    new CollectionView()
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        SelectionMode = SelectionMode.Single,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Never,
                        ItemsLayout = new GridItemsLayout(DeviceInfo.Idiom.ToString() switch { nameof(DeviceIdiom.Desktop) => 4, _ => 2 }, ItemsLayoutOrientation.Vertical),
                        ItemTemplate = new DataTemplate(typeof(ProductTemplate)),
                    }.Bindv2(CollectionView.SelectedItemProperty, static (CatalogViewModel vm) => vm.SelectedProduct, BindingMode.TwoWay)
                     .Bindv2(CollectionView.IsVisibleProperty, static (CatalogViewModel vm) => vm.Products.Count, converter: (IValueConverter)AppResource("HasCountConverter"))
                     .Bindv2(static (CatalogViewModel vm) => vm.Products, BindingMode.OneTime)
                     .Bindv2(CollectionView.SelectionChangedCommandProperty, static (CatalogViewModel vm) => vm.AddCatalogItemCommand)
                     .Bind(CollectionView.SelectionChangedCommandParameterProperty, nameof(CollectionView.SelectedItem), source: RelativeBindingSource.Self)
                     .Assign(out Products),
                    new ActivityIndicator()
                    {
                        Color = AppColor("BlackColor"),
                    }.Center()
                     .Bindv2(static (CatalogViewModel vm) => vm.IsBusy)
                     .Bindv2(ActivityIndicator.IsVisibleProperty, static (CatalogViewModel vm) => vm.IsBusy),
                    new BadgeView()
                    {
                        BadgeColor = Application.Current?.RequestedTheme switch
                        {
                            AppTheme.Dark => AppColor("LightBackgroundColor"),
                            AppTheme.Light or _ => AppColor("DarkBackgroundColor"),
                        },
                        TextColor = Application.Current?.RequestedTheme switch
                        {
                            AppTheme.Dark => AppColor("LightForegroundColor"),
                            AppTheme.Light or _ => AppColor("DarkForegroundColor"),
                        },
                        Content = new Button()
                        {
                            BackgroundColor = AppColor("LightGrayColor"),
                            CornerRadius = 8,
                        }.Padding(8)
                         .Height(56)
                         .Width(56)
                         .BindCommandv2(static (CatalogViewModel vm) => vm.ViewBasketCommand, BindingMode.OneTime)
                         .DynamicResource(Button.ImageSourceProperty, "BasketIconForTitleImageSource"),
                    }.Margin(16)
                     .End().Bottom()
                     .Bindv2(BadgeView.TextProperty, static (CatalogViewModel vm) => vm.BadgeCount, BindingMode.OneWay)
                     .Assign(out badge),
                }
            };
        }

        #region Variables
        private CollectionView Products;
        private BadgeView badge;
        #endregion
    }
}
