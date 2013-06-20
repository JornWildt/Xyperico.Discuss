using Xyperico.Agres;
using Xyperico.Discuss.Contract.Forums;
using Xyperico.Discuss.Contract.Forums.Commands;
using Xyperico.Discuss.Contract.Forums.Events;


namespace Xyperico.Discuss
{
  public static class ApplicationStarter
  {
    public static void Initialize()
    {
      Xyperico.Agres.ProtoBuf.Configuration.RegisterIdentity(typeof(ForumId));

      // FIXME: automate this
      AbstractSerializer.RegisterKnownType(typeof(ForumId));
      AbstractSerializer.RegisterKnownType(typeof(CreateForumCommand));
      AbstractSerializer.RegisterKnownType(typeof(UpdateForumCommand));
      AbstractSerializer.RegisterKnownType(typeof(ForumCreatedEvent));
      AbstractSerializer.RegisterKnownType(typeof(ForumUpdatedEvent));
    }
  }
}
