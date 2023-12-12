using eShop.ClientApp.Animations;
using eShop.ClientApp.Triggers;
using eShop.ClientApp.ViewModels;
using eShop.ClientApp.ViewModels.Base;
using eShop.ClientApp.Views.Templates;
using Microsoft.Maui.Devices;

namespace eShop.ClientApp.Views
{
    public partial class CampaignView : ContentPageBase
    {
        private void InitializeComponent()
        {
            Title = "CAMPAIGNS";
            Resources.Add("CampaignsListStyle", new Style(typeof(ListView))
            {
                Setters =
                {
                    new() { Property = ListView.RowHeightProperty, Value = 400 },
                    new() { Property = ListView.VerticalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = ListView.MarginProperty, Value = Thickness.Zero },
                },
            });
            Resources.Add("CampaignsAnimation", new StoryBoard()
            {
                Target = Campaigns,
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
                        Animation = (StoryBoard)Resources["CampaignsAnimation"],
                    },
                },
            });
            Content = new Grid()
            {
                ColumnSpacing = 0,
                RowSpacing = 0,
                RowDefinitions = Rows.Define(Auto,Star),
                Children =
                {
                    new Grid()
                    {
                        Children =
                        {
                            new Grid()
                            {
                                Children =
                                {
                                    new Label()
                                    {
                                        Text = "NO CAMPAIGNS FOUND",
                                    }.Center()
                                     .Bind(Label.IsVisibleProperty, "Campaigns.Count", converter: (IValueConverter)AppResource("DoesNotHaveCountConverter")),
                                },
                            }.Bindv2(Grid.IsVisibleProperty, static (CampaignViewModel vm) => vm.IsBusy, converter: (IValueConverter)AppResource("InverseBoolConverter")),
                            new CollectionView()
                            {
                                Style = (Style)Resources["CampaignsListStyle"],
                                ItemTemplate = new DataTemplate(typeof(CampaignTemplate)),
                            }.Bindv2(CollectionView.IsVisibleProperty, static (CampaignViewModel vm) => vm.Campaigns.Count, converter: (IValueConverter)AppResource("CountToBoolConverter"))
                             .Bindv2(static (CampaignViewModel vm) => vm.Campaigns)
                             .Bindv2(CollectionView.SelectionChangedCommandProperty, static (CampaignViewModel vm) => vm.GetCampaignDetailsCommand)
                             .Bind(CollectionView.SelectionChangedCommandParameterProperty, nameof(CollectionView.SelectedItem), source: RelativeBindingSource.Self)
                             .Assign(out Campaigns),
                        },
                    }.Row(1),
                    new ActivityIndicator()
                    {
                        Color = AppColor("LightGreenColor"),
#if (IOS || ANDROID)
                        WidthRequest = 100d,
#endif
#if WINDOWS
                        WidthRequest = 400d,
#endif
                    }.Row(0)
                     .RowSpan(2)
                     .Center()
                     .Bindv2(static (CampaignViewModel vm) => vm.IsBusy)
                     .Bindv2(ActivityIndicator.IsVisibleProperty, static (CampaignViewModel vm) => vm.IsBusy),
                }
            };
        }

        #region Variables
        private CollectionView Campaigns;
        #endregion
    }
}
