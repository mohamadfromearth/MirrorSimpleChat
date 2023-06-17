using System;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public static Action<int> onPlayerAddToServer;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        
    }
    
}