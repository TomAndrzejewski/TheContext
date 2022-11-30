
using Pathfinding;
using UnityEngine;

public class ThiefController : MonoBehaviour
{
    public Transform target;
    public Transform bankEntrace;
    public Transform bankEntrace2;
    public Transform runPoint;
    private AIPath aiPath;
    public bool IsMoving { get; private set; }
    private int frameCounter;

    
    void Start()
    {
        aiPath = GetComponent<AIPath>();
    }
    void Update()
    {
        frameCounter++;
        if(IsMoving && aiPath.reachedDestination && frameCounter > 5)
        {
            IsMoving = false;
        }
    }

    public void GoToBankEntrace(bool otherOption)
    {
        var dest = otherOption ? bankEntrace2 : bankEntrace;
        SetNewTargetPosition(dest);
    }

    public void SetNewTargetPosition(Transform targetPosition)
    {
        target.position = targetPosition.position;
        IsMoving = true;
        frameCounter = 0;
    }

    public void Run()
    {
        SetNewTargetPosition(runPoint);
    }
   
}
