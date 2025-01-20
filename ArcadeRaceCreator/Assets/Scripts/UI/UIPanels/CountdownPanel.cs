using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class CountdownPanel : UIPanel {
    public event Action CountdownFinished;

    [SerializeField] int duration = 3;
    [SerializeField] private TextMeshProUGUI _timeValueText;

    public override void Show(bool value) {
        base.Show(value);

        if (value)
            StartAnimation();
    }
    
    private async UniTask StartAnimation() {
        await Countdown();

        CountdownFinished?.Invoke();
    }

    private async UniTask Countdown() {
        for (int i = duration; i >= 0; i--) {
            _timeValueText.text = i.ToString();

            Sequence countdownSequence = DOTween.Sequence().SetEase(Ease.Linear);
            countdownSequence.Append(_timeValueText.transform.DOScale(Vector3.one, 0.5f));
            countdownSequence.Append(_timeValueText.transform.DOScale(Vector3.zero, 0.5f));
            countdownSequence.Play();

            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
