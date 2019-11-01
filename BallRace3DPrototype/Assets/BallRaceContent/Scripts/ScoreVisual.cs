using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreVisual : MonoBehaviour
{
    public float m_scorePunchAmount;
    public float m_scorePunchTime;

    public void OnScoreIncrease()
    {
        iTween.PunchScale(gameObject, Vector3.one * m_scorePunchAmount, m_scorePunchTime);
    }
}
