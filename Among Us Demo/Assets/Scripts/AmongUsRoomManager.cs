using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AmongUsRoomManager : NetworkRoomManager
{
    public GameRuleData gameRuleData;

    public int minPlayerCount;
    public int imposterCount;



    // 서버에서 새로운 클라이언트를 감지했을때 동작하는 함수
    public override void OnRoomServerConnect(NetworkConnection conn)
    {
        base.OnRoomServerConnect(conn);

        

    }

}
