using System;
using System.Linq;

public class Sampler
{
    // Returns the common base path between two given paths.
    public static string RelativeToCommonBase(string path1, string path2)
    {
        // Split both paths into components by the separator ('/')
        string[] components1 = path1.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        string[] components2 = path2.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

        // Determine the common base by comparing components
        int commonIndex = 0;
        while (commonIndex < components1.Length && commonIndex < components2.Length)
        {
            if (components1[commonIndex] != components2[commonIndex])
            {
                break;
            }
            commonIndex++;
        }

        // Construct the common base portion from the components
        string commonBase = string.Join("/", components1, 0, commonIndex);
        return "/" + commonBase;
    }

    // Calculates the Levenshtein distance between two strings.
    public static int LevenshteinDistance(string s1, string s2)
    {
        int len1 = s1.Length;
        int len2 = s2.Length;

        // Create a 2D array to store distances
        int[,] dp = new int[len1 + 1, len2 + 1];

        // Initialize the dp array
        for (int i = 0; i <= len1; i++)
        {
            for (int j = 0; j <= len2; j++)
            {
                if (i == 0)
                {
                    dp[i, j] = j; // Cost of inserting all characters of s2
                }
                else if (j == 0)
                {
                    dp[i, j] = i; // Cost of deleting all characters of s1
                }
                else if (s1[i - 1] == s2[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1]; // Characters match, no cost
                }
                else
                {
                    // Minimum cost of insert, delete, or replace
                    dp[i, j] = 1 + Math.Min(dp[i - 1, j], Math.Min(dp[i, j - 1], dp[i - 1, j - 1]));
                }
            }
        }
        return dp[len1, len2];
    }

    // Finds the closest word from an array of possibilities to the given word using Levenshtein distance.
    public static string ClosestWord(string word, string[] possibilities)
    {
        int minDistance = int.MaxValue;
        string closestWord = null;

        // Iterate through each possibility to find the closest word
        foreach (string possibility in possibilities)
        {
            int distance = LevenshteinDistance(word, possibility);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestWord = possibility;
            }
        }

        return closestWord;
    }

    // Calculates the speed at a given time using linear interpolation between points in time.
    public static double SpeedAtTime(double atTime, PointInTime[] path)
    {
        if (path == null || path.Length < 2)
        {
            throw new ArgumentException("Path must contain at least two points.");
        }
        if (atTime < path.First().Timestamp || atTime > path.Last().Timestamp)
        {
            throw new ArgumentOutOfRangeException("The given time is not within the range of timestamps in the path.");
        }

        // Find the points in time between which the given time falls
        PointInTime previousPoint = path[0];
        PointInTime nextPoint = path[1];

        for (int i = 1; i < path.Length; i++)
        {
            if (path[i].Timestamp > atTime)
            {
                nextPoint = path[i];
                break;
            }
            previousPoint = path[i];
        }

        // Calculate the time difference between the points
        double timeDifference = nextPoint.Timestamp - previousPoint.Timestamp;
        if (timeDifference == 0)
        {
            throw new DivideByZeroException("Time difference between points is zero, resulting in a divide by zero scenario.");
        }

        // Calculate the distance between the points
        double distance = Math.Sqrt(Math.Pow(nextPoint.X - previousPoint.X, 2) + Math.Pow(nextPoint.Y - previousPoint.Y, 2));
        return distance / timeDifference; // Speed is distance divided by time
    }

    public static void Main()
    {
        // Task 1: Finding the relative path to the common base
        string path1 = "/home/daniel/memes";
        string path2 = "/home/daniel/work";
        Console.WriteLine("Task 1: Relative Path to Common Base");
        Console.WriteLine(RelativeToCommonBase(path1, path2)); // Output: /home/daniel

        // Task 2: Finding the closest word
        string word = "hello";
        string[] possibilities = { "hallo", "hullo", "hell", "help" };
        Console.WriteLine("Task 2: Closest Word");
        Console.WriteLine(ClosestWord(word, possibilities)); // Output: hallo

        // Task 3: Calculating speed at a given time
        PointInTime[] path =
        {
            new PointInTime(0, 0, 1725100800),
            new PointInTime(3, 4, 1725101100),
            new PointInTime(6, 8, 1725101400),
            new PointInTime(10, 12, 1725101700),
            new PointInTime(15, 18, 1725102000),
            new PointInTime(20, 22, 1725102300),
            new PointInTime(25, 30, 1725102600),
            new PointInTime(30, 35, 1725102900),
            new PointInTime(35, 40, 1725103200),
            new PointInTime(40, 45, 1725103500),
            new PointInTime(45, 50, 1725103800)
        };
        Console.WriteLine("Task 3: Speed at Time");
        Console.WriteLine(SpeedAtTime(1725101750, path)); // Example output
    }
}

public struct PointInTime
{
    // Constructor for the PointInTime struct
    public PointInTime(double x, double y, double timestamp)
    {
        X = x;
        Y = y;
        Timestamp = timestamp;
    }

    // Properties for X, Y coordinates and the timestamp
    public double X { get; }
    public double Y { get; }
    public double Timestamp { get; }

    // Override ToString() for easy display of point information
    public override string ToString() => $"({X}, {Y}, {Timestamp})";
}
