using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    [SerializeField]
    private HP hP;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        hP.OnDamageTaken += OnPlayerHpChanged;
    }

    private void OnDestroy()
    {
        hP.OnDamageTaken -= OnPlayerHpChanged;
    }

    private void OnPlayerHpChanged()
    {
        image.fillAmount = (float)hP.CurrentHP / hP.MaxHp;
    }
}
