using System.Globalization;

namespace studentFinanceCalc
{
    // Another example:
    /* class Account {
     * private decimal balance = 0;
     * 
     * public bool WithdrawFunds ( decimal amount )
     * {
     * if ( balance < amount )
     * { return false; }
     * balance = balance - amount;
     * return true;
     * }
     * }
     */
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
            get { return Monthly_Remainder; }
            set { Monthly_Remainder = value; }
        }

        private decimal yearly_remainder = 0m;
        public decimal Yearly_Remainder
        {
            get { return Yearly_Remainder; }
            set { Yearly_Remainder = value; }
        }

        public decimal SetRemainders(bool planType, decimal threshold, decimal tax)
        {
            planType = true;
            Monthly_Remainder += (Monthly_Wage - threshold) * tax;
            Yearly_Remainder = Monthly_Remainder * 12m;
            return Monthly_Remainder;
        }

        // Student payment plan bools.
        public bool plan1 { get; set; }
        public bool plan2 { get; set; }
        public bool plan4 { get; set; }
        public bool postgrad { get; set; }
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

            Graduate graduate = new Graduate()
            {
                plan1 = false,
                plan2 = false,
                plan4 = false,
                postgrad = false
            };
            // Add a constructor to Graduate and then initialise this as: new Graduate("", 0, 0, false ...) etc.?

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

            string plan1 = "1. You’re on Plan 1 if you’re:\n- An English or Welsh student who started an undergraduate course anywhere in the UK before 1 September 2012.\n- A Northern Irish student who started an undergraduate or postgraduate course anywhere in the UK on or after 1 September 1998.\n- An EU student who started an undergraduate course in England or Wales on or after 1 September 1998, but before 1 September 2012.\n- An EU student who started an undergraduate or postgraduate course in Northern Ireland on or after 1 September 1998.";
            string plan2 = "2. You’re on Plan 2 if you’re:\n- An English or Welsh student who started an undergraduate course anywhere in the UK on or after 1 September 2012.\n- An EU student who started an undergraduate course in England or Wales on or after 1 September 2012.\n- Someone who took out an Advanced Learner Loan on or after 1 August 2013.\n- Someone who took out a Higher Education Short Course Loan on or after 1 September 2022.";
            string plan4 = "3. You’re on Plan 4 if you’re:\n- A Scottish student who started an undergraduate or postgraduate course anywhere in the UK on or after 1 September 1998.\n- An EU student who started an undergraduate or postgraduate course in Scotland on or after 1 September 1998.";
            string postgrad = "4. You’re on a Postgraduate Loan repayment plan if you’re:\n- An English or Welsh student who took out a Postgraduate Master’s Loan on or after 1 August 2016.\n- An English or Welsh student who took out a Postgraduate Doctoral Loan on or after 1 August 2018.\nAn EU student who started a postgraduate course on or after 1 August 2016.";
            // Can I move this somewhere tidier?

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
                        graduate.plan1 = true;
                        graduate.monthly_remainder += (graduate.monthly_wage - PLAN_1_MONTHLY_THRESHOLD) * PLANS_1_2_4_TAX;
                        break;
                    case '2':
                        graduate.plan2 = true;
                        graduate.monthly_remainder += (graduate.monthly_wage - PLAN_2_MONTHLY_THRESHOLD) * PLANS_1_2_4_TAX;
                        break;
                    case '3':
                        graduate.plan4 = true;
                        graduate.monthly_remainder += (graduate.monthly_wage - PLAN_4_MONTHLY_THRESHOLD) * PLANS_1_2_4_TAX;
                        break;
                    case '4':
                        graduate.postgrad = true;
                        graduate.monthly_remainder += (graduate.monthly_wage - POSTGRAD_MONTHLY_THRESHOLD) * POSTGRAD_TAX;
                        break;
                    default:
                        Console.WriteLine("Invalid value provided. You are not on any plan.");
                        break;
                }
            }
            // Variables are different, so not sure how to clean up, but can surely?

            decimal yearly_remainder = graduate.monthly_remainder * 12m;
            // Move as a method internal to class? Dislike that it is just sat here.

            decimal monthlyLeftover = graduate.monthly_wage - graduate.monthly_remainder;
            decimal yearlyLeftover = graduate.yearly_wage - graduate.yearly_remainder;

            Console.WriteLine($"Out of a monthly pre-deduction wage of £{graduate.monthly_wage.ToString("F", CultureInfo.InvariantCulture)}, you will repay £{graduate.monthly_remainder.ToString("F", CultureInfo.InvariantCulture)} every month in tax towards your student loans, leaving £{monthlyLeftover.ToString("F", CultureInfo.InvariantCulture)}. Out of a yearly pre-deduction wage of £{graduate.yearly_wage.ToString("F", CultureInfo.InvariantCulture)}, you will repay £{graduate.yearly_remainder.ToString("F", CultureInfo.InvariantCulture)} every year in tax towards your student loans, leaving £{yearlyLeftover.ToString("F", CultureInfo.InvariantCulture)}.\n\nThese figures do not account for other deductions for things like National Insurance and pension contributions. Also, they're just an estimate, I'm pretty bad at maths, and the interest rates for loans change at least yearly and sometimes more often! So double-check the government website!");

            // There are other things we are taxed for, I can work those out and add on later.
        }
    }
}