using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(PlayerControler))]
public class PlayerSetup : MonoBehaviour {
    //NetworkBehaviour
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    string dontDrawLayerName = "DontDraw";

    [SerializeField]
    GameObject playerGraphics;

    [SerializeField]
    GameObject playerUIPrefab;

    [HideInInspector]
    public GameObject playerUIInstance;

   
    void Start()
    {
       /* if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
       
            //Desativando graficos do jogador local
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));

            //Criando a Interface do Jogador
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

            // Configurando Interface do Jogador 
            PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();

            if (ui == null)
            {
                Debug.LogError("No PlayerUI component on Player UI prefab");
            }
            ui.SetController(GetComponent<PlayerControler>());
            GetComponent<Player>().SetupPlayer();
        }*/
        


    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {/*
        obj.layer = newLayer;

        foreach(Transform child in obj.transform)
        {   //recursiva pra fazer isso com todos os objetos filhos
            SetLayerRecursively(child.gameObject, newLayer);
        }*/
    }

   /* public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.RegisterPlayer(_netID, _player);
    }*/

    /*
            //dentificando jogadores 
            RegisterPlayer();

        }

        void RegisterPlayer()
        {    //Identificando jogadores
            string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
            transform.name = _ID;
        }*/

    void AssignRemoteLayer()
    {
       /* gameObject.layer = LayerMask.NameToLayer(remoteLayerName);*/
        
    }

    void DisableComponents()
    {
       /* for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }*/
    }

    void OnDisable()
    {/*
        Destroy(playerUIInstance);
        if (isLocalPlayer) 
            GameManager.instance.SetSceneCameraActive(true);

        GameManager.UnRegisterPlayer(transform.name);*/
    }
}
