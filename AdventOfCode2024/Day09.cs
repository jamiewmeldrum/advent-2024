namespace AdventOfCode2024;

public class Day09 : IDay
{
    public long SolvePart1(string[] input)
    {
        List<int> rawMemory = [.. input[0].Select(c => int.Parse(c.ToString()))];

        int id = 0;
        List<int> expandedMemory = [];
        for (int i = 0; i < rawMemory.Count; i++)
        {
            int val = rawMemory[i];
            if ( i % 2 == 0)
            {
                DoXTimes(val, () => expandedMemory.Add(id));
                id++;
            }
            else
            {
                DoXTimes(val, () => expandedMemory.Add(-1));
            }
        }

        List<int> restructuredMemory = [.. expandedMemory];
        int lastIndex = restructuredMemory.Count - 1;
        for (int i = 0; i < restructuredMemory.Count; i++)
        {
            if (i > lastIndex)
            {
                break;
            }
            else if (restructuredMemory[i] == -1)
            {
                int lastOccupiedIndex = restructuredMemory.FindLastIndex(e => e != -1);
                if (lastOccupiedIndex <= i)
                {
                    break;
                }
                restructuredMemory[i] = restructuredMemory[lastOccupiedIndex];
                restructuredMemory[lastOccupiedIndex] = -1;
                lastIndex = lastOccupiedIndex;
            }
        }

        long sum = 0;
        for (int i = 0; i < restructuredMemory.FindIndex(e => e == -1); i++)
        {
            sum += (long) i * restructuredMemory[i];
        }

        return sum;
    }

    public long SolvePart2(string[] input)
    {
        List<int> rawMemory = [.. input[0].Select(c => int.Parse(c.ToString()))];

        int id = 0;
        List<int> expandedMemory = [];
        for (int i = 0; i < rawMemory.Count; i++)
        {
            int val = rawMemory[i];
            if ( i % 2 == 0)
            {
                DoXTimes(val, () => expandedMemory.Add(id));
                id++;
            }
            else
            {
                DoXTimes(val, () => expandedMemory.Add(-1));
            }
        }

        List<int> restructuredMemory = [.. expandedMemory];
        List<int> reversedMemory = [.. restructuredMemory];
        reversedMemory.Reverse();

        do {
            (int fileId, int fileRangeStart, int fileLength) = FindNextBlock(reversedMemory, 0, e => e != -1);
            if (fileRangeStart == -1)
            {
                break;
            }

            int fileOriginalStart = reversedMemory.Count - (fileRangeStart + fileLength);

            int emptySearchStart = 0;
            int emptyRangeStart;
            int emptyLength;
            bool foundFit;
            do {
                (_, emptyRangeStart, emptyLength) = FindNextBlock(restructuredMemory, emptySearchStart, e => e == -1);
                foundFit = emptyRangeStart != -1 && emptyRangeStart < fileOriginalStart && emptyLength >= fileLength;
                emptySearchStart = emptyRangeStart == -1 ? restructuredMemory.Count : emptyRangeStart + emptyLength;
            }
            while (!foundFit && emptyRangeStart != -1 && emptyRangeStart < fileOriginalStart);

            if (foundFit)
            {
                for (int i = emptyRangeStart; i < emptyRangeStart + fileLength; i++)
                {
                    restructuredMemory[i] = fileId;
                }

                int restructuredIndexEnd = fileOriginalStart + fileLength;
                for (int i = fileOriginalStart; i < restructuredIndexEnd; i++)
                {
                    restructuredMemory[i] = -1;
                }
            }

            reversedMemory = [.. reversedMemory.Skip(fileRangeStart + fileLength)];
        }
        while (reversedMemory.Count > 0);

        long sum = 0;
        for (int i = 0; i < restructuredMemory.Count; i++)
        {
            if (restructuredMemory[i] == -1)
            {
                continue;
            }
            sum += (long) i * restructuredMemory[i];
        }

        return sum;
    }

    private static void DoXTimes(int count, Action action)
    {
        for (int i = 0; i < count; i++)
        {
            action();
        }
    }

    private static (int id, int fileRangeStart, int length) FindNextBlock(List<int> memory, int startIndex, Predicate<int> blockState)
    {
        int fileRangeStart = memory.FindIndex(startIndex, blockState);
        if (fileRangeStart == -1)
        {
            return (-1, -1, 0);
        }

        int fileId = memory[fileRangeStart];
        int lookupIndex = fileRangeStart;
        while (lookupIndex + 1 < memory.Count && memory[lookupIndex + 1] == fileId)
        {
            lookupIndex++;
        }

        return (fileId, fileRangeStart, lookupIndex - fileRangeStart + 1);
    }
}
