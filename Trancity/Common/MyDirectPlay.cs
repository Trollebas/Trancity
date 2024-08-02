/*namespace Common
{
    using Microsoft.DirectX.DirectPlay;
    using System;
    using System.Runtime.InteropServices;

    public class MyDirectPlay
    {
        public static bool connected;
        public static HostData[] found_hosts = new HostData[0];
        public static Guid guid;
        public static bool host_closed;
        public static object host_response;
        public static bool hosting;
        public static Address local_address;
        public static Peer peer;
        public static int port;
        public static int[] response_ranks;
        public static Type response_type;

        private static void Add_Found_Host(HostData host)
        {
            HostData[] dataArray = found_hosts;
            found_hosts = new HostData[dataArray.Length + 1];
            for (int i = 0; i < dataArray.Length; i++)
            {
                found_hosts[i] = dataArray[i];
            }
            found_hosts[dataArray.Length] = host;
        }

        public static bool Connect(int index)
        {
            if ((index < 0) || (index >= found_hosts.Length))
            {
                return false;
            }
            ApplicationDescription applicationDescription = new ApplicationDescription();
            applicationDescription.GuidInstance = found_hosts[index].description.GuidInstance;
            try
            {
                peer.Connect(applicationDescription, found_hosts[index].address, local_address, null, (ConnectFlags) 0);
                connected = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static NetworkPacket CreateNetworkPacket(object data)
        {
            NetworkPacket packet = new NetworkPacket();
            WriteDataToNetworkPacket(packet, data);
            return packet;
        }

        public static NetworkPacket CreateNetworkPacket(byte first_byte, params object[] data)
        {
            NetworkPacket packet = new NetworkPacket();
            packet.Write(first_byte);
            for (int i = 0; i < data.Length; i++)
            {
                WriteDataToNetworkPacket(packet, data[i]);
            }
            return packet;
        }

        public static bool FindHosts(string address, Type response_type, params int[] response_ranks)
        {
            MyDirectPlay.response_type = response_type;
            MyDirectPlay.response_ranks = response_ranks;
            found_hosts = new HostData[0];
            Address hostAddress = new Address(address, port);
            hostAddress.ServiceProvider = Address.ServiceProviderTcpIp;
            ApplicationDescription applicationDescription = new ApplicationDescription();
            applicationDescription.GuidApplication = guid;
            try
            {
                peer.FindHosts(applicationDescription, hostAddress, local_address, null, 0, 0, 0, FindHostsFlags.Sync);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Free()
        {
            Terminate_Session();
            if (peer != null)
            {
                try
                {
                    peer.Dispose();
                }
                catch
                {
                }
                peer = null;
            }
        }

        public static bool Host(string session_name, string password, object response)
        {
            local_address = new Address("localhost", port);
            host_response = response;
            host_closed = false;
            ApplicationDescription applicationDescription = new ApplicationDescription();
            applicationDescription.GuidApplication = guid;
            applicationDescription.SessionName = session_name;
            if (password.Length > 0)
            {
                applicationDescription.Flags |= SessionFlags.RequirePassword;
                applicationDescription.Password = password;
            }
            try
            {
                peer.Host(applicationDescription, local_address);
                hosting = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Initialize(Guid guid, int port)
        {
            try
            {
                MyDirectPlay.guid = guid;
                MyDirectPlay.port = port;
                peer = new Peer();
                peer.FindHostQuery += new FindHostQueryEventHandler(MyDirectPlay.peer_FindHostQuery);
                peer.FindHostResponse += new FindHostResponseEventHandler(MyDirectPlay.peer_FindHostResponse);
                local_address = new Address();
                local_address.ServiceProvider = Address.ServiceProviderTcpIp;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void peer_FindHostQuery(object sender, FindHostQueryEventArgs e)
        {
            NetworkPacket data = CreateNetworkPacket(host_response);
            e.Message.SetResponseData(data);
            e.RejectMessage = host_closed;
        }

        private static void peer_FindHostResponse(object sender, FindHostResponseEventArgs e)
        {
            HostData host = new HostData();
            host.description = e.Message.ApplicationDescription;
            host.address = (Address) e.Message.AddressSender.Clone();
            host.response = ReadNetworkPacketData(e.Message.ResponseData, response_type, response_ranks);
            for (int i = 0; i < found_hosts.Length; i++)
            {
                if (found_hosts[i].description.GuidInstance == host.description.GuidInstance)
                {
                    return;
                }
            }
            Add_Found_Host(host);
        }

        public static object ReadNetworkPacketData(NetworkPacket packet, Type type, params int[] ranks)
        {
            try
            {
                if (type != null)
                {
                    if (type == typeof(string))
                    {
                        return packet.ReadString();
                    }
                    if (ranks.Length > 0)
                    {
                        return packet.Read(type, ranks);
                    }
                    return packet.Read(type);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static byte ReadNetworkPacketFirstByte(NetworkPacket packet)
        {
            try
            {
                return (byte) packet.Read(typeof(byte));
            }
            catch
            {
                return 0;
            }
        }

        public static bool Send(int player_id, SendFlags flags, byte first_byte, params object[] data)
        {
            try
            {
                peer.SendTo(player_id, CreateNetworkPacket(first_byte, data), 0, flags);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Terminate_Session()
        {
            if (hosting || connected)
            {
                try
                {
                    peer.TerminateSession(null);
                }
                catch
                {
                }
            }
            hosting = false;
            connected = false;
        }

        public static void WriteDataToNetworkPacket(NetworkPacket packet, object data)
        {
            if (data != null)
            {
                if (data is string)
                {
                    packet.Write((string) data);
                }
                if (data is ValueType)
                {
                    packet.Write((ValueType) data);
                }
                if (data is Array)
                {
                    packet.Write((Array) data);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HostData
        {
            public Address address;
            public ApplicationDescription description;
            public object response;
        }
    }
}
*/
