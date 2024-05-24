using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 6.0F;
    public float sprintSpeedMultiplier = 1.5F; // Множитель скорости при беге
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float rotationSpeed = 100.0F; // Скорость вращения
    private Vector3 moveDirection = Vector3.zero;
    public static bool canMove = true; // Добавляем флаг для управления возможностью движения персонажа

    private void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        // Проверяем нажатие правой кнопки мыши и переключаем флаг canMove
        if (Input.GetMouseButtonDown(1))
        {
            canMove = !canMove; // Переключаем состояние между движением и стопом
        }

        // Обрабатываем вращение всегда, независимо от состояния canMove
        HandleRotation();

        // Двигаем персонажа только если canMove == true
        if (canMove && controller.isGrounded)
        {
            float currentSpeed = speed; // Скорость по умолчанию

            // Если нажата клавиша SHIFT, ускоряемся
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed *= sprintSpeedMultiplier;
            }

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= currentSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Применяем гравитацию всегда, чтобы персонаж не парил в воздухе
        moveDirection.y -= gravity * Time.deltaTime;

        // Перемещаем персонажа, даже если он не может двигаться, чтобы применить гравитацию
        controller.Move(moveDirection * Time.deltaTime);

        // Если персонаж не может двигаться, обнуляем горизонтальную и вертикальную составляющие вектора движения
        if (!canMove)
        {
            moveDirection.x = 0;
            moveDirection.z = 0;
        }
    }

    private void HandleRotation()
    {
        float rotation = 0.0F;

        // Обрабатываем нажатия клавиш Q и E для вращения
        if (Input.GetKey(KeyCode.Q))
        {
            rotation = -rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rotation = rotationSpeed * Time.deltaTime;
        }

        // Применяем вращение к объекту
        transform.Rotate(0, rotation, 0);
    }
}