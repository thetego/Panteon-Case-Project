using TMPro;
using UnityEngine;

public class HealthUIView : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    
    private int _initialHealth;

    void Start()
    {
        _initialHealth = GetComponent<Building>().Health;
        _healthText.text = _initialHealth.ToString()+"/"+_initialHealth.ToString();
    }


    public void UpdateHealthText(int health)
    {
        _healthText.text = health.ToString()+"/"+_initialHealth.ToString();
    }

}
