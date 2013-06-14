﻿using CuttingEdge.Conditions;
using Xyperico.Agres;
using Xyperico.Agres.Contract;


namespace Xyperico.Discuss.Contract.Forums.Commands
{
  public class CreateForumCommand : ICommand<ForumId>
  {
    public ForumId Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

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