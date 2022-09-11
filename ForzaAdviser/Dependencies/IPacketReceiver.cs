using System.Net;

namespace ForzaAdviser.Dependencies
{
    public interface IPacketReceiver
    {
        public int GeneratePort(string ipAddress);

        public void Dispose();
    }
}