using System.Collections.Generic;
using GameTool;
using UnityEngine;
using UnityEngine.UI;

public class Level_1 : BaseUI
{
    [SerializeField] private List<Button> stagesBtn;
    [SerializeField] private int curLevel;
    private void Start()
    {
        int count = stagesBtn.Count;
        for (int i = 0; i < count; ++i)
        {
            int index = i;
            stagesBtn[i].onClick.AddListener(() =>
            {
                CanvasManager.Instance.Push(eUIName.StageMenu);
            });
        }
    }
}
