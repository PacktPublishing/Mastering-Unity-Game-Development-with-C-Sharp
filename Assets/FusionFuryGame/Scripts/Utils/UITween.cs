using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class UITween : MonoBehaviour
{
    public enum TweenType
    {
        Fade,
        Rotate,
        Scale,
        Position
        // Add any other available tweens here
    }

    [System.Serializable]
    public class TweenSettings
    {
        public TweenType tweenType;
        public float duration;
        public float delay;
        public float fadeValue;
        public Ease ease;
        public Vector3 startValue; // For position and rotation tweens
        public Vector3 endValue; // For position and rotation tweens
    }

    public TweenSettings inTweens;
    public TweenSettings outTweens;
    public CanvasGroup canvasGroup;
    public bool loadCanvas;



    public static UnityAction onInTweenComplete = delegate { };
    public static UnityAction onOutTweenComplete = delegate { };



    private void Awake()
    {
        if (canvasGroup == null && loadCanvas)
            canvasGroup = GetComponent<CanvasGroup>();
    }

    public void PlayInTween()
    {
        var tween = GetTween(inTweens);
        tween.OnComplete(OnInTweenComplete);
    }

    public void PlayOutTween(UnityAction action)
    {
        if(outTweens != null)
        {
            var tween = GetTween(outTweens);
            tween.OnComplete(()=>action?.Invoke());
        }
        else
        {
            action?.Invoke();
        }

    }

    private Tweener GetTween(TweenSettings tweenSetting)
    {
        Tweener tween = null;

        switch (tweenSetting.tweenType)
        {
            case TweenType.Fade:
                tween = canvasGroup.DOFade(tweenSetting.fadeValue, tweenSetting.duration).SetDelay(tweenSetting.delay);
                break;
            case TweenType.Rotate:

                tween = transform.DORotate(tweenSetting.endValue, tweenSetting.duration).SetDelay(tweenSetting.delay);
                break;
            case TweenType.Scale:
                transform.localScale = tweenSetting.startValue;
                tween = transform.DOScale(tweenSetting.endValue, tweenSetting.duration).SetDelay(tweenSetting.delay);
                break;
            case TweenType.Position:
                transform.localPosition = tweenSetting.startValue;
                tween = transform.DOMove(tweenSetting.endValue, tweenSetting.duration).SetDelay(tweenSetting.delay);
                break;
                // Add cases for any other available tweens
        }

        if (tween != null)
            tween.SetEase(tweenSetting.ease);

        return tween;
    }

    private void OnInTweenComplete()
    {
        onInTweenComplete.Invoke();
    }

    private void OnOutTweenComplete()
    {
        onOutTweenComplete.Invoke();
    }

    private void EnableGameObject()
    {
        gameObject.SetActive(true);
    }

    private void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}