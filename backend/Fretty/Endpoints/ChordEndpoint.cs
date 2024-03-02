namespace Fretty.Endpoints;

public class ChordEndpoint: IEndpoint
{
    private const string Path = "/chord";
    private static readonly string[] Chords = [
        "A", "B", "C", "D", "E", "F", "G"
    ];
    
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(Path, HandleChordRequest)
            .WithName("GetChord")
            .WithOpenApi();
    }
    
    private ChordResponse HandleChordRequest(HttpContext context)
    {
        var random = new Random();
        var index = random.Next(Chords.Length);
        var chord = new ChordResponse(Chords[index], Chords.ToArray());
        
        return chord;
    }
}

public record ChordResponse(string Chord, string[] Chords);