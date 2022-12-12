# Student Finance Repayment Calculator

This is a console application which can take in a number of relevant arguments and appropriately return the percentages and amounts of student loan repayments that would be deducted from a given wage. Not especially ground-breaking stuff, I've just been getting into C# and wanted something to do that I could experiment with the basics on and get started with, so here we are.

Huge caveat on content that the UK student loan system is dark magic, subject to regular change, and none of this in any way constitutes mathematically or programmatically sound financial advice.

Currently: attempting to slim down repeated checks on user input.

## Notes & Learned

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

- Particularly for past and probably future me: when using a text file for resources, if you can't seem to actually use the resources, even though you've definitely put them in the file, it's probably not massive machine error, and probably just that you forgot to run `resgen filename.txt` after adding new content to the text file.

- Useful pages for this, that might be useful later too and that I am bookmarking here, included:
    - These government pages on [what you pay](https://www.gov.uk/repaying-your-student-loan/what-you-pay) and [which repayment plan you are on](https://www.gov.uk/repaying-your-student-loan/which-repayment-plan-you-are-on) if you're on one of the current UK student loan repayment plans.
    - Also, the .NET documentation, but particularly this page on [creating resource files for .NET apps](https://learn.microsoft.com/en-us/dotnet/core/extensions/create-resource-files), this page on the [Console class](https://learn.microsoft.com/en-us/dotnet/api/system.console?view=net-7.0) and available methods, this page on [method parameters](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/method-parameters), this page on [object initialisers](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers), the style guide for [coding conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions), this [introduction to OOP in C#](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/classes?source=recommendations), this [introduction to LINQ](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/), and this page on the [CultureInfo object](https://learn.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo.invariantculture?view=net-7.0) which was a cool thing to find out about.