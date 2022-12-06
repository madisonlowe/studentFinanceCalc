using System.Globalization;

namespace studentFinanceCalc
{
    class Calculator
    {
        class Graduate
        {
            // Class per file, tidy this.
            private string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            // See: https://www.w3schools.com/cs/cs_properties.php.

            public decimal monthly_wage { get; set; }
            public decimal yearly_wage { get; set; }

            public bool plan1 { get; set; }
            public bool plan2 { get; set; }
            public bool plan4 { get; set; }
            public bool postgrad { get; set; }

            public decimal monthly_remainder { get; set; }
        }

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
                Name = "",
                monthly_wage = 0,
                yearly_wage = 0,
                plan1 = false,
                plan2 = false,
                plan4 = false,
                postgrad = false
            };

            Console.WriteLine("Would you like to enter your wage monthly or yearly?\nPlease type a number:\n\n1. Monthly.\t2. Yearly.");
            int wageType = int.Parse(Console.ReadLine());

            switch (wageType)
            {
                case 1:
                    Console.Write("Please enter your monthly wage: £");
                    graduate.monthly_wage = decimal.Parse(Console.ReadLine());
                    graduate.yearly_wage = graduate.monthly_wage * 12m;
                    break;
                case 2:
                    Console.Write("Please enter your yearly wage: £");
                    graduate.yearly_wage = decimal.Parse(Console.ReadLine());
                    graduate.monthly_wage = graduate.yearly_wage / 12m;
                    break;
                default:
                    Console.Write("Unreadable value provided!");
                    break;
            }

            string plan1 = "1. You’re on Plan 1 if you’re:\n- An English or Welsh student who started an undergraduate course anywhere in the UK before 1 September 2012.\n- A Northern Irish student who started an undergraduate or postgraduate course anywhere in the UK on or after 1 September 1998.\n- An EU student who started an undergraduate course in England or Wales on or after 1 September 1998, but before 1 September 2012.\n- An EU student who started an undergraduate or postgraduate course in Northern Ireland on or after 1 September 1998.";
            string plan2 = "2. You’re on Plan 2 if you’re:\n- An English or Welsh student who started an undergraduate course anywhere in the UK on or after 1 September 2012.\n- An EU student who started an undergraduate course in England or Wales on or after 1 September 2012.\n- Someone who took out an Advanced Learner Loan on or after 1 August 2013.\n- Someone who took out a Higher Education Short Course Loan on or after 1 September 2022.";
            string plan4 = "3. You’re on Plan 4 if you’re:\n- A Scottish student who started an undergraduate or postgraduate course anywhere in the UK on or after 1 September 1998.\n- An EU student who started an undergraduate or postgraduate course in Scotland on or after 1 September 1998.";
            string postgrad = "4. You’re on a Postgraduate Loan repayment plan if you’re:\n- An English or Welsh student who took out a Postgraduate Master’s Loan on or after 1 August 2016.\n- An English or Welsh student who took out a Postgraduate Doctoral Loan on or after 1 August 2018.\nAn EU student who started a postgraduate course on or after 1 August 2016.";


            Console.WriteLine($"Which of these statements is true?\nMore than one statement may be true if you have completed more than one programme of study.\nPlease type a number for each plan that applies to you, then press enter.\n\n{plan1}\n\n{plan2}\n\n{plan4}\n\n{postgrad}");
            string qualifications = Console.ReadLine();
            string parsedQualifications = new(qualifications.Where(Char.IsDigit).ToArray());

            char[] planArr = parsedQualifications.ToArray();

            foreach (char c in planArr)
            {
                switch (c)
                {
                    case '1': // Casting c if 1 to char, otherwise tries to convert to string or int.
                        graduate.plan1 = true;
                        break;
                    case '2':
                        graduate.plan2 = true;
                        break;
                    case '3':
                        graduate.plan4 = true;
                        break;
                    case '4':
                        graduate.postgrad = true;
                        break;
                    default:
                        Console.WriteLine("Invalid value provided. You are not on any plan.");
                        break;
                }
            }

            if (graduate.plan1)
            {
                graduate.monthly_remainder += (graduate.monthly_wage - PLAN_1_MONTHLY_THRESHOLD) * PLANS_1_2_4_TAX;
            }

            if (graduate.plan2)
            {
                graduate.monthly_remainder += (graduate.monthly_wage - PLAN_2_MONTHLY_THRESHOLD) * PLANS_1_2_4_TAX;
            }

            if (graduate.plan4)
            {
                graduate.monthly_remainder += (graduate.monthly_wage - PLAN_4_MONTHLY_THRESHOLD) * PLANS_1_2_4_TAX;
            }

            if (graduate.postgrad)
            {
                graduate.monthly_remainder += (graduate.monthly_wage - POSTGRAD_MONTHLY_THRESHOLD) * POSTGRAD_TAX;
            }

            decimal yearlyRemaining = graduate.monthly_remainder * 12m; // Move to same place as monthly_remainder?

            decimal monthlyLeftover = graduate.monthly_wage - graduate.monthly_remainder;
            decimal yearlyLeftover = graduate.yearly_wage - yearlyRemaining;

            Console.WriteLine($"Out of a monthly pre-deduction wage of £{graduate.monthly_wage.ToString("F", CultureInfo.InvariantCulture)}, you will repay £{graduate.monthly_remainder.ToString("F", CultureInfo.InvariantCulture)} every month in tax towards your student loans, leaving £{monthlyLeftover.ToString("F", CultureInfo.InvariantCulture)}. Out of a yearly pre-deduction wage of £{graduate.yearly_wage.ToString("F", CultureInfo.InvariantCulture)}, you will repay £{yearlyRemaining.ToString("F", CultureInfo.InvariantCulture)} every year in tax towards your student loans, leaving £{yearlyLeftover.ToString("F", CultureInfo.InvariantCulture)}.\n\nThese figures do not account for other deductions for things like National Insurance and pension contributions. Also, they're just an estimate, I'm pretty bad at maths, and the interest rates for loans change at least yearly and sometimes more often! So double-check the government website!");

            // Note: there are other things we are taxed for, I can work those out and add on later.
        }
    }
}