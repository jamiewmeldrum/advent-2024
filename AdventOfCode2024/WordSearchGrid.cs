namespace AdventOfCode2024;

public class WordSearchGrid(string[] input) : Grid(input)
{
    public int SearchForTextMatchesCount(string text)
    {
        int textLength = text.Length;
        char startChar = text[0];
        List<Coordinate> positions = CharacterLookupDict[startChar];

        List<string> permutations = [];
        foreach (Coordinate position in positions)
        {
            int x = position.X;
            int y = position.Y;

            string[] results = new string[8];
            for (int k = 0; k < textLength; k++)
            {
                results[0] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x+k, y);
                results[1] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x+k, y+k);
                results[2] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x, y+k);
                results[3] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x-k, y+k);
                results[4] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x-k, y);
                results[5] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x-k, y-k);
                results[6] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x, y-k);
                results[7] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x+k, y-k);
            }
            permutations.AddRange(results);
        }

        return permutations.Count(x => x == text);
    }

    public int SearchForCrossTextMatchesCount(string text)
    {
        if (text.Length % 2 != 1)
        {
            //Can't cross something with an even length and not worth adding proper handling
            return -1;
        }

        int textLength = text.Length;
        int centreIndex = textLength/2;
        char centreChar = text[centreIndex];
        List<Coordinate> positions = CharacterLookupDict[centreChar];

        int matches = 0;
        foreach (Coordinate position in positions)
        {
            int x = position.X;
            int y = position.Y;

            string[] results = new string[2];
            for (int i = -centreIndex; i <= centreIndex; i++)
            {
                results[0] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x-i, y-i);
                results[1] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x-i, y+i);
            }

            if ((results[0] == results[1] || results[0] == Reverse(results[1])) && (results[0] == text || Reverse(results[0]) == text))
            {
                matches++;
            }
        }

        return matches;
    }

    private static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
