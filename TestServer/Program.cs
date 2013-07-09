using System;
using System.Messaging;
using log4net;
using Xyperico.Agres;
using Xyperico.Agres.DocumentStore;
using Xyperico.Agres.MessageBus;
using Xyperico.Agres.MSMQ;
using Xyperico.Agres.ProtoBuf;
using Xyperico.Agres.Sql;
using Xyperico.Discuss;
using Xyperico.Discuss.Forums;
using Xyperico.Discuss.Forums.Commands;
using Xyperico.Agres.JsonNet;
using Msmq = System.Messaging;


namespace TestServer
{
  class Program
  {
    const string SqlConnectionString = "Data Source=C:\\tmp\\Xyperico.Discuss.TestStorage\\Xyperico.Discuss.Tests.db";

    protected static readonly ILog Logger = LogManager.GetLogger((typeof(Program)));

    
    static void Main(string[] args)
    {
      StartLogging();

      ApplicationStarter.Initialize();

      SQLiteAppendOnlyStore.CreateTableIfNotExists(SqlConnectionString);
      IAppendOnlyStore aStore = new SQLiteAppendOnlyStore(SqlConnectionString);
      ISerializer serializer = new ProtoBufSerializer();
      IEventStore eStore = new EventStore(aStore, serializer);
      IEventPublisher publisher = new MultiEventPublisher();
      IDocumentSerializer streamSerializer = new DotNetBinaryDocumentSerializer();
      IDocumentStoreFactory docStoreFactory = new FileDocumentStoreFactory("C:\\tmp\\Xyperico.Discuss.TestStorage", streamSerializer);

      EventStoreHost host = new EventStoreHost(eStore, publisher, docStoreFactory);
      host.Start();

      ISerializer messageSerializer = new JsonNetSerializer();
      Msmq.IMessageFormatter messageFormater = new MSMQMessageFormatter(messageSerializer);
      using (MSMQMessageSource messageSource = new MSMQMessageSource(".\\private$\\comsite", messageFormater))
      {
        MessageBusHost busHost = new MessageBusHost(messageSource);
        busHost.Start();

        StartService(eStore);

        Console.WriteLine("Running");
        Console.ReadLine();
      }
    }


    static void StartLogging()
    {
      log4net.Config.XmlConfigurator.Configure();
      Logger.Info("******************************************************************");
      Logger.Info("Disucss test");
      Logger.Info("******************************************************************");
    }


    static void StartService(IEventStore eStore)
    {
      ForumApplicationService service = new ForumApplicationService(eStore);

      string inp = "";
      while (inp == "")
      {
        CreateForumCommand cc = new CreateForumCommand(new ForumId(), "Test 1", "Blah blah");
        service.Handle(cc);
        inp = Console.ReadLine();
        //TestMessaging();
      }
    }


    static void TestMessaging()
    {
      using (MessageQueue q = new MessageQueue(".\\private$\\comsite"))
      {
        //TestMessage body = new TestMessage { X = 10 };
        Msmq.Message m = new Msmq.Message("HEllo");
        m.Label = "HELLO";// body.GetType().ToString();
        m.Recoverable = true;
        
        //m.ResponseQueue
        q.Send(m);
      }
    }
  }


  [Serializable]
  public class TestMessage
  {
    public int X { get; set; }
  }
}
