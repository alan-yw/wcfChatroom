namespace WcfChatRoom
{
    using System.ServiceModel;

    public delegate void MessagePostHandler(string message);


    
    [ServiceContract]
    public interface IVeryBasicCommunicationService
    {
        

        [OperationContract]
        void Transmit(string message);
    }


  
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class VeryBasicCommunicationService : IVeryBasicCommunicationService
    {
        
      
        private MessagePostHandler postMessageMethod { get; set; }

        public VeryBasicCommunicationService(MessagePostHandler callbackMethod)
        {
            this.postMessageMethod = callbackMethod;
        }


        public void Transmit(string message)
        {
            if (this.postMessageMethod != null)
            {
                this.postMessageMethod(message);
            }
        }
    }
}
