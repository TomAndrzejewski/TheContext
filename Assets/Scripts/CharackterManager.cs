using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CharackterManager : MonoBehaviour
{
    public static CharackterManager instance;

    public GameObject ActivePlayer { get; private set; }
    public UnityEvent OnAllPlayersDead;
    private PlayerController[] playerControllers;
    private int activePlayerIndex;
    private bool started;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera vcamera;

    private void Awake()
    {
        if (instance)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        playerControllers = FindObjectsOfType<PlayerController>();
        ActivePlayer = playerControllers[activePlayerIndex].gameObject;
        SetCanMove(false);
        foreach(var pc in playerControllers)
        {
            pc.OnDead += OnPlayerDead;
        }
    }

    private void OnDestroy()
    {
        foreach (var pc in playerControllers)
        {
            pc.OnDead -= OnPlayerDead;
        }
    }

    private void Update()
    {
        if (started && Input.GetKeyDown(KeyCode.E))
            ChangeActivePlayer();
    }

    public void OnZombiakiArrived()
    {
        ChangeActivePlayer();
        SetCanMove(true);
        started = true;
        EnemySpawner.instance.BeginSpawningEnemies();
    }

    private void ChangeActivePlayer()
    {
        Debug.Log(playerControllers.Length);
        Debug.Log(activePlayerIndex);

        ActivePlayer.SetActive(false);
        activePlayerIndex++;

        if (activePlayerIndex >= playerControllers.Length)
            activePlayerIndex = 0;

        ActivePlayer = playerControllers[activePlayerIndex].gameObject;
        ActivePlayer.SetActive(true);

        vcamera.Follow = ActivePlayer.transform;
    }

    private void SetCanMove(bool canMove)
    {
        foreach (var player in playerControllers)
            player.canMove = canMove;
    }

    public Transform GetPlayerTransform()
    {
        return ActivePlayer.transform;
    }

    private void OnPlayerDead()
    {
        if(!playerControllers.Any(pc => !pc.IsDead))
        {
            OnAllPlayersDead.Invoke();
            started = false;
        }
    }

}
