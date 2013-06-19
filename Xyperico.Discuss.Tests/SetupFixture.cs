using NUnit.Framework;
using Xyperico.Agres;
using Xyperico.Base;
using Xyperico.Discuss.Contract.Forums;
using Xyperico.Discuss.Contract.Forums.Commands;
using Xyperico.Discuss.Contract.Forums.Events;


namespace Xyperico.Discuss.Tests
{
  [SetUpFixture]
  public class SetupFixture
  {
    public static void Setup(IObjectContainer container)
    {
      // FIXME: automate this
      AbstractSerializer.RegisterKnownType(typeof(ForumId));
      AbstractSerializer.RegisterKnownType(typeof(CreateForumCommand));
      AbstractSerializer.RegisterKnownType(typeof(UpdateForumCommand));
      AbstractSerializer.RegisterKnownType(typeof(ForumCreatedEvent));
      AbstractSerializer.RegisterKnownType(typeof(ForumUpdatedEvent));
    }


    [SetUp]
    public void TestSetup()
    {
      Xyperico.Base.Testing.TestHelper.ClearObjectContainer();
      Setup(Xyperico.Base.Testing.TestHelper.ObjectContainer);
    }
  }
}
