namespace SimpleApi.Models
{
    /// <summary>
    /// Odpowiedź endpointa
    /// </summary>
    public record Response(
        string Status,
        string Type,
        int RowCount,
        IEnumerable<Dictionary<string, object?>> Data);
}