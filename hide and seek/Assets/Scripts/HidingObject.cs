using UnityEngine;

public class HidingObject : MonoBehaviour
{
    public static float health = 100f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount/2;
        if (health <= 0f)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    void Die()
    {
        // Логика для уничтожения объекта или другой обработки смерти
        Destroy(gameObject);
    }
}