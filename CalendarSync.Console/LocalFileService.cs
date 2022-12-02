using System.Text.Json;

namespace CalendarSync.Console
{
    internal class LocalFileService
    {
        const string FilePath = @"";
        internal TokenFile Tokens { get; set; } = new TokenFile();

        internal async Task ReadFileAsync()
        {
            var content = await File.ReadAllTextAsync(FilePath);
            if (string.IsNullOrEmpty(content))
            {
                Tokens = new TokenFile();
                return;
            }
            Tokens = JsonSerializer.Deserialize<TokenFile>(content);
        }

        internal async Task WriteFileAsync()
        {
            var content = JsonSerializer.Serialize(Tokens);
            await File.WriteAllTextAsync(FilePath, content);
        }
    }
}
