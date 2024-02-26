using System.Runtime.InteropServices;

namespace Freddy;

public class Essentia
{
    // Declare the external functions from the C++ DLL
    [DllImport("libFreddysEssentia.so")]
    private static extern void essentia_init();

    [DllImport("libFreddysEssentia.so")]
    private static extern void essentia_shutdown();

    [DllImport("libFreddysEssentia.so")]
    private static extern void essentia_load_audio(string audioFilename, List<double> audioBuffer);

    public static List<Double> getAudio()
    {
        List<Double> buffer = new List<double>();
        
        // Call the C++ function from C#
        essentia_init();
        essentia_load_audio("/app/audio/example_audio_file.mp3", buffer);
        essentia_shutdown();

        return buffer;
    }
}