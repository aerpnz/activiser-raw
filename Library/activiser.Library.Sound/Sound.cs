using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace activiser.Library
{
    public sealed class Sound
    {
        private sealed class NativeMethods
        {
            [DllImport("CoreDll.dll", EntryPoint = "PlaySound")]
            internal static extern int WCE_PlaySound(string szSound, IntPtr hMod, int flags);

            [DllImport("CoreDll.dll", EntryPoint = "PlaySound")]
            internal static extern int WCE_PlaySoundBytes(byte[] szSound, IntPtr hMod, int flags);
        }

        private byte[] m_soundBytes;
        private string m_fileName;

        private enum Flags
        {
            SND_SYNC = 0,
            // play synchronously (default) 
            SND_ASYNC = 1,
            // play asynchronously 
            SND_NODEFAULT = 2,
            // silence (!default) if sound not found 
            SND_MEMORY = 4,
            // pszSound points to a memory file 
            SND_LOOP = 8,
            // loop the sound until next sndPlaySound 
            SND_NOSTOP = 16,
            // don't stop any currently playing sound 
            SND_NOWAIT = 8192,
            // don't wait if the driver is busy 
            SND_ALIAS = 65536,
            // name is a registry alias 
            SND_ALIAS_ID = 1114112,
            // alias is a predefined ID 
            SND_FILENAME = 131072,
            // name is file name 
            SND_RESOURCE = 262148
            // name is resource name or atom 
        }


        public Sound(string fileName) // Construct the Sound object to play sound data from the specified file. 
        {
            m_fileName = fileName;
        }

        public Sound(Stream stream) // Construct the Sound object to play sound data from the specified stream. 
        {
            if (stream == null) throw new ArgumentNullException("stream");
            m_soundBytes = new byte[(int)stream.Length + 1];
            stream.Read(m_soundBytes, 0, (int)stream.Length);
        }

        public void Play()
        {
            if (m_fileName != null)
                NativeMethods.WCE_PlaySound(m_fileName, IntPtr.Zero, (int) (Flags.SND_ASYNC | Flags.SND_FILENAME));
            else
                NativeMethods.WCE_PlaySoundBytes(m_soundBytes, IntPtr.Zero, (int)(Flags.SND_ASYNC | Flags.SND_MEMORY));
        }

        public static void Play(string filename)
        {
            Sound s = new Sound(filename);
            s.Play();
        }

        public static void Beep()
        {
            Sound s = new Sound(@"\Windows\Default");
            s.Play();
        }
    } 
}
