using Pathfinding;
using UnityEngine;

public class RotateToVelocity : MonoBehaviour
{
    public AIPath aIPath;
    Vector2 direction;

    void Start()
    {
        aIPath = GetComponent<AIPath>();
    }

    void Update()
    {
        direction = aIPath.desiredVelocity;
        transform.right = direction;
    }
}
