using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerSetup))]
public class PlayerManager  : Photon.PunBehaviour, IPunObservable {
    //: NetworkBehaviour
    //[SyncVar]
   

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    [SerializeField]
    private int maxHealth = 100;

    //[SyncVar]
    private int currentHealth;
    /* //Verificar 
    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    [SerializeField]
    private GameObject[] disableGameObjectsOnDeath;

    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private GameObject spawnEffect;

    private bool firstSetup = true;
    */

    public void Awake()
    {
       PlayerControler playerInputs =  gameObject.GetComponent<PlayerControler>();
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instanciation when levels are synchronized
        if (photonView.isMine)
        {
            LocalPlayerInstance = gameObject;

            playerInputs.ProcessInputs();
        }
        else
        {
            playerInputs.DisableInputs();
        }

        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(gameObject);
    }

    //chamado quando o "player setup" está pronto
    public void SetupPlayer()
    {/*
        if (isLocalPlayer)
        {
            //trocando cameras
            GameManager.instance.SetSceneCameraActive(false);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);
        }
       
        
        CmdBroadCastNewPlayerSetup();*/
    }

   /* [Command]
    private void CmdBroadCastNewPlayerSetup()
    {
        //RpcSetupPlayerOnAllClients();
    }*/
    /*
    [ClientRpc]
    private void RpcSetupPlayerOnAllClients()
    {
        if (firstSetup)
        {
            wasEnabled = new bool[disableOnDeath.Length];
            for (int i = 0; i < wasEnabled.Length; i++)
            {
                wasEnabled[i] = disableOnDeath[i].enabled;
            }

            firstSetup = false;
        }
      


        SetDefaults();
    }*/

    void Update()
    {/*
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(9999);
        }*/
    }///Teste

    /* [ClientRpc]
     public void RpcTakeDamage(int _amount)
     {
         if (isDead)
             return;

         currentHealth -= _amount;

         Debug.Log(transform.name + " agora tem " + currentHealth + " de vida.");

         if (currentHealth <= 0)
         {
             Die();
         }
}*/

    private void Die()
    {
        //Verificar depois
        /*
        isDead = true;
        //disable components here (Desativando os componentes aqui)
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false; 
        }
        //Desativando Game Objects 
        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive (false);
        }

        //Desativando Blocos de Colisão
        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false;

        //Efeito de morte (Explosão)

        GameObject _gfxIns = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(_gfxIns, 3f);

        //trocando cameras
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(true);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(false);
        }



        Debug.Log(transform.name + " tá morto vei!");

        // Call Respawn
        Invoke("Respawn", GameManager.instance.matchSettings.respawnTime);
         */
    }


    private void Respawn()
    {/*
        
        Transform _spawnpoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnpoint.position;
        transform.rotation = _spawnpoint.rotation;

        

        SetupPlayer();
        
        Debug.Log(transform.name + " respawned");*/
    }

    //resetando tudo
    public void SetDefaults()
    {/*
        isDead = false;
        currentHealth = maxHealth;
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        //Ativando graficos denovo
        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(true);
        }
        //Ativando blocos de colisão
        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;



        //Criando efeito Spawn
        GameObject _gfxIns = (GameObject)Instantiate(spawnEffect, transform.position, Quaternion.identity);
        Destroy(_gfxIns, 3f);*/
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            //stream.SendNext(this.IsFiring);
            stream.SendNext(this.currentHealth);
        }
        else
        {
            // Network player, receive data
           // this.IsFiring = (bool)stream.ReceiveNext();
            this.currentHealth = (int)stream.ReceiveNext();
        }

        
    }
}
