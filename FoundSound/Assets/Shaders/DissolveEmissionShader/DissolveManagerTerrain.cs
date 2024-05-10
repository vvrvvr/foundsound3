using System.Collections;
using UnityEngine;

public class DissolveManagerTerrain : MonoBehaviour
{
    public GameObject[] objectsToDissolve;
    public Material dissolveMaterial;
    public float dissolveTime = 2f; // Время, за которое должно произойти растворение
    public GameObject dissolveEffectPrefab;
    public float dissolveInterval = 5f; // Интервал между растворениями объектов

    private bool dissolving = false;

    private void Start()
    {
        // Запускаем корутину для растворения объектов по таймеру
        StartCoroutine(DissolveObjectsWithDelay());
    }

    // Метод для растворения объектов по таймеру
    private IEnumerator DissolveObjectsWithDelay()
    {
        while (true)
        {
            // Перемешиваем массив объектов перед началом процесса
            ShuffleArray(objectsToDissolve);

            // Растворяем объекты поочередно
            foreach (GameObject obj in objectsToDissolve)
            {
                StartCoroutine(DoDissolve(obj));
                // Ждем интервал перед следующим растворением
                yield return new WaitForSeconds(dissolveInterval);
            }

            // Повторяем процесс после завершения всех растворений
            yield return null;
        }
    }

    // Метод для перемешивания массива
    private void ShuffleArray(GameObject[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            GameObject temp = array[i];
            int randomIndex = Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    // Корутина для постепенного растворения объекта
    private IEnumerator DoDissolve(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null || dissolveMaterial == null)
        {
            Debug.LogWarning("Renderer or Material is missing.");
            yield break;
        }

        dissolving = true;

        float elapsedTime = 0f;
        float startValue = 0f;
        float endValue = 1f;

        // Устанавливаем материал растворения
        renderer.material = dissolveMaterial;

        while (elapsedTime < dissolveTime)
        {
            float t = elapsedTime / dissolveTime;
            dissolveMaterial.SetFloat("_DissolveAmount", Mathf.Lerp(startValue, endValue, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dissolveMaterial.SetFloat("_DissolveAmount", endValue);

        if (dissolveEffectPrefab != null)
        {
            Instantiate(dissolveEffectPrefab, obj.transform.position, Quaternion.identity);
        }

        // Отключаем рендер объекта, чтобы он оставался невидимым после растворения
        obj.SetActive(false);

        dissolving = false;
    }

    private void OnDisable()
    {
        // Сбросить значение _DissolveAmount при выходе из режима Play
        if (dissolveMaterial != null)
        {
            dissolveMaterial.SetFloat("_DissolveAmount", 0);
        }
    }
}



