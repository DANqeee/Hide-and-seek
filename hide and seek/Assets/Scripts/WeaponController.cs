using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject weapon; // Объект оружия
    public Transform firePoint; // Точка выстрела
    public GameObject bulletImpactPrefab; // Префаб следа выстрела
    public float damage = 10f; // Урон
    public AudioClip shootSound; // Звуковой клип для выстрела

    private Camera _playerCamera;
    private int _playerLayerMask;
    private int _hinterLayerMask;
    private AudioSource _audioSource; // Источник звука

    void Start()
    {
        _playerCamera = Camera.main; // Получаем главную камеру
        _playerLayerMask = LayerMask.GetMask("Player"); // Получаем маску слоя игрока
        _hinterLayerMask = LayerMask.GetMask("Hinter"); // Получаем маску слоя Hinter
        _audioSource = gameObject.AddComponent<AudioSource>(); // Добавляем компонент AudioSource
        weapon.SetActive(false); 
    }

    void Update()
    {
        // Проверяем, что игрок имеет тег "Hinter" перед включением/выключением оружия
        if (Input.GetKeyDown(KeyCode.Alpha2) && gameObject.CompareTag("Hinter"))
        {
            weapon.SetActive(!weapon.activeSelf); // Включаем/выключаем оружие
        }

        if (Input.GetButtonDown("Fire1") && weapon.activeSelf)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Воспроизводим звук выстрела
        _audioSource.PlayOneShot(shootSound);

        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward.normalized);
        RaycastHit hit;

        // Используем побитовую операцию NOT (~) для игнорирования слоя игрока и слоя Hinter
        int combinedMask = ~_playerLayerMask & ~_hinterLayerMask;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, combinedMask))
        {
            Debug.Log(hit.collider.name);

            // Создаем след выстрела на поверхности
            Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));

            // Проверяем, попали ли мы в объект с тегом "Player"
            if (hit.collider.CompareTag("Player"))
            {
                HidingObject hidingObject = hit.collider.GetComponent<HidingObject>();
                if (hidingObject != null)
                {
                    hidingObject.TakeDamage(damage);
                }
            }
            else
            {
                // Не попали в объект с тегом "Player", снимаем здоровье у "Hinter"
                TakeDamageAsHinter(damage);
            }
        }
        else
        {
            // Не попали ни в какой объект, снимаем здоровье у "Hinter"
            TakeDamageAsHinter(damage);
        }
    }

    void TakeDamageAsHinter(float amount)
    {
        if (gameObject.CompareTag("Hinter"))
        {
            HidingObject hidingObject = GetComponent<HidingObject>();
            if (hidingObject != null)
            {
                hidingObject.TakeDamage(amount);
            }
        }
    }
}