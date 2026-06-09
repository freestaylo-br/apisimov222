using AnisimovApp.Views;

namespace AnisimovApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(
                nameof(AddEditProductPage),
                typeof(AddEditProductPage));
        }
    }
}
