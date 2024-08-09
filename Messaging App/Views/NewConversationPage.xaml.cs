namespace Messaging_App.Views;

public partial class NewConversationPage : ContentPage
{
	public NewConversationPage()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled; // Ensure flyout is disabled
    }
}