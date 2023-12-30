using eShop.ClientApp.ViewModels;
using ios = Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;

namespace eShop.ClientApp.Views
{
    public partial class FiltersView : ContentPage
    {
        private void InitializeComponent()
        {
            BackgroundColor = Color.FromArgb("#01FFFFFF");
            Shell.SetPresentationMode(this, PresentationMode.ModalAnimated);
            Content = new Grid()
            {
                ColumnDefinitions = Columns.Define(Star, Stars(2), Star),
                Children =
                {
                    new Border()
                    {
                        StrokeShape = new RoundRectangle()
                        {
                            CornerRadius = 8,
                        },
                        Content = new VerticalStackLayout()
                        {
                            Spacing = 8d,
                            Children =
                            {
                                new Picker()
                                {
                                    Title = "BRAND",
                                    ItemDisplayBinding = new Binding("Brand"),
                                }.Bindv2(Picker.ItemsSourceProperty, static (CatalogViewModel vm) => vm.Brands)
                                 .Bindv2(Picker.SelectedItemProperty, static (CatalogViewModel vm) => vm.Brand, BindingMode.TwoWay)
                                 .Invoke(pkr => ios.Picker.SetUpdateMode(pkr, ios.UpdateMode.WhenFinished)),
                                new Picker()
                                {
                                    Title = "TYPE",
                                    ItemDisplayBinding = new Binding("Type"),
                                }.Bindv2(Picker.ItemsSourceProperty, static (CatalogViewModel vm) => vm.Types)
                                 .Bindv2(Picker.SelectedItemProperty, static (CatalogViewModel vm) => vm.Type, BindingMode.TwoWay)
                                 .Invoke(pkr => ios.Picker.SetUpdateMode(pkr, ios.UpdateMode.WhenFinished)),
                                new Button().Text("Apply")
                                 .BindCommandv2(static (CatalogViewModel vm) => vm.FilterCommand)
                                 .AddVisualState(CreateVisualStateGroupList(new[]
                                 {
                                     new VisualStateGroup()
                                     {
                                         Name = nameof(VisualStateManager.CommonStates),
                                         States =
                                         {
                                             new VisualState()
                                             {
                                                 Name = VisualStateManager.CommonStates.Normal,
                                                 Setters =
                                                 {
                                                     new Setter()
                                                     {
                                                         Property = Button.OpacityProperty,
                                                         Value = 1,
                                                     },
                                                 },
                                             },
                                             new VisualState()
                                             {
                                                 Name = VisualStateManager.CommonStates.Disabled,
                                                 Setters =
                                                 {
                                                     new Setter()
                                                     {
                                                         Property = Button.OpacityProperty,
                                                         Value = .5,
                                                     },
                                                 },
                                             },
                                         },
                                     },
                                 })),
                                new Button().Text("Clear")
                                 .Bindv2(Button.IsEnabledProperty, static (CatalogViewModel vm) => vm.IsFilter, BindingMode.OneWay)
                                 .BindCommandv2(static (CatalogViewModel vm) => vm.ClearFilterCommand)
                                 .AddVisualState(CreateVisualStateGroupList(new[]
                                 {
                                     new VisualStateGroup()
                                     {
                                         Name = nameof(VisualStateManager.CommonStates),
                                         States =
                                         {
                                             new VisualState()
                                             {
                                                 Name = VisualStateManager.CommonStates.Normal,
                                                 Setters =
                                                 {
                                                     new Setter()
                                                     {
                                                         Property = Button.OpacityProperty,
                                                         Value = 1,
                                                     },
                                                 },
                                             },
                                             new VisualState()
                                             {
                                                 Name = VisualStateManager.CommonStates.Disabled,
                                                 Setters =
                                                 {
                                                     new Setter()
                                                     {
                                                         Property = Button.OpacityProperty,
                                                         Value = .5,
                                                     },
                                                 },
                                             },
                                         },
                                     },
                                 })),
                            },
                        }.Padding(8)
                         .AppThemeColorBinding(VerticalStackLayout.BackgroundColorProperty, AppColor("LightBackgroundColor"), AppColor("DarkBackgroundColor")),
                    }.Row(0)
                     .Column(1)
                     .AppThemeBinding(Border.BackgroundProperty, AppBrush("LightBackgroundColor"), AppBrush("DarkBackgroundColor")),
                }
            }.CenterVertical();
        }
    }
}
