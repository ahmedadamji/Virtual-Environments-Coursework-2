using UnityEngine;


public class AccessManager : MonoBehaviour
{
    public int playerNumber;

    //[HideInInspector] 
    public bool locked = true;
    //[HideInInspector] 
    public bool available = true;
    
    public bool shareable;

    //private PlayerSpawnManager playerSpawnManager;
    private Player player;

    private void Awake()
    {
        PlayerSpawnManager.OnGameStart += OnGameStart;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        //playerSpawnManager = FindObjectOfType<PlayerSpawnManager>();
        ChangeMaterials(PlayerSpawnManager.OthersMaterial);
    }

    void OnGameStart()
    {
        locked = false;
        
        if (playerNumber == player.PlayerNumber || shareable)
        {
            available = true;
        }
        else
        {
            available = false;
        }
        
        ChangeMaterials(FindObjectOfType<PlayerSpawnManager>().PlayerMaterials[playerNumber]);
        
        if (shareable)
        {
            ChangeMaterials(PlayerSpawnManager.SharedMaterial);
        }
    }
    
    private void ChangeMaterials(Material material)
    {
        var children = GetComponentsInChildren<MeshRenderer>();
        foreach (var rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (int j = 0; j < rend.materials.Length; j++) 
            { 
                mats[j] = material; 
            }
            rend.materials = mats;
        }

    }
}
