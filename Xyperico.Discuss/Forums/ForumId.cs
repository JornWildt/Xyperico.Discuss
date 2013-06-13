using System;
using Xyperico.Agres;


namespace Xyperico.Discuss.Forums
{
  public class ForumId : Identity<Guid>
  {
    public ForumId()
      : base(Guid.NewGuid())
    {
    }
  }
}
