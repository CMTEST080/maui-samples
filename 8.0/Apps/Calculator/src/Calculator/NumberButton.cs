namespace Calculator;

public class NumberButton : ContentView
{
	public NumberButton()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				,TextColor = Colors.White, FontSize = 24, FontAttributes = FontAttributes.Bold
				}
			}
		};
	}
}