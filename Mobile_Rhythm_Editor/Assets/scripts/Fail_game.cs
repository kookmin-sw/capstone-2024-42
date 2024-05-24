using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fail_game : MonoBehaviour
{
    void Start()
    {
        // 점수와 콤보를 초기화합니다.
        ScoreManager.Instance.ResetScore();
        ComboManager.Instance.ResetCombo();
    }


}
