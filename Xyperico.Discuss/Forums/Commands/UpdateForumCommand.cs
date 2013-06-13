using CuttingEdge.Conditions;
using Xyperico.Agres;


namespace Xyperico.Discuss.Forums.Commands
{
  public class UpdateForumCommand : ICommand<ForumId>
  {
    public ForumId Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

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
