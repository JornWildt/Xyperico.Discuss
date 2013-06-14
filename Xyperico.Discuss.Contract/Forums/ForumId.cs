using System;
using Xyperico.Agres;
using Xyperico.Agres.Contract;


namespace Xyperico.Discuss.Contract.Forums
{
  public class ForumId : Identity<Guid>
  {
    public ForumId()
      : base(Guid.NewGuid())
    {
    }


    protected override string Prefix
    {
      get { return ""; }
    }
  }
}
