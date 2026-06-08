namespace BananaParty.WebSocketRelay
{
    public interface IBinaryState : IState
    {
        void WriteToBinary(BinaryWriteGraph binaryWriteGraph);
        void ReadFromBinary(BinaryReadGraph binaryReadGraph);
    }
}
