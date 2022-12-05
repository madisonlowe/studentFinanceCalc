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

            public string student_type { get; set; }

            public bool plan1 { get; set; }
            public bool plan2 { get; set; }
            public bool plan4 { get; set; }
            public bool postgrad { get; set; }
        }

        public static object GatherInfo()
        {

            Graduate graduate = new Graduate()
            {
                Name = "",
                monthly_wage = 0,
                yearly_wage = 0,
                student_type = "",
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


            Console.WriteLine($"Which of these statements is true?\nMore than one statement may be true if you have completed more than one programme of study.\nPlease type a number for each plan that applies to you.\n\n{plan1}\n\n{plan2}\n\n{plan4}\n\n{postgrad}");
            string qualifications = Console.ReadLine();
            string parsedQualifications = new String(qualifications.Where(Char.IsDigit).ToArray());

            char[] planArr = parsedQualifications.ToArray();

            foreach (char c in planArr)
            {
                int parsedChar = int.Parse(c);
            }

            return graduate;
        }


        static void Main()
        {


            // goal: calculate tax that will be taken off of you for uk student loans

            // variables we need from user: annual wage, monthly wage, when you graduated, where you're from
            // plan 1: current interest rate (changes regularly), threshhold for repayment, monthly wage
            // plan 2: current interest rate (changes regularly), threshhold for repayment, monthly wage
            // plan 4: current interest rate (changes regularly), threshhold for repayment, monthly wage
            // masters: current interest rate (changes regularly), threshhold for repayment, monthly wage

            // class Student

            // calculate for plan 1, plan 2, plan 4, and masters repayments

            // take in: when they graduated, what from, how many degrees
            // take in: will you be entering your wage hourly or annually
            // - if annual: take in annual wage pre-tax
            // - if hourly: take in hours worked monthly,
            //   calculate monthly wage and then annual wage,
            //   show back to user and ask if it seems correct before proceeding

            // should also calculate general tax someone will pay to show net result?
            GatherInfo();
        }
    }
}