using System;
using CuttingEdge.Conditions;
using Xyperico.Agres.Contract;


namespace Xyperico.Discuss.Contract.Forums.Events
{
  [Serializable]
  public class ForumCreatedEvent : IEvent
  {
    public ForumId Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

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
