using Database_Assignment_MAUI.MVVM.ViewModels;

namespace Database_Assignment_MAUI.MVVM.Views;

public partial class ProductView : ContentPage
{
	public ProductView(ProductViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}