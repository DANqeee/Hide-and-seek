using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public Transform target; // Цель, за которой следует камера (обычно персонаж)
    private Transform player;
    public Vector3 offset; // Смещение относительно цели
    public float sensitivity = 10f; // Чувствительность мыши
    public float maxYAngle = 80f; // Максимальный угол подъема камеры вверх
    public float minYAngle = 20f; // Максимальный угол опускания камеры вниз

    public LayerMask collisionLayerMask; // Слой, который будет использоваться для обнаружения столкновений

    public float smoothSpeed = 0.125f;
    public float zoomSpeed = 2f; // Скорость приближения

    private Vector3 _currentRotation;
    private float _currentY = 0f;
    private float _currentX = 0f;
    private float _distance; // Расстояние от камеры до цели

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Скрыть и заблокировать курсор
        Cursor.visible = false;
        _distance = offset.magnitude; // Вычисляем начальное расстояние от камеры до цели
    }

    private void Update()
    {
        _currentX += Input.GetAxis("Mouse X") * sensitivity;
        _currentY -= Input.GetAxis("Mouse Y") * sensitivity;
        _currentY = Mathf.Clamp(_currentY, minYAngle, maxYAngle); // Ограничение углов вращения камеры

        // Приближение или отдаление камеры при помощи колеса прокрутки
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _distance -= scroll * zoomSpeed;
        _distance = Mathf.Clamp(_distance, 0f, Mathf.Infinity);
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.y = Mathf.Max(desiredPosition.y, target.position.y + 3 + offset.y + 1); // Убедитесь, что камера всегда выше
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target);
        }

        // Плавное вращение камеры
        _currentRotation = Vector3.SmoothDamp(_currentRotation, new Vector3(_currentY, _currentX), ref _currentRotation, 0f);
        transform.eulerAngles = _currentRotation;

        RaycastHit hit;
        Vector3 desiredCameraPosition = target.position - transform.forward * _distance + transform.up * offset.y;
        if (Physics.Linecast(target.position, desiredCameraPosition, out hit, collisionLayerMask))
        {
            // Если есть препятствие, помещаем камеру ближе к цели
            transform.position = hit.point;
        }
        else
        {
            transform.position = desiredCameraPosition; // Если препятствий нет, камера находится на желаемом расстоянии от цели
        }

        if (Controller.canMove)
        {
            // Поворачиваем персонажа в сторону, куда смотрит камера
            var targetRotation = Quaternion.Euler(0, _currentRotation.y, 0);
            target.rotation = Quaternion.Slerp(target.rotation, targetRotation, Time.deltaTime * sensitivity);
        }
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}