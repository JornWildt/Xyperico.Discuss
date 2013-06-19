using System;
using System.Runtime.Serialization;
using CuttingEdge.Conditions;
using Xyperico.Agres.Contract;
using ProtoBuf;


namespace Xyperico.Discuss.Contract.Forums.Events
{
  [Serializable] // FIXME: remove this
  [DataContract]
  [ProtoContract]
  public class ForumUpdatedEvent : IEvent
  {
    [DataMember]
    [ProtoMember(1)]
    public ForumId Id { get; private set; }

    [DataMember]
    [ProtoMember(2)]
    public string Title { get; private set; }
    
    [DataMember]
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
