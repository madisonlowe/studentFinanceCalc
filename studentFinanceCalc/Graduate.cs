namespace studentFinanceCalc
{
    class Graduate
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        // Specific example above something I wanted to remember that I read in an article.
        // The private string is representative of a 'sensitive' value.
        // We provide read and write permissions through a public proxy value - Name - with get and set.
        // Conventionally, lower-case 'name' is the field and upper-case 'Name' is the property.
        // See: https://www.w3schools.com/cs/cs_properties.php.

        public decimal monthly_wage { get; set; }
        public decimal yearly_wage { get; set; }

        public bool plan1 { get; set; }
        public bool plan2 { get; set; }
        public bool plan4 { get; set; }
        public bool postgrad { get; set; }

        // Google: how to make bool[] of named values eg. [plan1, plan2, plan4, postgrad].
    }

    class Calculator
    {

        public static object GatherInfo()
        {

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
                    graduate.yearly_wage = graduate.monthly_wage * 12;
                    // Above calculation doesn't come out the same as case 1. Numerical type specificity issue?
                    Console.WriteLine($"Based on that number, before tax and other deductions, your monthly wage is £{graduate.monthly_wage} and your yearly wage is £{graduate.yearly_wage}.");
                    break;
                case 2:
                    Console.Write("Please enter your yearly wage: £");
                    graduate.yearly_wage = decimal.Parse(Console.ReadLine());
                    graduate.monthly_wage = graduate.yearly_wage / 12;
                    Console.WriteLine($"Based on that number, before tax and other deductions, your monthly wage is £{graduate.monthly_wage} and your yearly wage is £{graduate.yearly_wage}.");
                    break;
                default:
                    Console.Write("Unreadable value provided!");
                    break;
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