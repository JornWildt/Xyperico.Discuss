using System;
using System.Runtime.Serialization;
using ProtoBuf;
using Xyperico.Agres;


namespace Xyperico.Discuss.Forums
{
  [ProtoContract]
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
