using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TCPGameChatProject
{
    public static class PacketHelper
    {
        public enum PacketType
        {
            Text = 1,
            Image = 2
        }

        public static void SendPacket(NetworkStream stream, string message, PacketType type = PacketType.Text)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                byte[] header = new byte[5]; // 1 byte type + 4 byte length

                header[0] = (byte)type;
                BitConverter.GetBytes(data.Length).CopyTo(header, 1);

                stream.Write(header, 0, header.Length);
                stream.Write(data, 0, data.Length);
            }
            catch { }
        }

        public static (PacketType, string) ReceivePacket(NetworkStream stream)
        {
            try
            {
                byte[] header = ReadExact(stream, 5);
                if (header == null) return (0, null);

                PacketType type = (PacketType)header[0];
                int length = BitConverter.ToInt32(header, 1);
                if (length <= 0) return (0, null);

                byte[] data = ReadExact(stream, length);
                if (data == null) return (0, null);

                return (type, Encoding.UTF8.GetString(data));
            }
            catch
            {
                return (0, null);
            }
        }

        private static byte[] ReadExact(NetworkStream stream, int size)
        {
            byte[] buffer = new byte[size];
            int totalRead = 0;

            while (totalRead < size)
            {
                int bytesRead = stream.Read(buffer, totalRead, size - totalRead);
                if (bytesRead == 0) return null;
                totalRead += bytesRead;
            }

            return buffer;
        }
    }
}
