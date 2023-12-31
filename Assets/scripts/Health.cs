using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth = 3;
    // public float currentHealth { get; private set}
    public float currentHealth;
    public bool live;
    private float maxHealth = 3;
    public GameObject DieScreen;

    private void Update()
    {
        if (currentHealth > 0)
        {
            live = true;
            DieScreen.SetActive(false);
        }

        else
        {
            DieScreen.SetActive(true);
            live = false;
        }
    }
    private void Awake()
    {
        currentHealth = startingHealth;
    }
    public void addHeath(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public bool isHealthMax()
    {
        return currentHealth == maxHealth;
    }
}
