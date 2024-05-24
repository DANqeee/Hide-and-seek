using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public HidingObject hidingObject; // Ссылка на объект, у которого нужно отображать здоровье
    public Text healthText; // Текстовый элемент для отображения здоровья

    void Update()
    {
        if (hidingObject != null && healthText != null)
        {
            healthText.text = "Health: " + HidingObject.health.ToString();
        }
    }
}