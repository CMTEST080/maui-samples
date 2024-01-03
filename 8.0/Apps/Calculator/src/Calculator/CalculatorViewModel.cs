using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Calculator;

public partial class CalculatorViewModel : ObservableObject
{
    [ObservableProperty]
    private string resultText = "0";

    [ObservableProperty]
    private string currentCalculation;

    public CalculatorViewModel()
    {

    }

    private string currentEntry = "";
    private int currentState = 1;
    private string mathOperator;
    private double firstNumber, secondNumber;
    private string decimalFormat = "N0";

    [RelayCommand]
    void SelectNumber(object sender)
    {

        Button button = (Button)sender;
        string pressed = button.Text;

        currentEntry += pressed;

        if ((ResultText == "0" && pressed == "0")
            || (currentEntry.Length <= 1 && pressed != "0")
            || currentState < 0)
        {
            ResultText = "";
            if (currentState < 0)
                currentState *= -1;
        }

        if (pressed == "." && decimalFormat != "N2")
        {
            decimalFormat = "N2";
        }

        ResultText += pressed;
    }

    [RelayCommand]
    void OnSelectOperator(object sender)
    {
        LockNumberValue(ResultText);

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

    [RelayCommand]
    void Clear(object sender)
    {
        firstNumber = 0;
        secondNumber = 0;
        currentState = 1;
        decimalFormat = "N0";
        ResultText = "0";
        currentEntry = string.Empty;
    }

    [RelayCommand]
    void Calculate(object sender)
    {
        if (currentState == 2)
        {
            // if (secondNumber == 0)
            LockNumberValue(ResultText);

            double result = Calculator.Calculate(firstNumber, secondNumber, mathOperator);

            CurrentCalculation = $"{firstNumber} {mathOperator} {secondNumber}";

            ResultText = result.ToTrimmedString(decimalFormat);
            firstNumber = result;
            secondNumber = 0;
            currentState = -1;
            currentEntry = string.Empty;
        }
    }

    [RelayCommand]
    void Negative(object sender)
    {
        if (currentState == 1)
        {
            secondNumber = -1;
            mathOperator = "×";
            currentState = 2;
            Calculate(this);
        }
    }

    [RelayCommand]
    void Percentage(object sender)
    {
        if (currentState == 1)
        {
            LockNumberValue(ResultText);
            decimalFormat = "N2";
            secondNumber = 0.01;
            mathOperator = "×";
            currentState = 2;
            Calculate(this);
        }
    }



}
