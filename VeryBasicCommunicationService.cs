namespace WcfChatRoom
{
    using System.ServiceModel;

    public delegate void MessagePostHandler(string message);

    /// <summary>
    /// Interface for a very basic WCF service
    /// </summary>
    [ServiceContract]
    public interface IVeryBasicCommunicationService
    {
        /// <summary>
        /// A basic method that is used for transmition/receiving of messages
        /// </summary>
        /// <param name="message">The message</param>
        [OperationContract]
        void Transmit(string message);
    }

    /// <summary>
    /// A very basic WCF Service example
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class VeryBasicCommunicationService : IVeryBasicCommunicationService
    {
        /// <summary>
        /// Post Message Delegate Method
        /// </summary>
        private MessagePostHandler postMessageMethod { get; set; }

        public VeryBasicCommunicationService(MessagePostHandler callbackMethod)
        {
            this.postMessageMethod = callbackMethod;
        }

        /// <summary>
        /// A basic method that is used for transmition/receiving of messages
        /// </summary>
        /// <param name="message">The message</param>
        public void Transmit(string message)
        {
            if (this.postMessageMethod != null)
            {
                this.postMessageMethod(message);
            }
        }
    }
}
