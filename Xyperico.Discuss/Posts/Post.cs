using System;
using Xyperico.Base;


namespace Xyperico.Discuss.Posts
{
  public class Post : IHaveId<Guid>
  {
    #region Persisted properties

    public Guid Id { get; protected set; }

    public Guid Forum_Id { get; protected set; }

    public string Subject { get; protected set; }

    public string Content { get; protected set; } // FIXME: rich media content editor here ...

    #endregion


    public Post()
    {
    }


    public Post(Guid forum_id, string subject, string content)
    {
    }
  }
}
