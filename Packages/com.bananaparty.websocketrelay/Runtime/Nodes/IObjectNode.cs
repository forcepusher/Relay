using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public interface IObjectNode : INode
    {
        List<INode> GetNodes();
    }
}
