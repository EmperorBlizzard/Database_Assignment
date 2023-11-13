using Database_Assignment_MAUI.MVVM.ViewModels;

namespace Database_Assignment_MAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}