using System.Runtime.InteropServices;

namespace BusinessLogic;

public class AudioManager
{
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern void Beep(uint dwFreq, uint dwDuration);

    public static void PlaySound(int frequency, int duration)
    {
        Beep((uint)frequency, (uint)duration);
    }
}