using eShop.ClientApp.Converters;
using eShop.ClientApp.ViewModels;
using eShop.ClientApp.ViewModels.Base;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Layouts;

namespace eShop.ClientApp.Views
{
    public partial class CampaignDetailsView : ContentPage
    {
        private void InitializeComponent()
        {
            Title = "CAMPAIGN DETAILS";
            #region Resources
            Resources.Add("CampaignTitleStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppDouble("MediumSize") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Start },
                    new() { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.MarginProperty, Value = new Thickness(12, 0) },
                    new() { Property = Label.TextColorProperty, Value = AppColor("GreenColor") },
                },
            });
            Resources.Add("CampaignDescriptionStyle", new Style(typeof(Label))
            {
                BasedOn = (Style)Resources["CampaignTitleStyle"],
                Setters =
                {
                    new() { Property = Label.FontSizeProperty, Value = AppDouble("LittleSize") },
                },
            });
            Resources.Add("CampaignImageStyle", new Style(typeof(Image))
            {
                Setters =
                {
                    new() { Property = Image.AspectProperty, Value = Aspect.AspectFit },
                    new() { Property = Image.VerticalOptionsProperty, Value = LayoutOptions.Start },
                    new() { Property = Image.MarginProperty, Value = new Thickness(12) },
                },
            });
            Resources.Add("CampaignAvailabilityDescriptionStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "Montserrat-Regular" },
                    new() { Property = Label.TextColorProperty, Value = AppColor("WhiteColor") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.CenterAndExpand },
                    new() { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.Center },
                },
            });
            Resources.Add("CampaignViewSiteButtonStyle", new Style(typeof(Button))
            {
                Setters =
                {
                    new() { Property = Button.TextColorProperty, Value = AppColor("WhiteColor") },
                    new() { Property = Button.BackgroundColorProperty, Value = AppColor("LightGreenColor") },
                },
            });
            Resources.Add("CampaignAvailabilityBannerStyle", new Style(typeof(Grid))
            {
                Setters =
                {
                    new() { Property = Grid.BackgroundColorProperty, Value = AppColor("GrayColor") },
                    new() { Property = Grid.PaddingProperty, Value = new Thickness(12) },
                    new() { Property = Grid.VerticalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Grid.ColumnSpacingProperty, Value = 0 },
                    new() { Property = Grid.RowSpacingProperty, Value = 0 },
                },
            });
            #endregion
            Content = new Grid()
            {
                RowDefinitions = Rows.Define(Auto,Star,60),
                Children =
                {
                    new StackLayout()
                    {
                        Children =
                        {
                            new Button()
                            {
                                Style = (Style)Resources["CampaignViewSiteButtonStyle"],
                            }.Text("VIEW SITE")
                             .BackgroundColor(AppColor("LightGreenColor"))
                             .BindCommandv2(static (CampaignDetailsViewModel vm) => vm.EnableDetailsSiteCommand),
                        },
                    }.Height(50)
                     .Column(0)
                     .Row(0)
                     .Bindv2(StackLayout.IsVisibleProperty, static (CampaignDetailsViewModel vm) => vm.Campaign.DetailsUri, converter: AppConverter("StringNullOrEmptyBoolConverter")),
                    new Grid()
                    {
                        ColumnSpacing = 0d,
                        RowSpacing = 0d,
                        RowDefinitions = Rows.Define(Star),
                        Children =
                        {
                            new ScrollView()
                            {
                                Content = new StackLayout()
                                {
                                    Children =
                                    {
                                        new Grid()
                                        {
                                            RowDefinitions = Rows.Define(Auto,Auto,Star),
                                            Children =
                                            {
                                                new Image()
                                                {
                                                    Style = (Style)Resources["CampaignImageStyle"],
                                                }.Row(0)
                                                 .Bindv2(static (CampaignDetailsViewModel vm) => vm.Campaign.PictureUri),
                                                new Label()
                                                {
                                                    Style = (Style)Resources["CampaignTitleStyle"],
                                                }.Bindv2(static (CampaignDetailsViewModel vm) => vm.Campaign.Name)
                                                 .Row(1),
                                                new Label()
                                                {
                                                    Style = (Style)Resources["CampaignDescriptionStyle"],
                                                }.Bindv2(static (CampaignDetailsViewModel vm) => vm.Campaign.Description)
                                                 .Row(2),
                                            },
                                        },
                                    },
                                },
                            },
                        },
                    }.Row(1),
                    new Grid()
                    {
                        Style = (Style)Resources["CampaignAvailabilityBannerStyle"],
                        RowDefinitions = Rows.Define(Auto,Auto),
                        Children =
                        {
                            new Label()
                            {
                                Style = (Style)Resources["CampaignAvailabilityDescriptionStyle"],
                            }.Bindv2(static (CampaignDetailsViewModel vm) => vm.Campaign.From, stringFormat: "From {0:MMMM dd, yyyy}")
                             .Row(0),
                            new Label()
                            {
                                Style = (Style)Resources["CampaignAvailabilityDescriptionStyle"],
                            }.Bindv2(static (CampaignDetailsViewModel vm) => vm.Campaign.To, stringFormat: "until {0:MMMM dd, yyyy}")
                             .Row(1),
                        },
                    }.Row(2),
                    new AbsoluteLayout()
                    {
                        Children =
                        {
                            new WebView().AbsLayoutBounds(0, 0, 1, 1)
                             .AbsLayoutFlags(AbsoluteLayoutFlags.All)
                             .Bindv2(static (CampaignDetailsViewModel vm) => vm.Campaign.DetailsUri),
                        },
                    }.Column(0)
                     .Row(0)
                     .RowSpan(3)
                     .Bindv2(AbsoluteLayout.IsVisibleProperty, static (CampaignDetailsViewModel vm) => vm.IsDetailsSite),
                    new ActivityIndicator()
                    {
#if (IOS || ANDROID)
                        WidthRequest = 100d,
#endif
#if WINDOWS
                        WidthRequest = 400d,
#endif
                    }.Row(0)
                     .Center()
                     .Bindv2(static (CampaignDetailsViewModel vm) => vm.IsBusy)
                     .Bindv2(ActivityIndicator.IsVisibleProperty, static (CampaignDetailsViewModel vm) => vm.IsBusy),
                }
            };
        }
    }
}
