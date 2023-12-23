using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public Health playerHalth;
    [SerializeField] private Image totalHeaalthBar;
    [SerializeField] private Image currentHeaalthBar;

    void Start()
    {
        totalHeaalthBar.fillAmount = playerHalth.currentHealth / 10;
    }
    void Update()
    {
        currentHeaalthBar.fillAmount = playerHalth.currentHealth / 10;
    }
}
