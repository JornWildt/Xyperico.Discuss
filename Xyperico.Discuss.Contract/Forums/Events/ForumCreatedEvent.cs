using System;
using System.Runtime.Serialization;
using CuttingEdge.Conditions;
using Xyperico.Agres.Contract;
using ProtoBuf;


namespace Xyperico.Discuss.Contract.Forums.Events
{
  [Serializable]
  [DataContract]
  [ProtoContract]
  public class ForumCreatedEvent : IEvent
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
