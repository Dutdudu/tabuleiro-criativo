using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private string pieceSceneName = "escolha de peças";
    public static NetworkManager Instance;
    private bool wantsToJoinGame = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Importante: Habilita sincronização automática de cena
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        Debug.Log("Conectando ao servidor...");
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public void JoinQueue()
    {
        Debug.Log("JoinQueue chamado");
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Ainda não conectado. Aguardando conexão...");
            wantsToJoinGame = true;
            return;
        }
        
        TryJoinRoom();
    }

    private void TryJoinRoom()
    {
        Debug.Log("Tentando entrar em sala...");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado ao servidor Master!");
        
        if (wantsToJoinGame)
        {
            wantsToJoinGame = false;
            TryJoinRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Nenhuma sala disponível. Criando nova sala...");
        RoomOptions roomOptions = new RoomOptions 
        { 
            MaxPlayers = 2 
        };
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Entrou na sala! Jogadores: {PhotonNetwork.CurrentRoom.PlayerCount}/2");
        
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("Sala cheia! Iniciando jogo...");
            // Apenas o MasterClient carrega a nova cena
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(pieceSceneName);
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Novo jogador entrou! Total: {PhotonNetwork.CurrentRoom.PlayerCount}");
        
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("Sala cheia após entrada de jogador! Iniciando...");
            // Apenas o MasterClient carrega a nova cena
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(pieceSceneName);
            }
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log($"Novo Master Client: {newMasterClient.NickName}");
        // Se o MasterClient desconectar e a sala estiver cheia, o novo MasterClient carrega a cena
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel(pieceSceneName);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Desconectado do servidor: {cause}");
        wantsToJoinGame = false;
    }
}