public static class StringExtensions
{
    public static string ToSnakeCase(this string input)
    {
        return string.Concat(
            input.Select((x, i) =>
                i > 0 && char.IsUpper(x)
                    ? "_" + x.ToString()
                    : x.ToString()
            )
        ).ToLower();
    }
}
