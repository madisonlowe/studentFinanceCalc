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
        // Class per file? Add error handling to methods.

        // Monthly wage variable and methods.
        private decimal monthly_wage = 0m;
        public decimal Monthly_Wage
        {
            get { return monthly_wage; }
            set { monthly_wage = value; }
        }

        public decimal GatherMonthlyWage() // Add error handling.
        {
            Console.Write("Please enter your monthly wage: £");
            Monthly_Wage = decimal.Parse(Console.ReadLine());
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

        public decimal GatherYearlyWage() // Add error handling.
        {
            Console.Write("Please enter your yearly wage: £");
            Yearly_Wage = decimal.Parse(Console.ReadLine());
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
            return Monthly_Remainder;
        }

        // Student payment plan bools.
        public bool plan1 { get; set; } = false;
        public bool plan2 { get; set; } = false;
        public bool plan4 { get; set; } = false;
        public bool postgrad { get; set; } = false;
    }

    class Calculator
    {
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
            int wageType = int.Parse(Console.ReadLine());
            // Add more error-handling to ReadLines to get rid of warnings in case of improper input?

            switch (wageType) // Change to while loop? To allow for default case relooping?
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

            string plan1 = rm.GetString("plan1");
            string plan2 = rm.GetString("plan2");
            string plan4 = rm.GetString("plan4");
            string postgrad = rm.GetString("postgrad");

            Console.WriteLine($"Which of these statements is true?\nMore than one statement may be true if you have completed more than one programme of study.\nPlease type a number for each plan that applies to you, then press enter.\n\n{plan1}\n\n{plan2}\n\n{plan4}\n\n{postgrad}");
            // Couldn't move this to Resources with variables, as didn't show variable text, just their names.
            // Could potentially make one resource which combines all strings and above writeline into one big paragraph.
            // However: if details for just one plan change, might be hard to search or update neatly in future.
            // So leaving as is for now, even though it's a couple extra lines of variable assignment.

            string qualifications = Console.ReadLine();
            string parsedQualifications = new String(qualifications.Where(Char.IsDigit).ToArray());
            char[] planArr = parsedQualifications.ToArray();
            foreach (char c in planArr)
            {
                switch (c)
                {
                    case '1':
                        // Casting c if 1 to char with '', otherwise tries to convert to string or int.
                        // You can use more than one case for the same outcome. Eg. could have case '1' and case "one".
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

            // There are other things we are taxed for, I can work those out and add on later.
        }
    }
}