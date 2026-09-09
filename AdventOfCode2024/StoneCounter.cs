namespace AdventOfCode2024;

public class StoneCounter(List<long> stones, int iterationMax)
{
    private readonly Dictionary<(long Stone, int Iteration), long> _cache = [];

    public long Count() => stones.Sum(stone => CountAfter(stone, 0));

    private long CountAfter(long stone, int iteration)
    {
        if (iteration >= iterationMax)
        {
            return 1;
        }

        (long Stone, int Iteration) key = (stone, iteration);
        if (_cache.TryGetValue(key, out long cached))
        {
            return cached;
        }

        long result = ProcessStone(stone).Sum(s => CountAfter(s, iteration + 1));
        _cache[key] = result;
        return result;
    }

    private static List<long> ProcessStone(long stone)
    {
        List<long> newStones = [];
        if (stone == 0)
        {
            newStones.Add(1);
        }
        else if (NumberHasEvenNumberOfDigits(stone))
        {
            newStones.AddRange(SplitNumberIfEvenNumberOfDigits(stone));
        }
        else
        {
            newStones.Add(stone * 2024);
        }
        return newStones;
    }

    private static bool NumberHasEvenNumberOfDigits(long number)
    {
        string numberStr = number.ToString();
        if (numberStr.Length % 2 == 0)
        {
            return true;
        }
        return false;
    }

    private static List<long> SplitNumberIfEvenNumberOfDigits(long number)
    {
        if (NumberHasEvenNumberOfDigits(number))
        {
            string numberStr = number.ToString();
            return [
                long.Parse(numberStr.Substring(0, numberStr.Length/2)),
                long.Parse(numberStr.Substring(numberStr.Length/2, numberStr.Length/2))
                ];
        }
        return [number];
    }
}
