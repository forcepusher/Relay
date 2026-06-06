namespace BananaParty.WebSocketRelay
{
    public class ObjectNode : IObjectNode
    {
        public string Name { get; private set; }

        private readonly IObjectNode[] _childObjectNodes;

        public ObjectNode(string name, params IObjectNode[] childObjectNodes)
        {
            Name = name;
            _childObjectNodes = childObjectNodes;
        }

        public IObjectNode[] GetNodes() => _childObjectNodes;
    }
}
