using Xyperico.Agres;
using Xyperico.Agres.MSMQ;
using Xyperico.Agres.JsonNet;
using Xyperico.Agres.DocumentStore;
using Msmq = System.Messaging;


namespace TestServer
{
  public class MultiEventPublisher : IEventPublisher
  {
    Log4NetPublisher Log4NetPublisher;
    ConsolePublisher ConsolePublisher;
    MSMQEventPublisher MSMQPublisher;

    public MultiEventPublisher()
    {
      Log4NetPublisher = new Log4NetPublisher();
      ConsolePublisher = new ConsolePublisher();
      ISerializer messageSerializer = new JsonNetSerializer();
      Msmq.IMessageFormatter messageFormater = new MSMQMessageFormatter(messageSerializer);
      MSMQPublisher = new MSMQEventPublisher(".\\private$\\comsite", messageFormater);
    }

    public void Publish(IEvent e)
    {
      Log4NetPublisher.Publish(e);
      ConsolePublisher.Publish(e);
      MSMQPublisher.Publish(e);
    }
  }
}
