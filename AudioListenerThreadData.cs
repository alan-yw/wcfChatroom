namespace WcfChatRoom
{
    using System.Net;
    using NAudioStreaming;

    public class AudioListenerThreadData
    {
        public IPEndPoint EndPoint { get; set; }
        public INetworkChatCodec Codec { get; set; }
    }
}
