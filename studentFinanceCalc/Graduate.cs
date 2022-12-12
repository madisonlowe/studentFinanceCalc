using System;
using System.Linq;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace studentFinanceCalc
{
    class Graduate
    {
        // Monthly wage variable and methods.
        private decimal monthly_wage = 0m;
        public decimal Monthly_Wage
        {
            get { return monthly_wage; }
            set { monthly_wage = value; }
        }

        public decimal GatherMonthlyWage()
        {
            Console.Write("Please enter your monthly wage: £");
            string? input = Console.ReadLine();
            while (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input) || input.All(Char.IsLetter))
            {
                Console.WriteLine("Invalid input! Please type a number.");
                input = Console.ReadLine();
            }
            Monthly_Wage = decimal.Parse(input);
            Yearly_Wage = Monthly_Wage * 12m;
            return Monthly_Wage;
        }

        // Yearly wage variable and methods.
        private decimal yearly_wage = 0m;
        public decimal Yearly_Wage
        {
            get { return yearly_wage; }
            set { yearly_wage = value; }
        }

        public decimal GatherYearlyWage()
        {
            Console.Write("Please enter your yearly wage: £");
            string? input = Console.ReadLine();
            while (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input) || input.All(Char.IsLetter))
            {
                Console.WriteLine("Invalid input! Please type a number.");
                input = Console.ReadLine();
            }
            Yearly_Wage = decimal.Parse(input);
            Monthly_Wage = Yearly_Wage / 12m;
            return Yearly_Wage;
        }

        // Remainder variables and methods.
        private decimal monthly_remainder = 0m;
        public decimal Monthly_Remainder
        {
            get { return monthly_remainder; }
            set { monthly_remainder = value; }
        }

        private decimal yearly_remainder = 0m;
        public decimal Yearly_Remainder
        {
            get { return yearly_remainder; }
            set { yearly_remainder = value; }
        }

        public decimal SetRemainders(bool planType, decimal threshold, decimal tax)
        {
            planType = true;
            Monthly_Remainder += (Monthly_Wage - threshold) * tax;
            Yearly_Remainder = Monthly_Remainder * 12m; // Side-effects?
            return Monthly_Remainder; // Don't love these returns, vis-a-vis above comment.
        }

        // Student payment plan bools.
        public bool plan1 { get; set; } = false;
        public bool plan2 { get; set; } = false;
        public bool plan4 { get; set; } = false;
        public bool postgrad { get; set; } = false;
    }

    class Calculator
    {
        static string Checker(string? input)
        {
            while (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input) || !input.All(Char.IsNumber))
            {
                Console.WriteLine("Invalid input! Please type a number.");
                input = Console.ReadLine();
            }
            return input;
        }

        static void Main()
        {
            const decimal PLAN_1_MONTHLY_THRESHOLD = 1682m;
            const decimal PLAN_2_MONTHLY_THRESHOLD = 2274m;
            const decimal PLAN_4_MONTHLY_THRESHOLD = 2114m;
            const decimal POSTGRAD_MONTHLY_THRESHOLD = 1750m;
            const decimal PLANS_1_2_4_TAX = 0.09m;
            const decimal POSTGRAD_TAX = 0.06m;

            ResourceManager rm = new ResourceManager("studentFinanceCalc.Resources",
                               typeof(Calculator).Assembly);
            Graduate graduate = new Graduate();

            Console.WriteLine(rm.GetString("monthlyOrYearly"));
            string? wageInput = Checker(Console.ReadLine());
            int wageType = int.Parse(wageInput);

            switch (wageType)
            {
                case 1:
                    graduate.GatherMonthlyWage();
                    break;
                case 2:
                    graduate.GatherYearlyWage();
                    break;
                default:
                    Console.Write("Unreadable value provided!");
                    break;
            }

            string? planIntro = rm.GetString("planIntro");
            string? plan1 = rm.GetString("plan1");
            string? plan2 = rm.GetString("plan2");
            string? plan4 = rm.GetString("plan4");
            string? postgrad = rm.GetString("postgrad");

            Console.WriteLine($"{planIntro}\n\n{plan1}\n\n{plan2}\n\n{plan4}\n\n{postgrad}");
            // Could potentially make one resource which combines all strings and above writeline into one big paragraph,
            // then would only have to call one variable and display it once.
            // However: wanted Resources.txt to be readable and editable, which I didn't think it would be with one giant variable.
            // So leaving as is for now.

            string? qualifications = Checker(Console.ReadLine());
            string parsedQualifications = new String(qualifications.Where(Char.IsDigit).ToArray());
            char[] planArr = parsedQualifications.ToArray();

            foreach (char c in planArr)
            {
                switch (c)
                {
                    case '1':
                        graduate.SetRemainders(graduate.plan1, PLAN_1_MONTHLY_THRESHOLD, PLANS_1_2_4_TAX);
                        break;
                    case '2':
                        graduate.SetRemainders(graduate.plan2, PLAN_2_MONTHLY_THRESHOLD, PLANS_1_2_4_TAX);
                        break;
                    case '3':
                        graduate.SetRemainders(graduate.plan4, PLAN_4_MONTHLY_THRESHOLD, PLANS_1_2_4_TAX);
                        break;
                    case '4':
                        graduate.SetRemainders(graduate.postgrad, POSTGRAD_MONTHLY_THRESHOLD, POSTGRAD_TAX);
                        break;
                    default:
                        Console.WriteLine("Invalid value provided. You are not on any plan.");
                        break;
                }
            }

            decimal monthlyLeftover = graduate.Monthly_Wage - graduate.Monthly_Remainder;
            decimal yearlyLeftover = graduate.Yearly_Wage - graduate.Yearly_Remainder;

            Console.WriteLine($"Out of a monthly pre-deduction wage of £{graduate.Monthly_Wage.ToString("F", CultureInfo.InvariantCulture)}, you will repay £{graduate.Monthly_Remainder.ToString("F", CultureInfo.InvariantCulture)} every month in tax towards your student loans, leaving £{monthlyLeftover.ToString("F", CultureInfo.InvariantCulture)}. Out of a yearly pre-deduction wage of £{graduate.Yearly_Wage.ToString("F", CultureInfo.InvariantCulture)}, you will repay £{graduate.Yearly_Remainder.ToString("F", CultureInfo.InvariantCulture)} every year in tax towards your student loans, leaving £{yearlyLeftover.ToString("F", CultureInfo.InvariantCulture)}.");
            Console.WriteLine(rm.GetString("disclaimer"));
        }
    }
}