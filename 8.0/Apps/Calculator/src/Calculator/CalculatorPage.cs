using Microsoft.Maui.Controls;

namespace Calculator;

public class CalculatorPage : ContentPage
{
    private Label currentCalculation;
    private Label resultText;

    public CalculatorPage()
    {
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
        currentCalculation = new Label
        {
            FontSize = 22,
            LineBreakMode = LineBreakMode.NoWrap,
            TextColor = Colors.LightGray,
            HorizontalTextAlignment = TextAlignment.End,
            VerticalTextAlignment = TextAlignment.Start
            // Set other properties
        };
        grid.Add(currentCalculation, 0, 0);
        Grid.SetColumnSpan(currentCalculation, 4);

        resultText = new Label
        {
            FontSize = 64,
            FontAttributes = FontAttributes.Bold,
            Text = "0",
            HorizontalTextAlignment = TextAlignment.End,
            VerticalTextAlignment = TextAlignment.End,
            TextColor = Colors.LightGray,
            LineBreakMode = LineBreakMode.NoWrap
            // Set other properties
        };
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
        Button buttonC = new Button { Text = "C" };
        buttonC.Clicked += OnClear; 
        grid.Add(buttonC, 0, 1);

        Button buttonPlusMinus = new Button { Text = "+/-" };
        buttonPlusMinus.Clicked += OnNegative; 
        grid.Add(buttonPlusMinus, 1, 1);

        Button buttonPercent = new Button { Text = "%" };
        buttonPercent.Clicked += OnPercentage;
        grid.Add(buttonPercent, 2, 1);

        Button button7 = new Button { Text = "7" };
        button7.Clicked += OnSelectNumber;
        grid.Add(button7, 0, 2);

        Button button8 = new Button { Text = "8" };
        button8.Clicked += OnSelectNumber;
        grid.Add(button8, 1, 2);

        Button button9 = new Button { Text = "9" };
        button9.Clicked += OnSelectNumber;
        grid.Add(button9, 2, 2);

        Button button4 = new Button { Text = "4" };
        button4.Clicked += OnSelectNumber;
        grid.Add(button4, 0, 3);

        Button button5 = new Button { Text = "5" };
        button5.Clicked += OnSelectNumber;
        grid.Add(button5, 1, 3);

        Button button6 = new Button { Text = "6" };
        button6.Clicked += OnSelectNumber;
        grid.Add(button6, 2, 3);

        Button button1 = new Button { Text = "1" };
        button1.Clicked += OnSelectNumber;
        grid.Add(button1, 0, 4);

        Button button2 = new Button { Text = "2" };
        button2.Clicked += OnSelectNumber;
        grid.Add(button2, 1, 4);

        Button button3 = new Button { Text = "3" };
        button3.Clicked += OnSelectNumber;
        grid.Add(button3, 2, 4);

        Button button00 = new Button { Text = "00" };
        button00.Clicked += OnSelectNumber;
        grid.Add(button00, 0, 5);

        Button button0 = new Button { Text = "0" };
        button0.Clicked += OnSelectNumber;
        grid.Add(button0, 1, 5);

        Button buttonDot = new Button { Text = "." };
        buttonDot.Clicked += OnSelectNumber;
        grid.Add(buttonDot, 2, 5);

        Button buttonDiv = new Button { Text = "ÅÄ" };
        buttonDiv.Clicked += OnSelectOperator;
        grid.Add(buttonDiv, 3, 1);

        Button buttonMul = new Button { Text = "Å~" };
        buttonMul.Clicked += OnSelectOperator;
        grid.Add(buttonMul, 3, 2);

        Button buttonMinus = new Button { Text = "-" };
        buttonMinus.Clicked += OnSelectOperator;
        grid.Add(buttonMinus, 3, 3);

        Button buttonPlus = new Button { Text = "+" };
        buttonPlus.Clicked += OnSelectOperator;
        grid.Add(buttonPlus, 3, 4);

        Button buttonCalc = new Button { Text = "=" };
        buttonCalc.Clicked += OnCalculate;
        grid.Add(buttonCalc, 3, 5);

        // Finally, set the content of the page
        this.Content = grid;
    }

    string currentEntry = "";
    int currentState = 1;
    string mathOperator;
    double firstNumber, secondNumber;
    string decimalFormat = "N0";



    void OnSelectNumber(object sender, EventArgs e)
    {

        Button button = (Button)sender;
        string pressed = button.Text;

        currentEntry += pressed;

        if ((this.resultText.Text == "0" && pressed == "0")
            || (currentEntry.Length <= 1 && pressed != "0")
            || currentState < 0)
        {
            this.resultText.Text = "";
            if (currentState < 0)
                currentState *= -1;
        }

        if (pressed == "." && decimalFormat != "N2")
        {
            decimalFormat = "N2";
        }

        this.resultText.Text += pressed;
    }

    void OnSelectOperator(object sender, EventArgs e)
    {
        LockNumberValue(resultText.Text);

        currentState = -2;
        Button button = (Button)sender;
        string pressed = button.Text;
        mathOperator = pressed;
    }

    private void LockNumberValue(string text)
    {
        double number;
        if (double.TryParse(text, out number))
        {
            if (currentState == 1)
            {
                firstNumber = number;
            }
            else
            {
                secondNumber = number;
            }

            currentEntry = string.Empty;
        }
    }

    void OnClear(object sender, EventArgs e)
    {
        firstNumber = 0;
        secondNumber = 0;
        currentState = 1;
        decimalFormat = "N0";
        this.resultText.Text = "0";
        currentEntry = string.Empty;
    }

    void OnCalculate(object sender, EventArgs e)
    {
        if (currentState == 2)
        {
            // if (secondNumber == 0)
                LockNumberValue(resultText.Text);

            double result = Calculator.Calculate(firstNumber, secondNumber, mathOperator);

            this.currentCalculation.Text = $"{firstNumber} {mathOperator} {secondNumber}";

            this.resultText.Text = result.ToTrimmedString(decimalFormat);
            firstNumber = result;
            secondNumber = 0;
            currentState = -1;
            currentEntry = string.Empty;
        }
    }

    void OnNegative(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            secondNumber = -1;
            mathOperator = "Å~";
            currentState = 2;
            OnCalculate(this, null);
        }
    }

    void OnPercentage(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            LockNumberValue(resultText.Text);
            decimalFormat = "N2";
            secondNumber = 0.01;
            mathOperator = "Å~";
            currentState = 2;
            OnCalculate(this, null);
        }
    }

}