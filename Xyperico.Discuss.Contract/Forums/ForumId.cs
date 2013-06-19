using System;
using System.Runtime.Serialization;
using Xyperico.Agres.Contract;
using ProtoBuf;


namespace Xyperico.Discuss.Contract.Forums
{
  [Serializable]
  [DataContract]
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
