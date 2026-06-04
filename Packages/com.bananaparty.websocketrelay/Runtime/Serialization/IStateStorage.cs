using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public interface IStateStorage
    {
        void WriteInt(string key, int value);
        int ReadInt(string key);
        bool HasInt(string key);

        void WriteFloat(string key, float value);
        float ReadFloat(string key);
        bool HasFloat(string key);
    }
}
