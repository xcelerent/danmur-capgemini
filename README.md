# Danmur Capgemini Project

This project includes various tasks implemented in C#. Below is a brief description of the tasks and how to run the project.

## How to Run

1. **Build the project**:
    ```sh
    dotnet build
    ```

2. **Run the project**:
    ```sh
    dotnet run
    ```

## Example Output

![sample output](https://i.imgur.com/S2YhnwV.png)

## Files

- [Program.cs](Program.cs): Contains the main logic for the tasks.
- [danmur-capgemini.csproj](danmur-capgemini.csproj): Project file containing metadata and dependencies.

## Requirements

- .NET 8.0 SDK or later

## Notes

### StringSimilarity

I tried to use 3rd party .net library to do the string distance calcuation. But I could not find one compatible with .net 8.
The one I tried to use is [F23.StringSimilarity](https://github.com/feature23/StringSimilarity.NET). However it from https://www.nuget.org/packages?q=F23.StringSimilarity, it seems it only support .net 4.6.1 which is already out of main stream support. As such, I search and copy the code from https://stackoverflow.com/questions/6944056/compare-string-similarity.
My normal preference is always for for proven 3rd party libary first.

I have done similar task before with Python 3rd party library.

![](https://i.imgur.com/AoUNPU8.png)

### Improvement Suggestions

    1. Implement Test-Driven Development (TDD): Write unit tests for each of the public methods in the `Sampler` class.
        - Create test cases for `RelativeToCommonBase`, `LevenshteinDistance`, `ClosestWord`, and `SpeedAtTime` methods.
        - Cover edge cases such as empty inputs, null inputs, boundary values, and normal scenarios.

    2. Try to find a proven working 3rd party library to handle string similarity comparison.

    3. Refactor `SpeedAtTime` method to use binary search for finding the appropriate interval, which would improve performance for large datasets.

    4. Introduce logging for exceptions to help in debugging issues during runtime.
