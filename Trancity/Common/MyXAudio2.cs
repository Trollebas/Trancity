/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 18.08.2015
 * Time: 15:14
 * 
 * This file is part of ODE_Test project.
 * 
 */
using Engine.Sound;

namespace Common
{
    /// <summary>
    /// Base overlay for XAudio2/X3DAudio features
    /// </summary>
    public class MyXAudio2
    {
        public static SoundDevice Device;

        public static void Initialize(SoundDeviceType type)
        {
            Device = SoundDevice.CreateDevice(type);
        }

        public static void Free()
        {
            Device.Dispose();
        }
    }
}
