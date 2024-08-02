namespace Common
{
//    using Microsoft.DirectX.DirectSound;
    using SlimDX.DirectSound;
    using System;
    using System.Windows.Forms;
    using System.Collections;
	using System.Collections.Generic;

    public class MyDirectSound
    {
        public static DirectSound device;//Device
        public static PrimarySoundBuffer mainbuffer;
        public static SoundListener3D listner;
        private static SecondarySoundBuffer sb;
        private static SoundBuffer3D buff3d;

        public static void Free()
        {
            if (device != null)
            {
                device.Dispose();
                device = null;
            }
        }

        public static bool Initialize(Control control)
        {
            try
            {
            	var collection = DirectSound.GetDevices();
            	device = new DirectSound(collection[1].DriverGuid);//Device();
            	Logging.Write("DirectSound", "Uses device " + collection[1].Description);
                device.SetCooperativeLevel(control.Handle, CooperativeLevel.Priority);
                SoundBufferDescription desc = new SoundBufferDescription();
                desc.Flags = BufferFlags.PrimaryBuffer | BufferFlags.ControlVolume | BufferFlags.Control3D;
                mainbuffer = new PrimarySoundBuffer(device, desc);
//                listner = new SoundListener3D(mainbuffer);
                mainbuffer.Play(0, PlayFlags.Looping);
                //
            }
            catch (Exception exc)
            {
            	Logging.WriteExc(exc);
                return false;
            }
            return true;
        }
        
        public static void TestInt()
        {
                listner.Position = new SlimDX.Vector3(-2.0f, 0.0f, 0.0f);
                listner.Velocity = SlimDX.Vector3.Zero;
                listner.TopOrientation = new SlimDX.Vector3(0.0f, 1.0f, 0.0f);
                listner.FrontOrientation = new SlimDX.Vector3(1.0f, 0.0f, 0.0f);
                listner.CommitDeferredSettings();
                SoundBufferDescription _desc = new SoundBufferDescription();
                var loader = new SoundLoader("Sound 1_.wav");
				_desc.SizeInBytes = loader.OutBytes.Length;
				_desc.Format = loader.Format;
				_desc.AlgorithmFor3D = DirectSound3DAlgorithmGuid.FullHrt3DAlgorithm;
                _desc.Flags = BufferFlags.ControlVolume | BufferFlags.ControlFrequency | BufferFlags.Control3D;
                sb = new SlimDX.DirectSound.SecondarySoundBuffer(device, _desc);
                sb.Frequency = 100;
                sb.Volume = -1000;
                sb.Write(loader.OutBytes, 0, LockFlags.None);
                buff3d = new SoundBuffer3D(sb);
                sb.Play(0, PlayFlags.Hardware);
                buff3d.Mode = Mode3D.Normal;
                buff3d.MinDistance = 1.0f;
                buff3d.MaxDistance = 200.0f;
                buff3d.Position = SlimDX.Vector3.Zero;
                buff3d.Velocity = SlimDX.Vector3.Zero;
        }
    }
}

