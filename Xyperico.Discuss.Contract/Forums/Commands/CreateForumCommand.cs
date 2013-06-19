using System.Runtime.Serialization;
using CuttingEdge.Conditions;
using Xyperico.Agres.Contract;


namespace Xyperico.Discuss.Contract.Forums.Commands
{
  [DataContract]
  public class CreateForumCommand : ICommand<ForumId>
  {
    [DataMember]
    public ForumId Id { get; private set; }
    
    [DataMember]
    public string Title { get; private set; }
    
    [DataMember]
    public string Description { get; private set; }

    public CreateForumCommand() { }

    public CreateForumCommand(ForumId id, string title, string description)
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
