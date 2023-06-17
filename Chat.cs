using System;
using System.Net.NetworkInformation;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chat : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI chatText;
    [SerializeField] private Button btn;
    [SerializeField] private Canvas canvas;

    private static event Action OnMessageRecive;
    public static event Action<int> clientAuth;


    
    public override void OnStartClient()
    {
        
    }


    public override void OnStartServer()
    {
        Debug.Log("StartServer");
    }

    
    public override void OnStartAuthority()
    {
        Debug.Log("Start Auth");
        OnMessageRecive += MessageRecieve;
        clientAuth += HandleClientAuth;
        CmdSendPlayerID();
        canvas.gameObject.SetActive(true);
    }
    
    
    
    private void HandleClientAuth(int id)
    { 
        chatText.text += $"Player : {id}";
    }


    [Command]
    private void CmdSendChat(string message)
    {
        RpcNotifyClients(message);
    }

    [Command]
    private void CmdSendPlayerID()
    {
        RpcSendPlayerId(connectionToClient.connectionId);
    }

    [ClientRpc]
    private void RpcSendPlayerId(int id)
    {
     clientAuth?.Invoke(id);
    }
    
    [ClientRpc]
    private void RpcNotifyClients(string message)
    {
        Debug.Log("Rpc client");
      //  OnMessageRecive?.Invoke();
         var texts = FindObjectsOfType<TextMeshProUGUI>();
        if (texts!= null)
        {
            foreach (var textMeshProUGUI in texts)
            {
                textMeshProUGUI.text += "\n salam";
            }

            
        }
        //GetComponentInChildren<TextMeshProUGUI>().text += "\n salam";
        // chatText.text += "\n Salam";
    }


    [ClientCallback]
    public void SendMessage()
    {
        CmdSendChat("Salam");
    }

    private void MessageRecieve()
    {
        chatText.text += "\n Salam";
    }


   
}