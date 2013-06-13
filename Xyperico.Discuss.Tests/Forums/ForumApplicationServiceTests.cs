using NUnit.Framework;
using Xyperico.Agres;
using Xyperico.Agres.InMemoryEventStore;
using Xyperico.Discuss.Forums;
using Xyperico.Discuss.Forums.Commands;
using Xyperico.Base.Exceptions;


namespace Xyperico.Discuss.Tests.Forums
{
  [TestFixture]
  public class ForumApplicationServiceTests : TestHelper
  {
    IEventStore Store;
    AbstractRepository<Forum, ForumId> Repository;
    ForumApplicationService Service;

    
    protected override void SetUp()
    {
      base.SetUp();
      Store = new InMemoryEventStore();
      Repository = new AbstractRepository<Forum, ForumId>(Store);
      Service = new ForumApplicationService(Store);
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
    public void WhenSendingCommandToNonExistingForumItThrowsMissingResourceException()
    {
      // Arrange
      UpdateForumCommand cmd = new UpdateForumCommand(new ForumId(), "Forum A", "Oh well");

      // Act + Assert
      AssertThrows<MissingResourceException>(() => Service.Handle(cmd));
      
    }
  }
}
