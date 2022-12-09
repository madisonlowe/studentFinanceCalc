using System.Globalization;
using System.Resources;

namespace studentFinanceCalc
{
    class Graduate
    {
        // Class per file?

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

        public decimal GatherYearlyWage()
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

            Graduate graduate = new Graduate(); // Try constructor next time.

            Console.WriteLine("Would you like to enter your wage monthly or yearly?\nPlease type a number:\n\n1. Monthly.\t2. Yearly.");
            int wageType = int.Parse(Console.ReadLine());
            // Add more error-handling to ReadLines to get rid of warnings in case of improper input?

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

            string plan1 = "1. You are on Plan 1 if you are:\n- An English or Welsh student who started an undergraduate course anywhere in the UK before 1 September 2012.\n- A Northern Irish student who started an undergraduate or postgraduate course anywhere in the UK on or after 1 September 1998.\n- An EU student who started an undergraduate course in England or Wales on or after 1 September 1998, but before 1 September 2012.\n- An EU student who started an undergraduate or postgraduate course in Northern Ireland on or after 1 September 1998.";
            string plan2 = "2. You are on Plan 2 if you are:\n- An English or Welsh student who started an undergraduate course anywhere in the UK on or after 1 September 2012.\n- An EU student who started an undergraduate course in England or Wales on or after 1 September 2012.\n- Someone who took out an Advanced Learner Loan on or after 1 August 2013.\n- Someone who took out a Higher Education Short Course Loan on or after 1 September 2022.";
            string plan4 = "3. You are on Plan 4 if you are:\n- A Scottish student who started an undergraduate or postgraduate course anywhere in the UK on or after 1 September 1998.\n- An EU student who started an undergraduate or postgraduate course in Scotland on or after 1 September 1998.";
            string postgrad = "4. You are on a Postgraduate Loan repayment plan if you are:\n- An English or Welsh student who took out a Postgraduate Master’s Loan on or after 1 August 2016.\n- An English or Welsh student who took out a Postgraduate Doctoral Loan on or after 1 August 2018.\nAn EU student who started a postgraduate course on or after 1 August 2016.";
            // Attempting to move this to Resources but encountering about a million build errors from an earlier rename.

            Console.WriteLine($"Which of these statements is true?\nMore than one statement may be true if you have completed more than one programme of study.\nPlease type a number for each plan that applies to you, then press enter.\n\n{plan1}\n\n{plan2}\n\n{plan4}\n\n{postgrad}");
            string qualifications = Console.ReadLine();
            string parsedQualifications = new(qualifications.Where(Char.IsDigit).ToArray());

            char[] planArr = parsedQualifications.ToArray();

            foreach (char c in planArr)
            {
                switch (c)
                {
                    case '1': // Casting c if 1 to char with '', otherwise tries to convert to string or int.
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

            Console.WriteLine($"Out of a monthly pre-deduction wage of £{graduate.Monthly_Wage.ToString("F", CultureInfo.InvariantCulture)}, you will repay £{graduate.Monthly_Remainder.ToString("F", CultureInfo.InvariantCulture)} every month in tax towards your student loans, leaving £{monthlyLeftover.ToString("F", CultureInfo.InvariantCulture)}. Out of a yearly pre-deduction wage of £{graduate.Yearly_Wage.ToString("F", CultureInfo.InvariantCulture)}, you will repay £{graduate.Yearly_Remainder.ToString("F", CultureInfo.InvariantCulture)} every year in tax towards your student loans, leaving £{yearlyLeftover.ToString("F", CultureInfo.InvariantCulture)}.\n\nThese figures do not account for other deductions for things like National Insurance and pension contributions. Also, they're just an estimate, I'm pretty bad at maths, and the interest rates for loans change at least yearly and sometimes more often! So double-check the government website!");

            // There are other things we are taxed for, I can work those out and add on later.
        }
    }
}