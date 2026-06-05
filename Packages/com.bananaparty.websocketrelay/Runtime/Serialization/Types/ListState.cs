// This just doesn't work because only ISerializeableState is only building graph

// using System.Collections.Generic;

// namespace BananaParty.WebSocketRelay
// {
//     public class ListState<T> : IState where T : ISerializableState
//     {
//         public List<T> Value;
//         public readonly string Name;

//         public ListState(List<T> initialValue, string name = nameof(IntegerState))
//         {
//             Value = initialValue;
//             Name = name;
//         }

//         public void Serialize(IStateStream stateStream)
//         {
//             stateStream.WriteInt(Value.Count);
//             foreach (T item in Value)
//             {
//                 item.Serialize(stateStream);
//             }
//         }

//         public void Deserialize(IStateStream stateStream)
//         {
//             Value = stateStream.ReadInt();
//         }
//     }
// }
