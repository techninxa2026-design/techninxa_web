namespace Techninxa.Services;
public static class UploadTypes
{
    public static string Extension(string contentType) => contentType.ToLowerInvariant() switch
    {
        "image/jpeg" => ".jpg", "image/png" => ".png", "image/webp" => ".webp",
        "video/mp4" => ".mp4", "video/webm" => ".webm", "video/quicktime" => ".mov",
        _ => throw new ArgumentException("Unsupported media type.")
    };
    public static bool IsManagedTeamImage(string? path) => path != null &&
        System.Text.RegularExpressions.Regex.IsMatch(path, @"^/uploads/teams/[a-fA-F0-9]{32}\.(jpg|jpeg|png|webp)$");
}
