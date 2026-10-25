using UnityEngine;
using TMPro; // Wymagane do obs³ugi tekstów TextMeshPro

public class UpgradeManager : MonoBehaviour
{
    public int playerPoints = 0;
    public TMP_Text pointsText; 

    void Start()
    {
        UpdatePointsUI();
    }

    public void AddPoints(int amount)
    {
        playerPoints += amount;
        UpdatePointsUI();
    }

    private void UpdatePointsUI()
    {
        if (pointsText != null)
        {
            pointsText.text = "Twoje punkty: " + playerPoints;
        }
    }

    // Funkcja podpinana pod przycisk "Wiêksze obra¿enia"
    public void BuyDamageUpgrade()
    {
        int cost = 10;
        if (playerPoints >= cost)
        {
            playerPoints -= cost;
            UpdatePointsUI();
            Debug.Log("Kupiono ulepszenie: Wiêksze obra¿enia!");
            // W przysz³oœci przeka¿emy st¹d informacjê do statku gracza
        }
        else
        {
            Debug.Log("Za ma³o punktów na to ulepszenie!");
        }
    }

    // Przycisk zamykaj¹cy sklep i wznawiaj¹cy grê
    public void CloseShop()
    {
        gameObject.SetActive(false); // Ukrywa panel
        Debug.Log("Sklep zamkniêty. Zaczynamy kolejn¹ falê!");
    }
}