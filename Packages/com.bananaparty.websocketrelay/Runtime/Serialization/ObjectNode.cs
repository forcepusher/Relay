namespace BananaParty.WebSocketRelay
{
    public class ObjectNode : IObjectNode
    {
        public string Name { get; private set; }

        private readonly INode[] _childObjectNodes;

        public ObjectNode(string name, params INode[] childObjectNodes)
        {
            Name = name;
            _childObjectNodes = childObjectNodes;
        }

        public INode[] GetNodes() => _childObjectNodes;
    }
}
