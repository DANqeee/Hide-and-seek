using UnityEngine;
using UnityEngine.UI;

public class PlayerHinterCounter : MonoBehaviour
{
    public Text playerCountText;  // Ссылка на компонент UI Text для отображения количества игроков
    public Text hinterCountText;  // Ссылка на компонент UI Text для отображения количества "Hinter"

    private void Start()
    {
        // Ensure both UI Text components are set
        if (playerCountText == null)
        {
            Debug.LogError("Player Count UI Text компонент не установлен!");
            return;
        }

        if (hinterCountText == null)
        {
            Debug.LogError("Hinter Count UI Text компонент не установлен!");
            return;
        }

        // Обновляем текст UI при старте
        UpdateUIText();
    }

    private void Update()
    {
        // Обновляем текст UI каждый кадр
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        // Подсчитываем количество объектов с тегом "Player"
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;

        // Подсчитываем количество объектов с тегом "Hinter"
        int hinterCount = GameObject.FindGameObjectsWithTag("Hinter").Length;

        // Обновляем текст UI для игроков
        playerCountText.text = $"Players: {playerCount}";

        // Обновляем текст UI для "Hinter"
        hinterCountText.text = $"Hinters: {hinterCount}";
    }
}