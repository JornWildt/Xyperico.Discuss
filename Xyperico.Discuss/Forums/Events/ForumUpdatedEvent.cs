using CuttingEdge.Conditions;
using ProtoBuf;
using Xyperico.Agres;


namespace Xyperico.Discuss.Forums.Events
{
  [ProtoContract]
  public class ForumUpdatedEvent : IEvent
  {
    [ProtoMember(1)]
    public ForumId Id { get; private set; }

    [ProtoMember(2)]
    public string Title { get; private set; }
    
    [ProtoMember(3)]
    public string Description { get; private set; }

    
    public ForumUpdatedEvent() { }

    
    public ForumUpdatedEvent(ForumId id, string title, string description)
    {
      Condition.Requires(id, "id").IsNotNull();
      Condition.Requires(title, "title").IsNotNullOrEmpty();
      Condition.Requires(description, "description").IsNotNull();

      Id = id;
      Title = title;
      Description = description;
    }
  }
}
