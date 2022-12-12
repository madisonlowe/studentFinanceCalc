# Student Finance Repayment Calculator

This is a console application which can take in a number of relevant arguments and appropriately return the percentages and amounts of student loan repayments that would be deducted from a given wage.

Huge caveat that the UK student loan system is dark magic, subject to regular change, and none of this in any way constitutes mathematically or programmatically sound financial advice. I'm just getting into C# and wanted something to experiment with the basics on, so here we are!

Currently: attempting to add Resources, but I changed the file name a while back for this project, and it's come back to haunt me every time I try to run a build.

Fun things learned:

- When declaring variables in cases for a switch case, single speech marks are what will cast a number to `char` type. Ie. `case '1'` will look for `char` input of `1` whereas `case 1` would look for an `int` and `case "1"` would look for a string.

- Keeping with switch cases, didn't realise you could assign more than one case to an outcome. Eg:

```csharp
switch (example)
{
    case "1":
    case "one":
        // Code to be performed here.
}
```