using System.Runtime.InteropServices;

namespace gehoorttest.application_Service;

public class AudioManager
{
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool Beep(uint dwFreq, uint dwDuration);

}