using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameHandler : MonoBehaviour
{
    [SerializeField] Transform mainCam, canvas;
    [SerializeField] RectTransform panel;
    [SerializeField] TextMeshProUGUI p1ScoreText, p2ScoreText;
    [SerializeField] float moveAmount, moveDuration, shakeStrength, shakeDuration;
    public Ease easeAnim;
    int p1Score, p2Score;


    public void UpperSideScored()
    {
        p1Score += 1;
        p1ScoreText.text = p1Score.ToString();
        AnimateBoard();
        ShakeCam();
    }
    public void BottomSideScored()
    {
        p2Score += 1;
        p2ScoreText.text = p2Score.ToString();
        AnimateBoard();
        ShakeCam();
    }

    private void AnimateBoard()
    {
        panel.DOAnchorPos(Vector2.zero, moveDuration).SetEase(easeAnim).OnComplete(() =>
        {
            panel.DOAnchorPos(new Vector2(-500, 0), 2).SetEase(easeAnim);
        });

    }

    private void ShakeCam()
    {
        mainCam.transform.DOShakePosition(shakeDuration, shakeStrength, 10, 90);
        //canvas.transform.DOShakePosition(shakeDuration, shakeStrength, 10, 90);
    }

}
