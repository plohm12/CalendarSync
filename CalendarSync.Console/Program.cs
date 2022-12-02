using CalendarSync;
using CalendarSync.Console;
using System.Text.Json;

var fileService = new LocalFileService();
await fileService.ReadFileAsync();

var authService = new AuthService();

var tokens = await authService.GetToken(fileService.Tokens.ClientId, fileService.Tokens.RefreshToken);
var graph = new GraphService(tokens.AccessToken);
fileService.Tokens.RefreshToken = tokens.RefreshToken;
await fileService.WriteFileAsync();

var cEvent = await graph.GetEventByIdAsync();
Console.WriteLine(JsonSerializer.Serialize(cEvent));
