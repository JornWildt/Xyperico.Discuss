using CuttingEdge.Conditions;
using ProtoBuf;
using Xyperico.Agres;


namespace Xyperico.Discuss.Forums.Events
{
  [ProtoContract]
  public class ForumCreatedEvent : IEvent
  {
    [ProtoMember(1)]
    public ForumId Id { get; set; }

    [ProtoMember(2)]
    public string Title { get; set; }

    [ProtoMember(3)]
    public string Description { get; set; }

    
    public ForumCreatedEvent() { }

    
    public ForumCreatedEvent(ForumId id, string title, string description)
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
