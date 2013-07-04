using System;
using log4net;
using Xyperico.Agres;
using Xyperico.Agres.ProtoBuf;
using Xyperico.Agres.Sql;
using Xyperico.Discuss.Forums;
using Xyperico.Discuss.Forums.Commands;
using Xyperico.Discuss;
using Xyperico.Agres.DocumentStore;


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
      IEventPublisher publisher = new ConsolePublisher();
      IStreamSerializer streamSerializer = new DotNetBinaryStreamSerializer();
      IDocumentStoreFactory docStoreFactory = new FileDocumentStoreFactory("C:\\tmp\\Xyperico.Discuss.TestStorage", streamSerializer);

      EventStoreHost host = new EventStoreHost(eStore, publisher, docStoreFactory);
      host.Start();

      StartService(eStore);

      Console.WriteLine("Running");
      Console.ReadLine();
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
      }
    }
  }
}
