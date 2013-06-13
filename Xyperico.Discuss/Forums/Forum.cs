﻿using System;
using Xyperico.Base;
using Xyperico.Agres;
using System.Collections.Generic;
using Xyperico.Discuss.Forums.Events;
using CuttingEdge.Conditions;
using Xyperico.Discuss.Forums.Commands;


namespace Xyperico.Discuss.Forums
{
  public class Forum : AbstractAggregate<ForumId>
  {
    public override ForumId Id { get; protected set; }

    public string Title { get; protected set; }

    public string Description { get; protected set; }


    public Forum()
    {
    }

    /// <summary>
    /// Constructor for dehydrating from events.
    /// </summary>
    /// <param name="events"></param>
    public Forum(IEnumerable<IEvent> events)
    {
      Condition.Requires(events, "events").IsNotNull();

      Mutate(events);
    }


    /// <summary>
    /// Domain specific constructor.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="description"></param>
    public Forum(CreateForumCommand cmd)
    {
      Condition.Requires(cmd, "cmd").IsNotNull();

      Publish(new ForumCreatedEvent(cmd.Id, cmd.Title, cmd.Description));
    }


    public void Update(UpdateForumCommand cmd)
    {
      Condition.Requires(cmd, "cmd").IsNotNull();

      Publish(new ForumUpdatedEvent(cmd.Id, cmd.Title, cmd.Description));
    }


    #region Restore state

    protected void RestoreFrom(ForumCreatedEvent e)
    {
      Id = e.Id;
      Title = e.Title;
      Description = e.Description;
    }


    protected void RestoreFrom(ForumUpdatedEvent e)
    {
      Title = e.Title;
      Description = e.Description;
    }

    #endregion
  }
}
