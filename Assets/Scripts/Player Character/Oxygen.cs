using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    public Image oxygenBar; // UI Image con Fill Radial
    public float maxOxygen = 5f; // Tiempo máximo de oxígeno en segundos
    private float currentOxygen;
    private PlayerMovement player;
    public Canvas oxygenCanvas; // Canvas del indicador de oxígeno

    void Start()
    {
        player = GetComponent<PlayerMovement>();
        currentOxygen = 0;
        UpdateOxygenUI();
        oxygenCanvas.gameObject.SetActive(false); // Se desactiva el canvas al inicio
    }

    void Update()
    {
        if (player.inWater)
        {
            oxygenCanvas.gameObject.SetActive(true); // Activa el canvas al entrar al agua
            currentOxygen += Time.deltaTime;
            if (currentOxygen >= maxOxygen)
            {
                player.Die();
                FindObjectOfType<DeadTransition>().StartShrink();
            }
        }
        else
        {
            currentOxygen = 0; // Reinicia el oxígeno al salir del agua
            oxygenCanvas.gameObject.SetActive(false); // Desactiva el canvas al salir del agua
        }

        UpdateOxygenUI();
    }

    void UpdateOxygenUI()
    {
        if (oxygenBar != null)
        {
            oxygenBar.fillAmount = (currentOxygen / maxOxygen);
        }
    }
}
