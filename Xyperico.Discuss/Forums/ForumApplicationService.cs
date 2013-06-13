using Xyperico.Agres;
using Xyperico.Discuss.Forums.Commands;


namespace Xyperico.Discuss.Forums
{
  public class ForumApplicationService : GenericApplicationService<Forum, ForumId>
  {
    public ForumApplicationService(IEventStore eventStore)
      : base(eventStore)
    {
    }


    public void Handle(CreateForumCommand cmd)
    {
      Create(cmd, () => new Forum(cmd));
    }
    
    
    public void Handle(UpdateForumCommand cmd)
    {
      Update(cmd, f => f.Update(cmd));
    }
  }
}
