using System.Collections.Generic;
using GameTool;
using UnityEngine;
using UnityEngine.UI;

public class ScrollMenu : MonoBehaviour
{
    [SerializeField] private List<Button> levelBtn;
    private void Start()
    {
        int count = levelBtn.Count;
        for (int i = 0; i < count; ++i)
        {
            int index = i;
            levelBtn[i].onClick.AddListener(() =>
            {
                string sceneName = "Level " + (index + 1);
                Debug.Log(sceneName);
                LoadSceneManager.Instance.LoadScene(sceneName);
            });
        }
    }
}