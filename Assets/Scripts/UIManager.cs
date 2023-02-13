using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image _imgFrame;
    int _baloonCount = 0;
    [SerializeField] private TMP_Text _countText;
    private void Awake()
    {
        Baloon.BaloonPop += SetBaloonUI;
    }

    private void SetBaloonUI(Sprite sprite)
    {
        _imgFrame.sprite = sprite;
        _baloonCount++;
        _countText.text = _baloonCount.ToString();
    }

    private void OnDestroy()
    {
        Baloon.BaloonPop -= SetBaloonUI;
    }
}
