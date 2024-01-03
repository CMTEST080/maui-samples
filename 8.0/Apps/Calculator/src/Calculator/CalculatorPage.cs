using Microsoft.Maui.Controls;

namespace Calculator;

public class CalculatorPage : ContentPage
{
    public CalculatorPage(CalculatorViewModel vm)
    {
        BindingContext = vm;
        NavigationPage.SetHasNavigationBar(this, false);

        // Create grid
        Grid grid = new Grid
        {
            Padding = new Thickness(16),
            RowSpacing = 2,
            ColumnSpacing = 2,
            RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                },
            ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                }
        };

        // Create labels
        var currentCalculation = new Label
        {
            FontSize = 22,
            LineBreakMode = LineBreakMode.NoWrap,
            TextColor = Colors.LightGray,
            HorizontalTextAlignment = TextAlignment.End,
            VerticalTextAlignment = TextAlignment.Start,
        };
        currentCalculation.SetBinding(Label.TextProperty, "CurrentCalculation");
        grid.Add(currentCalculation, 0, 0);
        Grid.SetColumnSpan(currentCalculation, 4);

        var resultText = new Label
        {
            FontSize = 64,
            FontAttributes = FontAttributes.Bold,
            HorizontalTextAlignment = TextAlignment.End,
            VerticalTextAlignment = TextAlignment.End,
            TextColor = Colors.LightGray,
            LineBreakMode = LineBreakMode.NoWrap
            // Set other properties
        };
        resultText.SetBinding(Label.TextProperty, "ResultText");

        grid.Add(resultText, 0, 0);
        Grid.SetColumnSpan(resultText, 4);

        // Add BoxView
        BoxView boxView = new BoxView
        {
            BackgroundColor = Colors.LightGreen,
            HeightRequest = 1,
            VerticalOptions = LayoutOptions.End
        };
        grid.Add(boxView, 0, 0);
        Grid.SetColumnSpan(boxView, 4);

        // Add buttons
        Button buttonC = new Button { 
            Text = "C", 
            Command = vm.ClearCommand,
            CommandParameter = "C"
        };
        // buttonC.CommandParameter = buttonC;

        // Bind the ReturnCommand to a command in your view model
        // addItemEntry.SetBinding(Entry.ReturnCommandProperty, "AddItemCommand");
        // addItemEntry.SetBinding(Entry.ReturnCommandParameterProperty, new Binding { Source = addItemEntry });

        // buttonC.SetBinding(Button.CommandProperty, new Binding("ClearCommand", source: BindingContext));
        // buttonC.SetBinding(Button.CommandParameterProperty, new Binding("."));
        grid.Add(buttonC, 0, 1);

        Button buttonPlusMinus = new Button { Text = "+/-", Command = vm.NegativeCommand, CommandParameter = "+/-" };
        // buttonPlusMinus.CommandParameter = buttonPlusMinus;
        // buttonPlusMinus.SetBinding(Button.CommandProperty, new Binding("NegativeCommand", source: BindingContext));
        // buttonPlusMinus.SetBinding(Button.CommandParameterProperty, new Binding("."));
        grid.Add(buttonPlusMinus, 1, 1);

        Button buttonPercent = new Button { Text = "%", Command = vm.PercentageCommand, CommandParameter = "%" };
        // buttonPercent.CommandParameter = buttonPercent;
        // buttonPlusMinus.SetBinding(Button.CommandProperty, new Binding("PercentageCommand", source: BindingContext));
        // buttonPlusMinus.SetBinding(Button.CommandParameterProperty, new Binding("."));
        grid.Add(buttonPercent, 2, 1);

        Button button7 = new Button { Text = "7", Command = vm.SelectNumberCommand, CommandParameter = "7" };
        // button7.CommandParameter = button7;
        // button7.Clicked += OnSelectNumber;
        grid.Add(button7, 0, 2);

        Button button8 = new Button { Text = "8", Command = vm.SelectNumberCommand, CommandParameter = "8" };
        // button8.CommandParameter = button8;
        // button8.Clicked += OnSelectNumber;
        grid.Add(button8, 1, 2);

        Button button9 = new Button { Text = "9", Command = vm.SelectNumberCommand, CommandParameter = "9" };
        // button9.CommandParameter = button9;
        // button9.Clicked += OnSelectNumber;
        grid.Add(button9, 2, 2);

        Button button4 = new Button { Text = "4", Command = vm.SelectNumberCommand, CommandParameter = "4" };
        // button4.CommandParameter = button4;
        // button4.Clicked += OnSelectNumber;
        grid.Add(button4, 0, 3);

        Button button5 = new Button { Text = "5", Command = vm.SelectNumberCommand, CommandParameter = "5" };
        // button5.CommandParameter = button5;
        // button5.Clicked += OnSelectNumber;
        grid.Add(button5, 1, 3);

        Button button6 = new Button { Text = "6", Command = vm.SelectNumberCommand, CommandParameter = "6" };
        // button6.CommandParameter = button6;
        // button6.Clicked += OnSelectNumber;
        grid.Add(button6, 2, 3);

        Button button1 = new Button { Text = "1", Command = vm.SelectNumberCommand, CommandParameter = "1" };
        // button1.CommandParameter = button1;
        //button1.Clicked += OnSelectNumber;
        grid.Add(button1, 0, 4);

        Button button2 = new Button { Text = "2", Command = vm.SelectNumberCommand, CommandParameter = "2" };
        // button2.CommandParameter = button2;
        // button2.Clicked += OnSelectNumber;
        grid.Add(button2, 1, 4);

        Button button3 = new Button { Text = "3", Command = vm.SelectNumberCommand, CommandParameter = "3" };
        // button3.CommandParameter = button3;
        // button3.Clicked += OnSelectNumber;
        grid.Add(button3, 2, 4);

        Button button00 = new Button { Text = "00", Command = vm.SelectNumberCommand, CommandParameter = "00" };
        // button00.CommandParameter = button00;
        // button00.Clicked += OnSelectNumber;
        grid.Add(button00, 0, 5);

        Button button0 = new Button { Text = "0", Command = vm.SelectNumberCommand, CommandParameter = "0" };
        // button0.CommandParameter = button0;
        // button0.Clicked += OnSelectNumber;
        grid.Add(button0, 1, 5);

        Button buttonDot = new Button { Text = ".", Command = vm.SelectNumberCommand, CommandParameter = "." };
        // buttonDot.CommandParameter = buttonDot;
        // buttonDot.Clicked += OnSelectNumber;
        grid.Add(buttonDot, 2, 5);

        Button buttonDiv = new Button { Text = "ÅÄ", Command = vm.SelectOperatorCommand, CommandParameter = "ÅÄ" };
        // buttonDiv.CommandParameter = buttonDiv;
        // buttonDiv.Clicked += OnSelectOperator;
        grid.Add(buttonDiv, 3, 1);

        Button buttonMul = new Button { Text = "Å~", Command = vm.SelectOperatorCommand, CommandParameter = "Å~" };
        // buttonMul.CommandParameter = buttonMul;
        // buttonMul.Clicked += OnSelectOperator;
        grid.Add(buttonMul, 3, 2);

        Button buttonMinus = new Button { Text = "-", Command = vm.SelectOperatorCommand, CommandParameter = "-" };
        // buttonMinus.CommandParameter = buttonMinus;
        // buttonMinus.Clicked += OnSelectOperator;
        grid.Add(buttonMinus, 3, 3);

        Button buttonPlus = new Button { Text = "+", Command = vm.SelectOperatorCommand, CommandParameter = "+" };
        // buttonPlus.CommandParameter = buttonPlus;
        // buttonPlus.Clicked += OnSelectOperator;
        grid.Add(buttonPlus, 3, 4);

        Button buttonCalc = new Button { Text = "=", Command = vm.CalculateCommand, CommandParameter = "=" };
        // buttonCalc.CommandParameter = buttonCalc;
        // buttonCalc.Clicked += OnCalculate;
        grid.Add(buttonCalc, 3, 5);

        //NumberButton numberButton = new NumberButton
        //{
        //   //
        //};
        //grid.Add(numberButton, 3, 5);

        // Finally, set the content of the page
        this.Content = grid;
    }

}