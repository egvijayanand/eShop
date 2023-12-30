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
            #region Resources
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
            #endregion
            Triggers.Add(new EventTrigger()
            {
                Event = nameof(ContentPage.Appearing),
                Actions =
                {
                    new BeginAnimation()
                    {
                        Animation = (StoryBoard)Resources["ProductsAnimation"],
                    },
                },
            });
            #region Toolbar
            ToolbarItems.Add(new ToolbarItem().BindCommandv2(static (CatalogViewModel vm) => vm.ShowFilterCommand)
             .DynamicResource(ToolbarItem.IconImageSourceProperty, "FilterIconForTitleImageSource"));
            #endregion
            Content = new Grid()
            {
                ColumnSpacing = 0d,
                RowSpacing = 0d,
                Children =
                {
                    new Label().Text("NO PRODUCTS FOUND")
                     .Center()
                     .Bindv2(Label.IsVisibleProperty, static (CatalogViewModel vm) => vm.Products.Count, converter: AppConverter("DoesNotHaveCountConverter")),
                    new CollectionView()
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        SelectionMode = SelectionMode.Single,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Never,
                        ItemsLayout = new GridItemsLayout(DeviceInfo.Idiom.ToString() switch { nameof(DeviceIdiom.Desktop) => 4, _ => 2 }, ItemsLayoutOrientation.Vertical),
                        ItemTemplate = new DataTemplate(typeof(ProductTemplate)),
                    }.Bindv2(CollectionView.SelectedItemProperty, static (CatalogViewModel vm) => vm.SelectedProduct, BindingMode.TwoWay)
                     .Bindv2(CollectionView.IsVisibleProperty, static (CatalogViewModel vm) => vm.Products.Count, converter: AppConverter("HasCountConverter"))
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
                        Content = new Button()
                        {
                            CornerRadius = 8,
                        }.Padding(8)
                         .Height(56)
                         .Width(56)
                         .BackgroundColor(AppColor("LightGrayColor"))
                         .BindCommandv2(static (CatalogViewModel vm) => vm.ViewBasketCommand)
                         .DynamicResource(Button.ImageSourceProperty, "BasketIconForTitleImageSource"),
                    }.Bindv2(BadgeView.TextProperty, static (CatalogViewModel vm) => vm.BadgeCount, BindingMode.OneWay)
                     .Margin(16)
                     .End()
                     .Bottom()
                     .AppThemeColorBinding(BadgeView.BadgeColorProperty, AppColor("DarkBackgroundColor"), AppColor("LightBackgroundColor"))
                     .AppThemeColorBinding(BadgeView.TextColorProperty, AppColor("DarkForegroundColor"), AppColor("LightForegroundColor"))
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
