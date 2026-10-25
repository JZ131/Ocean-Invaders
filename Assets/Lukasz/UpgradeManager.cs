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

    // Funkcja podpinana pod przycisk "Bigger damages"
    public void BuyDamageUpgrade()
    {
        int cost = 10;
        if (playerPoints >= cost)
        {
            playerPoints -= cost;
            UpdatePointsUI();
            Debug.Log("Kupiono ulepszenie: Wiêksze obra¿enia!");
        }
        else
        {
            Debug.Log("Za ma³o punktów na to ulepszenie!");
        }
    }

    public void BuySpeedUpgrade()
    {
        int cost = 11;
        if (playerPoints >= cost)
        {
            playerPoints -= cost;
            UpdatePointsUI();
            Debug.Log("Kupiono ulepszenie: Wiêksze szybkoœæ!");
        }
        else
        {
            Debug.Log("Za ma³o punktów na to ulepszenie!");
        }
    }

    public void BuyLifeUpgrade()
    {
        int cost = 12;
        if (playerPoints >= cost)
        {
            playerPoints -= cost;
            UpdatePointsUI();
            Debug.Log("Kupiono ulepszenie: Wiêcej zycia!");
        }
        else
        {
            Debug.Log("Za ma³o punktów na to ulepszenie!");
        }
    }

    public void CloseShop()
    {
        gameObject.SetActive(false); // Ukrywa panel
        Debug.Log("Sklep zamkniêty. Zaczynamy kolejn¹ falê!");
    }
}