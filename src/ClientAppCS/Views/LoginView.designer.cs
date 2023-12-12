using eShop.ClientApp.Animations;
using eShop.ClientApp.Converters;
using eShop.ClientApp.Triggers;
using eShop.ClientApp.ViewModels;
using eShop.ClientApp.ViewModels.Base;
using CommunityToolkit.Maui.Behaviors;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;

namespace eShop.ClientApp.Views
{
    public partial class LoginView : ContentPageBase
    {
        private void InitializeComponent()
        {
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
            Shell.SetNavBarIsVisible(this, false);
            Shell.SetTabBarIsVisible(this, false);
            BackgroundColor = AppColor("LightGrayColor");
            Resources.Add("FirstValidationErrorConverter", new FirstValidationErrorConverter());
            Resources.Add("WebNavigatingEventArgsConverter", new WebNavigatingEventArgsConverter());
            Resources.Add("WebNavigatedEventArgsConverter", new WebNavigatedEventArgsConverter());
            Resources.Add("TitleLabelStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "PlusJakartaSans-Bold" },
                    new() { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("LargeSize") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.MarginProperty, Value = new Thickness(25, 25, 0, 0) },
                },
            });
            Resources.Add("HeaderLabelStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "PlusJakartaSans-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = AppResource<double>("LittleSize") },
                    new() { Property = Label.TextColorProperty, Value = AppColor("TextLightColor") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Start },
                },
            });
            Resources.Add("LoginButtonStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "PlusJakartaSans-Regular" },
                    new() { Property = Label.TextColorProperty, Value = AppColor("WhiteColor") },
                    new() { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                    new() { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.Center },
                },
            });
            Resources.Add("LoginPanelStyle", new Style(typeof(Grid))
            {
                Setters =
                {
                    new() { Property = Grid.HeightRequestProperty, Value = 60 },
                    new() { Property = Grid.BackgroundColorProperty, Value = AppColor("LightGreenColor") },
                    new() { Property = Grid.HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand },
                    new() { Property = Grid.VerticalOptionsProperty, Value = LayoutOptions.FillAndExpand },
                },
            });
            Resources.Add("RegisterPanelStyle", new Style(typeof(Grid))
            {
                BasedOn = (Style)Resources["LoginPanelStyle"],
                Setters =
                {
                    new() { Property = Grid.BackgroundColorProperty, Value = AppResource<Color>("GreenColor") },
                },
            });
            Resources.Add("SettingsPanelStyle", new Style(typeof(Grid))
            {
                BasedOn = (Style)Resources["LoginPanelStyle"],
                Setters =
                {
                    new() { Property = Grid.BackgroundColorProperty, Value = AppResource<Color>("BlackColor") },
                },
            });
            Resources.Add("SettingsImageStyle", new Style(typeof(Image))
            {
                Setters =
                {
                    new() { Property = Image.MarginProperty, Value = new Thickness(12) },
                },
            });
            Resources.Add("LoginAnimation", new StoryBoard()
            {
                Target = LoginPanel,
                Animations =
                {
                    new FadeInAnimation()
                    {
                        Direction = FadeInAnimation.FadeDirection.Up,
                        Duration = "1500",
                    },
                }
            });
#if (IOS || WINDOWS)
            Title = "Northern Mountains";
#endif
            Triggers.Add(new EventTrigger()
            {
                Event = "Appearing",
                Actions =
                {
                    new BeginAnimation()
                    {
                        Animation = (StoryBoard)Resources["LoginAnimation"],
                    },
                },
            });
            Content = new Grid()
            {
                Children =
                {
                    new Image()
                    {
                        Source = "header.png",
                        Aspect = Aspect.AspectFill,
                    }.Height(400)
                     .CenterHorizontal()
                     .Top(),
                    new Grid()
                    {
                        ColumnSpacing = 0,
                        RowSpacing = 0,
                        RowDefinitions = Rows.Define(Auto, Auto, Auto, Star, 60),
                        Children =
                        {
                            new Grid()
                            {
                                RowDefinitions = Rows.Define(66),
                                ColumnDefinitions = Columns.Define(Star, Star),
                                Children =
                                {
                                    new HorizontalStackLayout()
                                    {
                                        Children =
                                        {
                                            new Image()
                                            {
                                                Source = "logo_header.png",
                                                Aspect = Aspect.AspectFill,
                                            }.Height(250),
                                        },
                                    }.Column(0)
                                     .Start(),
                                    new HorizontalStackLayout()
                                    {
                                        GestureRecognizers =
                                        {
                                            new TapGestureRecognizer()
                                            {
                                                NumberOfTapsRequired = 1,
                                            }.BindCommand(static (LoginViewModel vm) => vm.SettingsCommand),
                                        },
                                        Children =
                                        {
                                            new Label()
                                            {
                                                Text = "SETTINGS",
                                            },
                                        },
                                    }.Column(2)
                                     .CenterHorizontal(),
                                },
                            }.Row(0)
                             .Margin(48, 24),
                            new Frame()
                            {
                                HasShadow = false,
                                CornerRadius = 10,
                                BackgroundColor = White,
                                MaximumWidthRequest = 600,
                                Content = new StackLayout()
                                {
                                    Children =
                                    {
                                        new Label()
                                        {
                                            Text = "Login",
                                            Style = (Style)Resources["TitleLabelStyle"],
                                        }.Row(1)
                                         .Start(),
                                        new StackLayout()
                                        {
                                            Children =
                                            {
                                                new Label()
                                                {
                                                    Text = "Username or email",
                                                    Style = (Style)Resources["HeaderLabelStyle"],
                                                },
                                                new Entry()
                                                {
#if (IOS || ANDROID)
                                                    Style = AppResource<Style>("EntryStyle"),
#endif
#if WINDOWS
                                                    Style = AppResource<Style>("WinUIEntryStyle"),
#endif
                                                    Behaviors =
                                                    {
                                                        new EventToCommandBehavior()
                                                        {
                                                            EventName = nameof(Entry.TextChanged),
                                                        }.BindCommand(static (LoginViewModel vm) => vm.ValidateCommand),
                                                    },
                                                    Triggers =
                                                    {
                                                        new DataTrigger(typeof(Entry))
                                                        {
                                                            Binding = new Binding(PropertyName(static (LoginViewModel vm) => vm.UserName.IsValid)),
                                                            Value = false,
                                                            Setters =
                                                            {
                                                                new Setter()
                                                                {
                                                                    Property = Entry.BackgroundColorProperty,
                                                                    Value = AppColor("ErrorColor"),
                                                                },
                                                            },
                                                        },
                                                    },
                                                }.Bindv2(static (LoginViewModel vm) => vm.UserName.Value, BindingMode.TwoWay),
                                                new Label()
                                                {
                                                    Style = AppStyle("ValidationErrorLabelStyle"),
                                                }.Bindv2(static (LoginViewModel vm) => vm.UserName.Errors, converter: (IValueConverter)Resources["FirstValidationErrorConverter"]),
                                                new Label()
                                                {
                                                    Text = "Password",
                                                    Style = (Style)Resources["HeaderLabelStyle"],
                                                },
                                                new Entry()
                                                {
                                                    IsPassword = true,
#if (IOS || ANDROID)
                                                    Style = AppResource<Style>("EntryStyle"),
#endif
#if WINDOWS
                                                    Style = AppResource<Style>("WinUIEntryStyle"),
#endif
                                                    Behaviors =
                                                    {
                                                        new EventToCommandBehavior()
                                                        {
                                                            EventName = nameof(Entry.TextChanged),
                                                        }.BindCommand(static (LoginViewModel vm) => vm.ValidateCommand),
                                                    },
                                                    Triggers =
                                                    {
                                                        new DataTrigger(typeof(Entry))
                                                        {
                                                            Binding = new Binding(PropertyName(static (LoginViewModel vm) => vm.Password.IsValid)),
                                                            Value = false,
                                                            Setters =
                                                            {
                                                                new Setter()
                                                                {
                                                                    Property = Entry.BackgroundColorProperty,
                                                                    Value = AppColor("ErrorColor"),
                                                                },
                                                            },
                                                        },
                                                    },
                                                }.Bindv2(static (LoginViewModel vm) => vm.Password.Value, BindingMode.TwoWay),
                                                new Label()
                                                {
                                                    Style = AppStyle("ValidationErrorLabelStyle"),
                                                }.Bindv2(static (LoginViewModel vm) => vm.Password.Errors, converter: (IValueConverter)Resources["FirstValidationErrorConverter"]),
                                            },
                                        }.Row(3)
                                         .Margin(24),
                                        new Grid()
                                        {
                                            BackgroundColor = AppColor("BlackColor"),
                                            GestureRecognizers =
                                            {
                                                new TapGestureRecognizer()
                                                {
                                                    NumberOfTapsRequired = 1,
                                                }.BindCommand(static (LoginViewModel vm) => vm.MockSignInCommand),
                                            },
                                            Children =
                                            {
                                                new Label()
                                                {
                                                    Text = "Login",
                                                    Style = (Style)Resources["LoginButtonStyle"],
                                                },
                                            },
                                        }.Padding(10)
                                         .Margins(25,0,25,0),
                                    },
                                },
                            }.Row(1)
                             .Margin(25,100),
                        },
                    }.Padding(0)
                     .Bindv2(Grid.IsVisibleProperty, static (LoginViewModel vm) => vm.IsMock)
                     .Assign(out LoginPanel),
                    new Grid()
                    {
                        ColumnSpacing = 0,
                        RowSpacing = 0,
                        RowDefinitions = Rows.Define(Star, 60),
                        ColumnDefinitions = Columns.Define(Star, Star, 64),
                        Children =
                        {
                            new Image()
                            {
                                Aspect = Aspect.AspectFill,
                                Source = "banner.png",
                            }.Row(0)
                             .Column(0)
                             .ColumnSpan(3)
                             .Assign(out Banner),
                            new Grid()
                            {
                                BackgroundColor = AppColor("BlackColor"),
                                Opacity = 0.5f,
                            }.Row(0)
                             .Column(0)
                             .ColumnSpan(3),
                            new Grid()
                            {
                                Style = (Style)Resources["LoginPanelStyle"],
                                GestureRecognizers =
                                {
                                    new TapGestureRecognizer()
                                    {
                                        NumberOfTapsRequired = 1,
                                    }.BindCommand(static (LoginViewModel vm) => vm.SignInCommand),
                                },
                                Children =
                                {
                                    new Label()
                                    {
                                        Text = "[ LOGIN ]",
                                        Style = (Style)Resources["LoginButtonStyle"],
                                    },
                                },
                            }.Column(0)
                             .Row(1),
                            new Grid()
                            {
                                Style = (Style)Resources["RegisterPanelStyle"],
                                GestureRecognizers =
                                {
                                    new TapGestureRecognizer()
                                    {
                                        NumberOfTapsRequired = 1,
                                    }.BindCommand(static (LoginViewModel vm) => vm.RegisterCommand),
                                },
                                Children =
                                {
                                    new Label()
                                    {
                                        Text = "[ REGISTER ]",
                                        Style = (Style)Resources["LoginButtonStyle"],
                                    },
                                },
                            }.Column(1)
                             .Row(1),
                            new Grid()
                            {
                                Style = (Style)Resources["SettingsPanelStyle"],
                                GestureRecognizers =
                                {
                                    new TapGestureRecognizer()
                                    {
                                        NumberOfTapsRequired = 1,
                                    }.BindCommand(static (LoginViewModel vm) => vm.SettingsCommand),
                                },
                                Children =
                                {
                                    new Image()
                                    {
                                        Style = (Style)Resources["SettingsImageStyle"],
                                        Source = AppResource<FontImageSource>("SettingsIconLightImageSource"),
                                    },
                                },
                            }.Column(2)
                             .Row(1),
                            new WebView()
                            {
                                Behaviors =
                                {
                                    new EventToCommandBehavior()
                                    {
                                        EventName = nameof(WebView.Navigating),
                                        EventArgsConverter = (WebNavigatingEventArgsConverter)Resources["WebNavigatingEventArgsConverter"],
                                    }.BindCommand(static (LoginViewModel vm) => vm.NavigateCommand),
                                },
                            }.Column(0)
                             .ColumnSpan(3)
                             .Row(0)
                             .RowSpan(2)
                             .Bindv2(static (LoginViewModel vm) => vm.LoginUrl)
                             .Bindv2(WebView.IsVisibleProperty, static (LoginViewModel vm) => vm.IsLogin),
                        },
                    }.Bindv2(Grid.IsVisibleProperty, static (LoginViewModel vm) => vm.IsMock, converter: (IValueConverter)AppResource("InverseBoolConverter")),
                    new ActivityIndicator()
                    {
                        Color = AppColor("BlackColor"),
                    }.Center()
                     .Bindv2(static (LoginViewModel vm) => vm.IsBusy)
                     .Bindv2(ActivityIndicator.IsVisibleProperty, static (LoginViewModel vm) => vm.IsBusy),
                }
            };
        }

        #region Variables
        private Grid LoginPanel;
        private Image Banner;
        #endregion
    }
}
