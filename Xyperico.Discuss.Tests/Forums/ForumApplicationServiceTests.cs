using NUnit.Framework;
using Xyperico.Agres;
using Xyperico.Agres.ProtoBuf;
using Xyperico.Agres.Sql;
using Xyperico.Discuss.Forums;
using Xyperico.Discuss.Forums.Commands;
using System;


namespace Xyperico.Discuss.Tests.Forums
{
  [TestFixture]
  public class ForumApplicationServiceTests : TestHelper
  {
    IEventStore Store;
    GenericRepository<Forum, ForumId> Repository;
    IAppendOnlyStore AppendOnlyStore;
    ForumApplicationService Service;

    
    protected override void SetUp()
    {
      base.SetUp();
      AppendOnlyStore = new SQLiteAppendOnlyStore(SetupFixture.SqlConnectionString, false);
      ISerializer serializer = new ProtoBufSerializer();
      Store = new EventStore(AppendOnlyStore, serializer);
      Repository = new GenericRepository<Forum, ForumId>(Store);
      Service = new ForumApplicationService(Store);
    }


    protected override void TearDown()
    {
      AppendOnlyStore.Dispose();
      base.TearDown();
    }


    [Test]
    public void CanCreateAndGetForum()
    {
      // Act
      CreateForumCommand cmd = new CreateForumCommand(new ForumId(), "Forum 1", "Blah blah");
      Service.Handle(cmd);

      Forum f = Repository.Get(cmd.Id).Aggregate;

      // Assert
      Assert.AreEqual(cmd.Id, f.Id);
      Assert.AreEqual(cmd.Title, f.Title);
      Assert.AreEqual(cmd.Description, f.Description);
    }


    [Test]
    public void CanUpdateForum()
    {
      // Arrange
      CreateForumCommand cmd = new CreateForumCommand(new ForumId(), "Forum 1", "Blah blah");
      Service.Handle(cmd);

      Forum f2 = Repository.Get(cmd.Id).Aggregate;

      // Act
      UpdateForumCommand cmd2 = new UpdateForumCommand(cmd.Id, "Forum A", "Oh well");
      Service.Handle(cmd2);

      Forum f3 = Repository.Get(cmd.Id).Aggregate;

      // Assert
      Assert.AreEqual(cmd.Id, f3.Id);
      Assert.AreEqual("Forum A", f3.Title);
      Assert.AreEqual("Oh well", f3.Description);
    }


    [Test]
    public void ItCannotCreateAlreadyCreatedForum()
    {
      // Arrange
      CreateForumCommand c = new CreateForumCommand(new ForumId(), "Hello", "Blah");
      Service.Handle(c);

      // Act + Assert
      AssertThrows<DomainException>(
        () => Service.Handle(c),
        ex => { Assert.AreEqual("Recreated", ex.Name); });
    }


    [Test]
    public void ItCannotUpdateNonExistingForum()
    {
      // Arrange
      UpdateForumCommand c = new UpdateForumCommand(new ForumId(), "Hello", "Blah");

      // Act + Assert
      AssertThrows<DomainException>(
        () => Service.Handle(c),
        ex => { Assert.AreEqual("NotCreated", ex.Name); });
    }
  }
}
