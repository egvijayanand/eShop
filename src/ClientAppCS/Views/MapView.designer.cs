using eShop.ClientApp.ViewModels;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace eShop.ClientApp.Views
{
    public partial class MapView : ContentPageBase
    {
        private void InitializeComponent()
        {
            Title = "Microsoft Visitor Center";
            Content = new Map(new MapSpan(new Location(47.6423109, -122.1368406), 0.01, 0.01))
            {
                IsScrollEnabled = false,
                IsZoomEnabled = false,
                ItemTemplate = new DataTemplate(() =>
                {
                    return new Pin().Bindv2(Pin.LocationProperty, static (Store vm) => vm.Location)
                     .Bindv2(Pin.AddressProperty, static (Store vm) => vm.Address)
                     .Bindv2(Pin.LabelProperty, static (Store vm) => vm.Description)
                     .Invoke(x => x.MarkerClicked += Pin_MarkerClicked);
                }),
            }.Bindv2(Map.ItemsSourceProperty, static (MapViewModel vm) => vm.Stores, BindingMode.OneWay);
        }
    }
}
