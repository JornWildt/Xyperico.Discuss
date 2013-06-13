using System;
using Xyperico.Agres;


namespace Xyperico.Discuss.Forums
{
  public class ForumId : AbstractIdentity<Guid>
  {
    public ForumId()
      : base(Guid.NewGuid())
    {
    }
  }
}
