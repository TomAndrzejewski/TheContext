using System;
using System.Collections;
using UnityEngine;


public class HP : MonoBehaviour
{
    [field:SerializeField]
    public int MaxHp { get; private set; } = 10;
    [SerializeField]
    private int cooldown = 1;
    private bool canBeDamaged = true;

    public event Action OnDamageTaken;

    private int currentHp;
    public int CurrentHP 
    {
        get
        {
            return currentHp;
        }
        private set
        {
            currentHp = Mathf.Clamp(value, 0, MaxHp);
        }
    }

    private void Start()
    {
        currentHp = MaxHp;
    }

    private void OnEnable()
    {
        canBeDamaged = true;
    }

    public void TakeDamage(int damage)
    {
        if (!canBeDamaged)
            return;
        CurrentHP -= damage;
        OnDamageTaken.Invoke();
        StartCoroutine(nameof(CooldownCoroutine));
    }

    private IEnumerator CooldownCoroutine()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(cooldown);
        canBeDamaged = true;
    }
    
 

}
