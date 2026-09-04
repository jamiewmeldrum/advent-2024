namespace AdventOfCode2024;

public class Day05 : IDay
{
    public long SolvePart1(string[] input)
    {
        List<string> inputList = [.. input];
        int splitIndex = inputList.FindIndex(0, inputList.Count-1, x => x == "");
        List<string> orderingRules = inputList[..splitIndex];
        List<string> updates = inputList.Slice(splitIndex+1, inputList.Count-splitIndex-1);

        Dictionary<string, List<string>> rules = [];
        foreach (string orderingRule in orderingRules)
        {
            string[] splitRule = orderingRule.Split("|");
            string key = splitRule[0];
            string newValue = splitRule[1];
            rules.TryGetValue(key, out List<string>? value);
            value ??= [];
            value.Add(newValue);
            rules[key] = value;
        }

        int total = 0;
        foreach (string update in updates)
        {
            bool inOrder = true;
            List<string> pageNumbers = [.. update.Split(",")];
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
                    inOrder = false;
                    break;
                }
            }

            if (inOrder)
            {
                total += int.Parse(pageNumbers[pageNumbers.Count/2]);
            }
        }

        return total;
    }

    public long SolvePart2(string[] input)
    {
        // TODO: implement
        throw new NotImplementedException();
    }
}
