using Database_Assignment_MAUI.MVVM.ViewModels;

namespace Database_Assignment_MAUI.MVVM.Views;

public partial class CustomerView : ContentPage
{
	public CustomerView(CustomerViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}