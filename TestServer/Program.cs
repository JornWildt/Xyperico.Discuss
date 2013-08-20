using System;
using System.Linq;
using System.Messaging;
using System.Reflection;
using log4net;
using Xyperico.Agres;
using Xyperico.Agres.DocumentStore;
using Xyperico.Agres.EventStore;
using Xyperico.Agres.JsonNet;
using Xyperico.Agres.MessageBus;
using Xyperico.Agres.MSMQ;
using Xyperico.Agres.ProtoBuf;
using Xyperico.Agres.Serialization;
using Xyperico.Agres.SQLite;
using Xyperico.Discuss;
using Xyperico.Discuss.Forums;
using Xyperico.Discuss.Forums.Commands;
using Xyperico.Agres.Configuration;
using Msmq = System.Messaging;


namespace TestServer
{
  class Program
  {
    const string SqlConnectionString = "Data Source=C:\\tmp\\Xyperico.Discuss.TestStorage\\Xyperico.Discuss.Tests.db";

    protected static readonly ILog Logger = LogManager.GetLogger((typeof(Program)));


    static void Main(string[] args)
    {
      var serializerTypes =
        typeof(ForumId).Assembly.GetTypes()
        .Where(t => typeof(Identity<>).IsAssignableFrom(t) || typeof(IMessage).IsAssignableFrom(t))
        .Where(t => !t.IsAbstract);

      Assembly[] handlerAssemblies = new Assembly[] { typeof(ForumApplicationService).Assembly };

      IMessageBus bus = Configure.With()
        .Log4Net()
        .ObjectContainer(Xyperico.Base.ObjectContainer.Container)
        .SerializableTypes(serializerTypes)
        .MessageBus(handlerAssemblies)
          .WithJsonSubscriptionSerializer()
          //.WithProtoBufSubscriptionSerializer()
          .WithFileSubscriptionStore("C:\\tmp\\Xyperico.Discuss.TestStorage")
          //.WithJsonMessageSerializer()
          .WithProtoBufMessageSerializer()
          .WithMSMQ(".\\private$\\comsite")
          .Done()
        .EventStore()
          .WithSQLiteEventStore(SqlConnectionString, true)
          .WithJsonEventSerializer()
          .WithJsonDocumentSerializer()
          .WithFileDocumentStore("C:\\tmp\\Xyperico.Discuss.TestStorage")
          .Done()
        .Start();

      do
      {
        CreateForumCommand cmd = new CreateForumCommand(new ForumId(), "Test forum", "Blah blah");
        bus.Send(cmd);
      }
      while (Console.ReadLine() == "");
    }
  }


  [Serializable]
  public class TestMessage
  {
    public int X { get; set; }
  }
}
