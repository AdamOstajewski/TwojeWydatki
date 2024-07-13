namespace YourSpendings.Extensions
{
    public static class Parserer
    {
        public static int ParseToInt(this string? value)
        {
            var result = 0;

            if (int.TryParse(value, out var parsedValue))
            {
                result = parsedValue;
            }

            return result;
        }
    }
}
