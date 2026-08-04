namespace SimpleApi.Models
{
    /// <summary>
    /// Payload wejściowy do endpointa
    /// </summary>
    public record Request(
        string? Type,
        string? Content
    );
}