using System.Runtime.Serialization;
using CuttingEdge.Conditions;
using Xyperico.Agres;


namespace Xyperico.Discuss.Forums.Commands
{
  [DataContract]
  public class UpdateForumCommand : ICommand<ForumId>
  {
    [DataMember]
    public ForumId Id { get; private set; }

    [DataMember]
    public string Title { get; private set; }

    [DataMember]
    public string Description { get; private set; }

    
    public UpdateForumCommand() { }

    
    public UpdateForumCommand(ForumId id, string title, string description)
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
