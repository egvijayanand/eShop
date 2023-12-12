using eShop.ClientApp.Converters;
using ios = Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using CommunityToolkit.Maui.Converters;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;

namespace eShop
{
    public partial class Styles : ResourceDictionary
    {
        public Styles()
        {
            Add("BaseButtonBorderRadius", 6d);
            Add("BaseButtonBorderWidth", 0d);
            Add("SettingsIcon", "\xF013");
            Add("CatalogIcon", "\xF03A");
            Add("MapIcon", "\xF279");
            Add("ProfileIcon", "\xF2BD");
            Add("BasketIcon", "\xF07A");
            Add("CampaignIcon", "\xF155");
            Add("FilterIcon", "\xF0B0");
            Add("AddIcon", "\xF067");
            Add("CircleIcon", "\xF111");
            Add("ToggleOffIcon", "\xF204");
            Add("ToggleOnIcon", "\xF205");

            Add("SettingsIconImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["SettingsIcon"],
            });
            Add("SettingsIconLightImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["SettingsIcon"],
                Color = AppColor("WhiteColor"),
            });
            Add("CatalogIconImageSource", new FontImageSource()
            {
                FontAutoScalingEnabled = true,
                Size = 22,
                Color = Application.Current?.RequestedTheme switch
                {
                    AppTheme.Dark => AppColor("DarkFontColor"),
                    AppTheme.Light or _ => AppColor("LightFontColor"),
                },
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["CatalogIcon"],
            });
            Add("MapIconImageSource", new FontImageSource()
            {
                FontAutoScalingEnabled = true,
                Size = 22,
                Color = Application.Current?.RequestedTheme switch
                {
                    AppTheme.Dark => AppColor("DarkFontColor"),
                    AppTheme.Light or _ => AppColor("LightFontColor"),
                },
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["MapIcon"],
            });
            Add("ProfileIconImageSource", new FontImageSource()
            {
                FontAutoScalingEnabled = true,
                Size = 22,
                Color = Application.Current?.RequestedTheme switch
                {
                    AppTheme.Dark => AppColor("DarkFontColor"),
                    AppTheme.Light or _ => AppColor("LightFontColor"),
                },
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["ProfileIcon"],
            });
            Add("BasketIconImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["BasketIcon"],
            });
            Add("BasketIconForTitleImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["BasketIcon"],
                Size = 24,
                Color = Application.Current?.RequestedTheme switch
                {
                    AppTheme.Dark => AppColor("DarkFontColor"),
                    AppTheme.Light or _ => AppColor("LightFontColor"),
                },
            });
            Add("CampaignIconImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["CampaignIcon"],
            });
            Add("FilterIconImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["FilterIcon"],
            });
            Add("FilterHighlightIconImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["FilterIcon"],
                Color = AppColor("AccentColor"),
            });
            Add("FilterIconForTitleImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["FilterIcon"],
                Size = 22,
                Color = Application.Current?.RequestedTheme switch
                {
                    AppTheme.Dark => AppColor("DarkFontColor"),
                    AppTheme.Light or _ => AppColor("LightFontColor"),
                },
            });
            Add("SettingsIconForTitleImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["SettingsIcon"],
                Size = 22,
                Color = Application.Current?.RequestedTheme switch
                {
                    AppTheme.Dark => AppColor("DarkFontColor"),
                    AppTheme.Light or _ => AppColor("LightFontColor"),
                },
            });
            Add("AddIconImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["AddIcon"],
            });
            Add("AddIconLightImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["AddIcon"],
                Color = AppColor("WhiteColor"),
            });
            Add("CircleIconImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["CircleIcon"],
            });
            Add("ToggleOffImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["ToggleOffIcon"],
                Color = AppColor("DisabledColor"),
            });
            Add("ToggleOnImageSource", new FontImageSource()
            {
                FontFamily = "FontAwesome-Solid",
                Glyph = (string)this["ToggleOnIcon"],
                Color = AppColor("AccentColor"),
            });

            Add("BaseButtonFontSize", DeviceInfo.Platform.ToString() switch
            {
                nameof(DevicePlatform.iOS) => 18d,
                _ => 16d,
            });
            Add("BaseFontSize", DeviceInfo.Platform.ToString() switch
            {
                nameof(DevicePlatform.iOS) => 16d,
                _ => 15d,
            });
            Add("LittleSize", DeviceInfo.Platform.ToString() switch
            {
                nameof(DevicePlatform.iOS) => 11d,
                _ => 12d,
            });
            Add("MidMediumSize", DeviceInfo.Platform.ToString() switch
            {
                nameof(DevicePlatform.iOS) => 12d,
                _ => 14d,
            });
            Add("MediumSize", DeviceInfo.Platform.ToString() switch
            {
                nameof(DevicePlatform.iOS) => 14d,
                _ => 16d,
            });
            Add("LargeSize", DeviceInfo.Platform.ToString() switch
            {
                nameof(DevicePlatform.iOS) => 16d,
                _ => 18d,
            });
            Add("LargerSize", DeviceInfo.Platform.ToString() switch
            {
                nameof(DevicePlatform.iOS) => 18d,
                _ => 20d,
            });
            Add("BigSize", DeviceInfo.Platform.ToString() switch
            {
                nameof(DevicePlatform.iOS) => 20d,
                _ => 24d,
            });
            Add("ExtraBigSize", DeviceInfo.Platform.ToString() switch
            {
                nameof(DevicePlatform.iOS) => 24d,
                _ => 32d,
            });
            Add("HugeSize", DeviceInfo.Platform.ToString() switch
            {
                nameof(DevicePlatform.iOS) => 32d,
                _ => 48d,
            });
            Add("BaseButtonFontAttributes", DeviceInfo.Platform.ToString() switch
            {
                nameof(DevicePlatform.iOS) => FontAttributes.Bold,
                _ => FontAttributes.None,
            });

            Add("CountToBoolConverter", new IntToBoolConverter());
            Add("InverseBoolConverter", new InvertedBoolConverter());
            Add("ToUpperConverter", new TextCaseConverter() { Type = TextCaseType.Upper });
            Add("StringNullOrEmptyBoolConverter", new IsStringNullOrEmptyConverter());
            Add("ItemTappedEventArgsConverter", new ItemTappedEventArgsConverter());
            Add("ListIsNullOrEmptyConverter", new IsListNullOrEmptyConverter());
            Add("ListIsNotNullOrEmptyConverter", new IsListNotNullOrEmptyConverter());
            Add("DoesNotHaveCountConverter", new DoesNotHaveCountConverter());
            Add("HasCountConverter", new HasCountConverter());
            Add("ItemsToHeightConverter", new ItemsToHeightConverter());
            Add("OrderStatusToStringConverter", new OrderStatusToStringConverter());
            Add("DateTimeFormat", "{0:MMMM dd yyyy}");

            Add("ValidationErrorLabelStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.TextColorProperty, Value = AppColor("ErrorColor") },
                    new() { Property = Label.FontSizeProperty, Value = (double)this["LittleSize"] },
                },
            });
            Add("EntryStyle", new Style(typeof(Entry))
            {
                Triggers =
                {
                    new Trigger(typeof(Entry))
                    {
                        Property = Entry.IsFocusedProperty,
                        Value = true,
                        Setters =
                        {
                            new Setter()
                            {
                                Property = Entry.OpacityProperty,
                                Value = 1,
                            },
                        },
                    },
                },
                Setters =
                {
                    new() { Property = Entry.FontFamilyProperty, Value = "PlusJakartaSans-Regular" },
                    new() { Property = Entry.FontSizeProperty, Value = (double)this["LargeSize"] },
                    new() { Property = Entry.HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand },
                    new() { Property = Entry.FontAttributesProperty, Value = FontAttributes.Bold },
                    new() { Property = Entry.OpacityProperty, Value = 0.6 },
                },
            });
            Add("MediumSizeFontStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "PlusJakartaSans-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = (double)this["MediumSize"] },
                },
            });
            Add("LittleSizeFontStyle", new Style(typeof(Label))
            {
                Setters =
                {
                    new() { Property = Label.FontFamilyProperty, Value = "PlusJakartaSans-Regular" },
                    new() { Property = Label.FontSizeProperty, Value = (double)this["LittleSize"] },
                },
            });
            Add("WinUIEntryStyle", new Style(typeof(Entry))
            {
                Triggers =
                {
                    new Trigger(typeof(Entry))
                    {
                        Property = Entry.IsFocusedProperty,
                        Value = true,
                        Setters =
                        {
                            new Setter()
                            {
                                Property = Entry.OpacityProperty,
                                Value = 1,
                            },
                        },
                    },
                },
                Setters =
                {
                    new() { Property = Entry.FontFamilyProperty, Value = "PlusJakartaSans-Regular" },
                    new() { Property = Entry.TextColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => White, AppTheme.Light or _ => AppResource<Color>("BlackColor") } },
                    new() { Property = Entry.PlaceholderColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => White, AppTheme.Light or _ => AppResource<Color>("BlackColor") } },
                    new() { Property = Entry.FontSizeProperty, Value = (double)this["LargeSize"] },
                    new() { Property = Entry.HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand },
                    new() { Property = Entry.FontAttributesProperty, Value = FontAttributes.Bold },
                    new() { Property = Entry.BackgroundColorProperty, Value = Transparent },
                    new() { Property = Entry.OpacityProperty, Value = 0.6 },
                },
            });

            Add(new Style(typeof(Button))
            {
                ApplyToDerivedTypes = true,
                CanCascade = true,
                Setters =
                {
                    new() { Property = Button.FontSizeProperty, Value = (double)this["BaseButtonFontSize"] },
                    new() { Property = Button.FontAttributesProperty, Value = (FontAttributes)this["BaseButtonFontAttributes"] },
                    new() { Property = Button.CornerRadiusProperty, Value = (double)this["BaseButtonBorderRadius"] },
                    new() { Property = Button.BorderWidthProperty, Value = (double)this["BaseButtonBorderWidth"] },
                    new() { Property = Button.BackgroundColorProperty, Value = AppColor("DefaultButtonClassBackgroundColor") },
                    new() { Property = Button.BorderColorProperty, Value = AppColor("DefaultButtonClassBorderColor") },
                    new() { Property = Button.TextColorProperty, Value = AppColor("DefaultButtonClassTextColor") },
                },
            });
            Add(new Style(typeof(Label))
            {
                ApplyToDerivedTypes = true,
                CanCascade = true,
                Setters =
                {
                    new() { Property = Label.FontSizeProperty, Value = (double)this["BaseFontSize"] },
                    new() { Property = Label.TextColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => AppColor("DarkFontColor"), AppTheme.Light or _ => AppColor("LightFontColor") } },
                },
            });
            Add(new Style(typeof(Entry))
            {
                ApplyToDerivedTypes = true,
                CanCascade = true,
                Setters =
                {
                    new() { Property = Entry.TextColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => AppColor("DarkFontColor"), AppTheme.Light or _ => AppColor("LightFontColor") } },
                    new() { Property = Entry.PlaceholderColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => AppColor("DarkFontColor"), AppTheme.Light or _ => AppColor("LightFontColor") } },
                    new() { Property = Entry.BackgroundColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => AppColor("DarkBackgroundColor"), AppTheme.Light or _ => AppColor("LightBackgroundColor") } },
                },
            });
            Add(new Style(typeof(ImageCell))
            {
                ApplyToDerivedTypes = true,
                CanCascade = true,
                Setters =
                {
                    new() { Property = ImageCell.TextColorProperty, Value = AppColor("InverseTextColor") },
                    new() { Property = ImageCell.DetailColorProperty, Value = AppColor("AccentColor") },
                },
            });
            Add(new Style(typeof(TextCell))
            {
                ApplyToDerivedTypes = true,
                CanCascade = true,
                Setters =
                {
                    new() { Property = TextCell.TextColorProperty, Value = AppColor("InverseTextColor") },
                    new() { Property = TextCell.DetailColorProperty, Value = AppColor("AccentColor") },
                },
            });
            Add(new Style(typeof(CollectionView))
            {
                ApplyToDerivedTypes = true,
                CanCascade = true,
                Setters =
                {
                    new() { Property = CollectionView.BackgroundColorProperty, Value = AppColor("ThemeListViewBackgroundColor") },
                },
            });
            Add(new Style(typeof(ActivityIndicator))
            {
                ApplyToDerivedTypes = true,
                CanCascade = true,
                Setters =
                {
                    new() { Property = ActivityIndicator.ColorProperty, Value = AppColor("ActivityIndicatorColor") },
                },
            });
            Add(new Style(typeof(TabBar))
            {
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new() { Property = Shell.TabBarTitleColorProperty, Value = AppColor("LightGreenColor") },
#if WINDOWS
                    new Setter()
                    {
                        Property = Shell.TabBarForegroundColorProperty,
                        Value = AppColor("LightGreenColor"),
                    },
                    new Setter()
                    {
                        Property = Shell.TabBarUnselectedColorProperty,
                        Value = Gray,
                    },
#endif
                },
            });
            Add(new Style(typeof(ContentPage))
            {
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new() { Property = NavigationPage.BackButtonTitleProperty, Value = "" },
                    new() { Property = ios.Page.UseSafeAreaProperty, Value = true },
                    new() { Property = ContentPage.BackgroundColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => AppColor("DarkBackgroundColor"), AppTheme.Light or _ => AppColor("LightBackgroundColor") } },
                    new() { Property = Shell.ForegroundColorProperty, Value = Application.Current?.RequestedTheme switch { AppTheme.Dark => AppColor("DarkForegroundColor"), AppTheme.Light or _ => AppColor("LightForegroundColor") } },
                },
            });
        }
    }
}
