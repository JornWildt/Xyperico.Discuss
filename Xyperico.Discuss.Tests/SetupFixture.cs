using NUnit.Framework;
using Xyperico.Base;
using Xyperico.Agres.SQLite;


namespace Xyperico.Discuss.Tests
{
  [SetUpFixture]
  public class SetupFixture
  {
    public const string SqlConnectionString = "Data Source=AgresEventStore.db";

    public static void Setup(IObjectContainer container)
    {
      SQLiteAppendOnlyStore.CreateTableIfNotExists(SqlConnectionString);
      Xyperico.Discuss.ApplicationStarter.Initialize();
    }


    [SetUp]
    public void TestSetup()
    {
      Xyperico.Base.Testing.TestHelper.ClearObjectContainer();
      Setup(Xyperico.Base.Testing.TestHelper.ObjectContainer);
    }
  }
}
