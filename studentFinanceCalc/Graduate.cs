namespace studentFinanceCalc
{
    class Graduate
    {
        public static object GatherWage()
        {
            var wageValues = new Dictionary<string, decimal>()
            { {"hourlyWage", 0}, {"monthlyWage", 0}, {"annualWage", 0} };

            Console.WriteLine("Would you like to enter your wage hourly, monthly or annually?\nPlease type a number:\n\n1. Hourly.\t2. Monthly.\t3. Annually.");
            int wageType = int.Parse(Console.ReadLine());

            switch (wageType)
            {
                case 1:
                    Console.Write("Please enter your hourly wage: £");
                    wageValues["hourlyWage"] = decimal.Parse(Console.ReadLine());
                    Console.WriteLine(wageValues["hourlyWage"]);
                    break;
                case 2:
                    Console.Write("Please enter your monthly wage: £");
                    wageValues["monthlyWage"] = decimal.Parse(Console.ReadLine());
                    Console.WriteLine(wageValues["monthlyWage"]);
                    break;
                case 3:
                    Console.Write("Please enter your annual wage: £");
                    wageValues["annualWage"] = decimal.Parse(Console.ReadLine());
                    Console.WriteLine(wageValues["annualWage"]);
                    break;
                default:
                    Console.Write("Unreadable value provided!");
                    break;
            }
            return wageValues;
        }

        public static string GatherEducation()
        {
            return "30";
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
            GatherWage();
        }
    }
}