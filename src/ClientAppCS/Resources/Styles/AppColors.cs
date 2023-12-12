using Microsoft.Maui.Graphics;

namespace eShop
{
    public partial class AppColors : ResourceDictionary
    {
        public AppColors()
        {
            Add("WhiteColor", Color.FromArgb("#ffffff"));
            Add("BlackColor", Color.FromArgb("#000000"));
            Add("AccentColor", Color.FromArgb("#00857D"));
            Add("LightGreenColor", Color.FromArgb("#83D01B"));
            Add("GreenColor", Color.FromArgb("#00A69C"));
            Add("DarkGreenColor", Color.FromArgb("#00857D"));
            Add("GrayColor", Color.FromArgb("#e2e2e2"));
            Add("DisabledColor", Gray);
            Add("ErrorColor", Color.FromArgb("#ff5252"));
            Add("TextColor", Color.FromArgb("#000"));
            Add("TextLightColor", Color.FromArgb("#444"));
            Add("InverseTextColor", Color.FromArgb("#FFFFFF"));
            Add("LightTextColor", Color.FromArgb("#979797"));
            Add("iOSDefaultTintColor", Color.FromArgb("#007aff"));
            Add("SeparatorLineColor", Color.FromArgb("#CCCCCC"));
            Add("DefaultButtonClassBackgroundColor", Color.FromArgb("#C9C9C9"));
            Add("DefaultButtonClassBorderColor", Transparent);
            Add("DefaultButtonClassTextColor", Color.FromArgb("#FFFFFF"));
            Add("EntryBackgroundColor", Transparent);
            Add("DefaultAccentColorColor", Color.FromArgb("#1FAECE"));
            Add("ListViewBackgroundColor", Transparent);
            Add("ThemeListViewBackgroundColor", Transparent);
            Add("ActivityIndicatorColor", Color.FromArgb("#00857D"));

            Add("LightBackgroundColor", Color.FromArgb("#FFFFFF"));
            Add("DarkBackgroundColor", Color.FromArgb("#222222"));
            Add("LightForegroundColor", Color.FromArgb("#222222"));
            Add("DarkForegroundColor", Color.FromArgb("#FFFFFF"));
            Add("LightFontColor", Color.FromArgb("#222222"));
            Add("DarkFontColor", Color.FromArgb("#E7E7E7"));
            Add("LightSeparatorColor", Color.FromArgb("#222222"));
            Add("DarkSeparatorColor", Color.FromArgb("#E7E7E7"));
            Add("LightGrayColor", Color.FromArgb("#eeeeee"));
        }
    }
}
