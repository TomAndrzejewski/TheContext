using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AIDestinationSetter destination;

    private void Awake()
    {
        destination = gameObject.GetComponent<AIDestinationSetter>();
    }

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void UpdateTarget()
    {
        destination.target = CharackterManager.instance.GetPlayerTransform();
    }

	  private void OnParticleCollision(GameObject other)
	  {
        Die();
	  }

  public void Die()
	{
      Destroy(gameObject);
  }
}
