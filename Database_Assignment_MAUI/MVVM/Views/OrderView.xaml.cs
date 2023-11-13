using Database_Assignment_MAUI.MVVM.ViewModels;

namespace Database_Assignment_MAUI.MVVM.Views;

public partial class OrderView : ContentPage
{
	public OrderView(OrderViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}