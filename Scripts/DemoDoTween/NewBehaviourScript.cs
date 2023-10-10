using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tapToPlay;
    [SerializeField] private Ease motionType;
    void Start()
    {
        tapToPlay.transform.DOScale(1.2f, 0.5f).SetLoops(100, LoopType.Yoyo).SetEase(motionType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
