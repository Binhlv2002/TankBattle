using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private float minimumLoadingTime = 2f; // ít nhất 2 giây

    private float targetProgress = 0f;
    private float currentProgress = 0f;

    private void Start()
    {
        StartCoroutine(LoadGameSceneAsync());
    }

    private IEnumerator LoadGameSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneLoader.sceneToLoad);
        operation.allowSceneActivation = false;

        float elapsedTime = 0f;

        while (!operation.isDone)
        {
            // Lấy target progress
            if (operation.progress < 0.9f)
            {
                targetProgress = operation.progress;
            }
            else
            {
                targetProgress = 1f;
            }

            // Mượt hoá thanh progress
            currentProgress = Mathf.MoveTowards(currentProgress, targetProgress, Time.deltaTime);

            if (progressBar != null)
                progressBar.value = currentProgress;
            
            if (loadingText != null)
            {
                loadingText.text = Mathf.RoundToInt(currentProgress * 100f) + "%";
            }

                // Tăng thời gian đã trôi qua
                elapsedTime += Time.deltaTime;

            // Khi progressBar gần đầy & thời gian tối thiểu đạt
            if (currentProgress >= 0.99f && elapsedTime >= minimumLoadingTime)
            {
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.2f); // Thêm chút delay cho mượt
        operation.allowSceneActivation = true;
    }
}
