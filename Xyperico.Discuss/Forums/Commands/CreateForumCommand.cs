using System.Runtime.Serialization;
using CuttingEdge.Conditions;
using Xyperico.Agres;
using ProtoBuf;


namespace Xyperico.Discuss.Forums.Commands
{
  [ProtoContract]
  public class CreateForumCommand : ICommand<ForumId>
  {
    [ProtoMember(1)]
    public ForumId Id { get; set; }

    [ProtoMember(2)]
    public string Title { get; set; }

    [ProtoMember(3)]
    public string Description { get; set; }

    
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
