using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

public class PolicemanController : MonoBehaviour
{
    public AIPath aiPath;
    public bool IsMoving { get; private set; }
    private int frameCounter;

    public event UnityAction OnReachedDestination;

    private void Awake()
    {
        gameObject.SetActive(false);
        aiPath = GetComponent<AIPath>();
    }
    void Update()
    {
        frameCounter++;
        if (IsMoving && aiPath.reachedDestination && frameCounter > 5)
        {
            IsMoving = false;
            OnReachedDestination?.Invoke();
        }
    }
    public void EnterBank()
    {
        IsMoving = true;
        frameCounter = 0;
        gameObject.SetActive(true);
    }

}
