using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace BadaniaOperacyjne.ValidationRules
{
    public class DoubleRangeChecker : DependencyObject
    {
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(DoubleRangeChecker));
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(DoubleRangeChecker));
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
    }

    public class RangeRule : ValidationRule
    {
        public DoubleRangeChecker ValidRange { get; set; }
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public bool Exclusive { get; set; }

        public RangeRule()
        {
            Exclusive = false;
        }

        public override ValidationResult Validate(object obj, System.Globalization.CultureInfo cultureInfo)
        {
            System.Diagnostics.Debug.WriteLine("validating field");
            double? value = null;
            try
            {
                value = double.Parse(((string)obj), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                System.Diagnostics.Debug.WriteLine("cool format: " + (string)obj + " -> " + value);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("wrong format: " + (string)obj);
                return new ValidationResult(false, "Wpisz liczbę");
            }

            if (!Exclusive && (value < Minimum || value > Maximum)
                || Exclusive && (value <= Minimum || value >= Maximum))
            {
                System.Diagnostics.Debug.WriteLine("validation, value: " + value + ", min: " + Minimum + ", max:" + Maximum);
                return new ValidationResult(false, "Wpisz liczbę z przedziału " + Minimum + " - " + Maximum);
            }
            return new ValidationResult(true, null);
        }
    }

}
