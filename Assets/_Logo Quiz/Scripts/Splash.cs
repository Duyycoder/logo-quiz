using DG.Tweening;
using GameTool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    [SerializeField] private Image logo;
    [SerializeField] private Image loadingBar;
    [SerializeField] private Image loadingImage;
    [SerializeField] private TextMeshProUGUI loadingText;
    private void Start()
    {
        var logoColor = logo.color;
        logoColor.a = 0f;
        logo.color = logoColor;
        logo.DOFade(1, 3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            loadingImage.gameObject.SetActive(true);
            loadingBar.DOFillAmount(1f, 5f);
            DOTween.To(value => loadingText.text = "Loading... " + (int) value + "%",
                0f, 100f, 5f).OnComplete(() =>
            {
                LoadSceneManager.Instance.LoadScene("Menu Scene");
            });
        });
    }
}
