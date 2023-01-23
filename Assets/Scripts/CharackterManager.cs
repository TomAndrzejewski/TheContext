using UnityEngine;

public class CharackterManager : MonoBehaviour
{
    public GameObject ActivePlayer { get; private set; }
    private PlayerController[] playerControllers;
    private int activePlayerIndex;
    private bool started;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera vcamera;

    void Start()
    {
        playerControllers = FindObjectsOfType<PlayerController>();
        ActivePlayer = playerControllers[activePlayerIndex].gameObject;
        SetCanMove(false);
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

}
