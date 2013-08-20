using System;
using Xyperico.Agres.EventStore;
using Xyperico.Agres.MessageBus;
using Xyperico.Discuss.Forums.Events;


namespace Xyperico.Discuss.Forums
{
  public class ForumEventTesterService :
    GenericApplicationService<Forum, ForumId>,
    IHandleMessage<ForumCreatedEvent>
  {
    public ForumEventTesterService(IEventStore eventStore)
      : base(eventStore)
    {
    }


    public void Handle(ForumCreatedEvent message)
    {
      Console.WriteLine("Handle ForumCreatedEvent.");
    }
  }
}
