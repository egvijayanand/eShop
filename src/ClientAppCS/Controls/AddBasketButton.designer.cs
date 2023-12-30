using eShop.ClientApp.Effects;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;

namespace eShop.ClientApp.Controls
{
    public partial class AddBasketButton : Grid
    {
        private void InitializeComponent()
        {
            Children.Add(new Ellipse()
            {
                Fill = Color.FromArgb("#000"),
                StrokeThickness = 0,
            }.Height(48)
             .Width(48)
             .Center());
            Children.Add(new Image()
            {
                Aspect = Aspect.AspectFit,
                Source = AppResource<FontImageSource>("AddIconLightImageSource"),
            }.Height(24)
             .Width(24)
             .Center());
        }
    }
}
