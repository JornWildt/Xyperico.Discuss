using Xyperico.Agres.EventStore;
using Xyperico.Agres.MessageBus;
using Xyperico.Discuss.Forums.Commands;
using System;


namespace Xyperico.Discuss.Forums
{
  public class ForumApplicationService : 
    GenericApplicationService<Forum, ForumId>,
    IHandleMessage<CreateForumCommand>,
    IHandleMessage<UpdateForumCommand>
  {
    public ForumApplicationService(IEventStore eventStore)
      : base(eventStore)
    {
    }


    public void Handle(CreateForumCommand cmd)
    {
      Console.WriteLine("Received CreateForumCommand");
      Update(cmd, f => f.Create(cmd));
    }
    
    
    public void Handle(UpdateForumCommand cmd)
    {
      Update(cmd, f => f.Update(cmd));
    }
  }
}
