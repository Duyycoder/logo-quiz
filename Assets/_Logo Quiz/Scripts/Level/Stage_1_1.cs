using System.Collections.Generic;
using _Logo_Quiz.Scripts;
using DG.Tweening;
using GameTool;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Stage_1_1 : BaseUI
{
    [SerializeField] private Button backBtn;
    [SerializeField] private Image logo;
    [SerializeField] private List<Image> blanks;
    [SerializeField] private List<bool> isFilled;
    [SerializeField] private List<Button> alphabets;
    [SerializeField] private List<Transform> localPos;
    [SerializeField] private List<int> alphabetIndex;

    [SerializeField] private Image nullBlank;
    [SerializeField] private Button nullAlphabet;
    [SerializeField] private HorizontalLayoutGroup blanksGroup;
    [SerializeField] private GridLayoutGroup alphabetsGroup;
    
    [SerializeField] private int level;
    [SerializeField] private int stage;
    [SerializeField] private int blankNumber;
    [SerializeField] private int alphabetNumber;
    [SerializeField] private string answer;
    [SerializeField] private string playerAnswer;
    [SerializeField] private StageResources curStage;

    
    private void Start()
    {
        backBtn.onClick.AddListener(() =>
        {
            // LoadSceneManager.Instance.LoadScene("Level " + (level + 1));
        });
        
        // Lấy và set data cho stage từ Game Data
        GetStageData();
        SetStageData();

        for (int i = 0; i < alphabets.Count; i++)
        {
            var index = i;
            alphabets[i].onClick.AddListener(() =>
            {
                Debug.Log("1 " + alphabets[index].transform.position + " " + localPos[index].position);
                // Nếu ô chữ được ấn đang nằm ở vị trí ban đầu
                if (alphabets[index].transform.position == localPos[index].position)
                {
                    Debug.Log("TH1");
                    // Lấy index ô chưa được điền đầu tiên
                    int firstunfillindex = FirstUnFilledIndex();
                    
                    // Nếu còn ô chưa được điền
                    if (firstunfillindex != -1)
                    {
                        // Điền vào ô đó và cập nhật trạng thái ô trống đã được điền
                        alphabets[index].transform.DOMove(blanks[firstunfillindex].transform.position, 0.5f).OnComplete(() =>
                            {
                                Debug.Log("2 " + alphabets[index].transform.position + " " + localPos[index].position);
                            });
                        isFilled[firstunfillindex] = true;
                    }
                    
                    // Gán lại firstunfillindex để nếu đã điền đủ thì nó trả về -1 => check điều kiện đáp án đúng hay sai
                    firstunfillindex = FirstUnFilledIndex();
                    // Nếu không còn ô nào trống
                    if (firstunfillindex == -1)
                    {
                        // Xử lý check đáp án khi người chơi điền toàn bộ ô trống
                        OnPlayerFillAllBlanks();
                    }
                }
                // Nút được ấn khi đang nằm trùng với ô trống
                //if (alphabets[index].transform.position != localPos[index].position) 
                else
                {
                    Debug.Log("TH2");
                    int fillIndex = -1; // Biến fillIndex lưu index của ô trống chứa nút vừa được ấn
                    for (int j = 0; j < blankNumber; j++)
                    {
                        // Tìm index của nút đang được ấn
                        if (alphabets[index].transform.position == blanks[j].transform.position)
                        {
                            fillIndex = j;
                            break;
                        }
                    }
                    Debug.Log("Fill Index = " + fillIndex);
                    
                    // Di chuyển về vị trí ban đầu và cập nhật ô trống chưa được điền
                    alphabets[index].transform.DOMove(localPos[index].position, 0.5f);
                    isFilled[fillIndex] = false;
                }
            });
        }
    }

    private void GetStageData()
    {
        curStage = GameData.Instance.gameResources.levels[level].stages[stage];
        // Lấy từ Game Data level và state hiện tại
        level = GameData.Instance.CurLevel - 1;
        stage = GameData.Instance.CurStage - 1;
        
        // Lấy thông tin hình ảnh và đáp án của màn
        answer = curStage.stageAnswer;
        logo.sprite = curStage.stageImg;
        
        // Lấy thông tin cho màn
        blankNumber = curStage.blankNumber;
        alphabetNumber = curStage.alphabetsChar.Count;
    }

    private void SetStageData()
    {
        for (int i = 0; i < blankNumber; i++)
        {
            isFilled.Add(false);
            alphabetIndex.Add(-1);
            var blank = Instantiate(nullBlank, blanksGroup.transform);
            blanks.Add(blank);
        }

        for (int i = 0; i < alphabetNumber; i++)
        {   
            var alphabet = Instantiate(nullAlphabet, alphabetsGroup.transform);
            alphabet.image.sprite = GameData.Instance.alphabetResources.
                alphabetDict[curStage.alphabetsChar[i].ToString()];
            alphabets.Add(alphabet);
            localPos.Add(alphabets[i].transform);
        }
    }
    
    // Trả về index ô trống đầu tiên chưa được điền, nếu tất cả ô trống đã được điền thì trả về -1
    private int FirstUnFilledIndex()
    {
        for (int i = 0; i < blankNumber; i++)
        {
            if (!isFilled[i])
                return i;
        }
        return -1;
    }

    private void OnPlayerFillAllBlanks()
    {
        // Người chơi điền tất cả các ô trống
        playerAnswer = "";
        for (int i = 0; i < blankNumber; i++)
        {
            for (int j = 0; j < alphabetNumber; j++)
            {
                // Lấy ra list chứa index của các nút đã điền vào ô trống
                if (blanks[i].transform.position == alphabets[j].transform.position)
                {
                    alphabetIndex[i] = j;
                    playerAnswer += curStage.alphabetsChar[j].ToString();
                    break;
                }
            }
            Debug.Log("AlphabetIndex " + alphabetIndex[i]);
        }

        // Chuẩn hoá đáp án của người chơi thành in hoa
        playerAnswer = playerAnswer.ToUpper();
        Debug.Log("Player Answer = " + playerAnswer);
            
        // Đáp án người chơi đúng 
        if (playerAnswer == answer)
        {
            CanvasManager.Instance.Push(eUIName.StageCompletePopup);
        }
        // Đáp án sai
        else
        {
            // Tìm những ô điền sai và trả về vị trí ban đầu
            for (int i = 0; i < blankNumber; i++)
            {
                Debug.Log("i = " + i);
                // Ô đã điền đúng
                if (playerAnswer[i] == answer[i])
                {
                    Debug.Log("Correct a character in index " + i + " : " + answer[i]);
                }
                // Ô điền sai 
                else
                {
                    // Trả nút lại về vị trí ban đầu, cập nhật ô trống chưa được điền
                    alphabets[alphabetIndex[i]].transform.DOMove(localPos[i].position, 2f);
                    isFilled[i] = false;
                }
            }
        }
    }
}
