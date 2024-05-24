using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTransformation : MonoBehaviour
{
    public Camera playerCamera; // Камера игрока
    public LayerMask transformableObjectsLayer; // Слой, содержащий объекты, в которые можно превратиться
    [SerializeField] float yOffset = -4f;
    public GameObject currentForm; // Текущая форма игрока

    private bool hasTransformed = false; // Флаг, чтобы убедиться, что трансформация произошла только один раз

    private void Start()
    {
        StartCoroutine(TransformRoutine());
    }

    private IEnumerator TransformRoutine()
    {
        while (true)
        {
            // Проверяем, что объект получил тег "Player" и еще не превращался
            if (gameObject.CompareTag("Player") && !hasTransformed)
            {
                TransformIntoRandomObject();
                hasTransformed = true;

                // Ждем 30 секунд перед следующим превращением
                yield return new WaitForSeconds(60f);
                hasTransformed = false; // Сбрасываем флаг трансформации
            }
            else
            {
                yield return null;
            }
        }
    }

    private void TransformIntoRandomObject()
    {
        // Получаем все объекты на сцене с заданным слоем
        GameObject[] transformableObjects = GetObjectsInLayer(transformableObjectsLayer);

        if (transformableObjects.Length > 0)
        {
            // Выбираем случайный объект
            GameObject randomObject = transformableObjects[Random.Range(0, transformableObjects.Length)];
            TransformInto(randomObject);
        }
        else
        {
            Debug.LogWarning("Нет объектов для трансформации на заданном слое.");
        }
    }

    private GameObject[] GetObjectsInLayer(LayerMask layerMask)
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> objectsInLayer = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (layerMask == (layerMask | (1 << obj.layer)))
            {
                objectsInLayer.Add(obj);
            }
        }

        return objectsInLayer.ToArray();
    }

    private void TransformInto(GameObject targetObject)
    {
        if (currentForm != null)
        {
            Destroy(currentForm); // Уничтожаем текущую форму, если она существует
        }

        // Создаем копию объекта и помещаем на позицию игрока
        currentForm = Instantiate(targetObject, transform.position, transform.rotation);
        currentForm.transform.SetParent(transform); // Делаем объект дочерним, чтобы он следовал за игроком

        currentForm.transform.localPosition = new Vector3(0, yOffset, 0);
    }
}