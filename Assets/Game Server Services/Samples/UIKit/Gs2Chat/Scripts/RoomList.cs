using Gs2.Unity.Gs2Chat.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Chat;
using UnityEngine;

namespace Gs2.Unity.UiKit.Sample.Gs2Chat
{
    public partial class RoomList : MonoBehaviour
    {
        public void OnCreateRoom(
            Namespace @namespace,
            string roomName
        )
        {
            var room = Instantiate(roomPrefab, populateNode);
            room.Set(@namespace, roomName);
            room.gameObject.SetActive(true);
        }
    }
    
    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class RoomList
    {
        public Gs2ChatRoomPrefab roomPrefab;
        public Transform populateNode;
    }

}