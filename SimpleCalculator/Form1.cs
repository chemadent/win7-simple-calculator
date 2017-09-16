using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        Double value = 0;
        Double secondTerm = 0;
        String operation = "";
        bool divisor_zero = false;
        bool operation_pressed = false;
        bool equation_pressed = false;
        bool isFirstDigit = true;
        bool haveValue = false;
        bool haveSecondTerm = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            /* users cannot type any button except "CE" and "C"
             * when the divisor is zero
             */
            if (divisor_zero == true)
                return;
            Button b = (Button)sender;
            if (operation_pressed && isFirstDigit)
            {
                result.Clear();
            }
            if (equation_pressed && isFirstDigit)
            {
                result.Clear();
            }
            if (result.Text == "0")
                result.Clear();
            if (b.Text == ".")
            {
                // two or more dots are not allowed
                if (result.Text.Contains("."))
                    return;
                else if (result.Text == "")
                {
                    result.Text = "0";
                }
                isFirstDigit = false;
            }
            else
            {
                if (isFirstDigit == true)
                {
                    result.Clear();
                    isFirstDigit = false;
                }
            }
            if (operation_pressed && !(isFirstDigit))
                haveSecondTerm = true;
            result.Text = result.Text + b.Text;
        }

        private void cleanEntry_Click(object sender, EventArgs e)
        {
            if (divisor_zero == true)
            {
                reset();
            }
            // when we click "CE", the second term will be set to zero as a default
            result.Text = "0";
            haveSecondTerm = true;
        }

        private void clean_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void reset()
        {
            divisor_zero = false;
            operation_pressed = false;
            equation_pressed = false;
            isFirstDigit = true;
            haveValue = false;
            haveSecondTerm = false;
            value = 0;
            secondTerm = 0;
            operation = "";
            result.Text = "0";
            equation.Text = "";
        }

        private void operator_Click(object sender, EventArgs e)
        {
            if (divisor_zero == true)
                return;
            Button b = (Button)sender;
            operation = b.Text;
            operation_pressed = true;
            isFirstDigit = true;
            if (haveSecondTerm)
            {
                calculate();
                haveSecondTerm = false;
            }
            value = Double.Parse(result.Text);
            haveValue = true;
            result.Text = value.ToString();
            equation.Text = value + " " + operation;
        }

        private void equal_Click(object sender, EventArgs e)
        {
            if (divisor_zero == true)
                return;
            calculate();
            equation_pressed = true;
        }
        private void calculate()
        {
            equation.Text = "";
            isFirstDigit = true;
            if (haveValue == false)
            {
                value = Double.Parse(result.Text);
            }
            else
            {
                secondTerm = Double.Parse(result.Text);
            }
            switch (operation)
            {
                case "+":
                    result.Text = (value + secondTerm).ToString();
                    haveValue = false;
                    break;
                case "-":
                    result.Text = (value - secondTerm).ToString();
                    haveValue = false;
                    break;
                case "*":
                    result.Text = (value * secondTerm).ToString();
                    haveValue = false;
                    break;
                case "/":
                    Double divisor = secondTerm;
                    if (divisor == 0)
                    {
                        divisor_zero = true;
                        if (value == 0)
                            result.Text = "Not defined";
                        else
                            result.Text = "Cannot divide by zero";
                    }
                    else
                        result.Text = (value / divisor).ToString();
                    haveValue = false;
                    break;
                default:
                    result.Text = Double.Parse(result.Text).ToString();
                    break;
            }//end switch
        }
    }
}