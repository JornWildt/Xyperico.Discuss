using System.Linq;
using Xyperico.Agres;
using Xyperico.Discuss.Forums;


namespace Xyperico.Discuss
{
  public static class ApplicationStarter
  {
    public static void Initialize()
    {
      Xyperico.Agres.ProtoBuf.SerializerSetup.RegisterIdentity(typeof(ForumId));

      var serializerTypes = 
        typeof(ForumId).Assembly.GetTypes()
        .Where(t => typeof(Identity<>).IsAssignableFrom(t) || typeof(IEvent).IsAssignableFrom(t))
        .Where(t => !t.IsAbstract);

      AbstractSerializer.RegisterKnownTypes(serializerTypes);
    }
  }
}
