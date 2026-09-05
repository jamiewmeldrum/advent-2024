namespace AdventOfCode2024;

public class Day05 : IDay
{
    public long SolvePart1(string[] input)
    {
        var (rules, updates) = Parse(input);

        int total = 0;
        foreach (string update in updates)
        {
            List<string> pageNumbers = [.. update.Split(",")];
            if (IsInOrder(rules, pageNumbers))
            {
                total += int.Parse(pageNumbers[pageNumbers.Count/2]);
            }
        }

        return total;
    }

    public long SolvePart2(string[] input)
    {
        var (rules, updates) = Parse(input);

        int total = 0;
        foreach (string update in updates)
        {
            List<string> pageNumbers = [.. update.Split(",")];
            if (IsInOrder(rules, pageNumbers))
            {
                continue;
            }

            pageNumbers = Shift(rules, pageNumbers);
            total += int.Parse(pageNumbers[pageNumbers.Count/2]);
        }

        return total;
    }

    private static (Dictionary<string, List<string>> Rules, List<string> Updates) Parse(string[] input)
    {
        List<string> inputList = [.. input];
        int splitIndex = inputList.IndexOf("");
        List<string> orderingRules = inputList[..splitIndex];
        List<string> updates = inputList[(splitIndex + 1)..];

        Dictionary<string, List<string>> rules = [];
        foreach (string orderingRule in orderingRules)
        {
            string[] splitRule = orderingRule.Split("|");
            string key = splitRule[0];
            string newValue = splitRule[1];
            if (!rules.TryGetValue(key, out List<string>? pages))
            {
                pages = [];
                rules[key] = pages;
            }
            pages.Add(newValue);
        }

        return (rules, updates);
    }

    private static bool IsInOrder(Dictionary<string, List<string>> rules, List<string> pageNumbers)
    {
        for (int i = 0; i < pageNumbers.Count; i++)
        {
            string pageNumber = pageNumbers[i];
            rules.TryGetValue(pageNumber, out List<string>? pagesThatMustComeAfter);
            if (pagesThatMustComeAfter is null)
            {
                continue;
            }

            List<string> pageNumbersThatComeBefore = pageNumbers[..i];
            if (pageNumbersThatComeBefore.Any(pagesThatMustComeAfter.Contains))
            {
                return false;
            }
        }

        return true;
    }

    private static List<string> Shift(Dictionary<string, List<string>> rules, List<string> pageNumbers)
    {
        List<string> shifted = [.. pageNumbers];

        do
        {
            for (int i = 0; i < shifted.Count; i++)
            {
                string pageNumber = shifted[i];
                rules.TryGetValue(pageNumber, out List<string>? pagesThatMustComeAfter);
                if (pagesThatMustComeAfter is null)
                {
                    continue;
                }

                List<string> pageNumbersThatComeBefore = shifted[..i];
                List<string> overlap = [.. pageNumbersThatComeBefore.Intersect(pagesThatMustComeAfter)];
                if (overlap.Count > 0)
                {
                    string elementToShift = overlap[0];
                    int indexToMoveForward = shifted.FindIndex(0, shifted.Count-1, x => x == elementToShift);
                    shifted.Insert(indexToMoveForward, pageNumber);
                    shifted.RemoveAt(indexToMoveForward+1);
                    shifted.Insert(i, elementToShift);
                    shifted.RemoveAt(i+1);
                    break;
                }
            }
        } while (!IsInOrder(rules, shifted));

        return shifted;
    }
}
