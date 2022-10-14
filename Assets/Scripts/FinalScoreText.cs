using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScoreText : MonoBehaviour
{
    [SerializeField]
    PersistantNumber finalScore;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = finalScore.Score.ToString();
    }

    public void Refresh()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = finalScore.Score.ToString();
    }
}
