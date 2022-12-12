# Student Finance Repayment Calculator

This is a console application which can take in a number of relevant arguments and appropriately return the percentages and amounts of student loan repayments that would be deducted from a given wage. Not especially ground-breaking stuff, I've just been getting into C# and wanted something to do that I could experiment with the basics on and get started with, so here we are.

Huge caveat on content that the UK student loan system is dark magic, subject to regular change, and none of this in any way constitutes mathematically or programmatically sound financial advice.

Currently: attempting to slim down repeated checks on user input.

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

- Particularly for past and probably future me: when using a text file for resources, if you can't seem to actually use the resources, even though you've definitely put them in the file, it's probably not massive machine error, and probably just that you forgot to run `resgen filename.txt` after adding new content to the text file.