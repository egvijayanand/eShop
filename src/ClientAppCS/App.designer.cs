namespace eShop.ClientApp
{
    public partial class App : Application
    {
        private void InitializeComponent()
        {
            Resources.MergedDictionaries.Add(new AppColors());
            Resources.MergedDictionaries.Add(new Styles());
        }
    }
}
