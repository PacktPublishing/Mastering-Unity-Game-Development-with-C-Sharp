using UnityEngine;

namespace Chapter5
{
    public abstract class BaseView : MonoBehaviour
    {
        private Canvas canvas;

        public bool isActiveView;
        public UITween tweenComponent;

        protected virtual void Start()
        {
            canvas = GetComponent<Canvas>();
        }

        // Show this view 
        public virtual void Show()
        {
            isActiveView = true;
            canvas.enabled = true;
            gameObject.SetActive(true);
            PlayTweens(true);
            ShowView();
        }

        public virtual void ShowView()
        {

        }

        // Hide this view 
        public virtual void Hide()
        {
            PlayTweens(false);
        }

        private void OnOutTweenComplete()
        {
            isActiveView = false;
            canvas.enabled = false;
        }


        public virtual void HideCanvas()
        {
            canvas.enabled = false;
        }

        // Return true if this view is currently visible 
        public bool IsVisible()
        {
            return canvas.enabled;
        }

        private void PlayTweens(bool state)
        {
            if (state)
            {
                tweenComponent?.PlayInTween();
            }
            else
            {
                if (tweenComponent == null)
                {
                    OnOutTweenComplete();
                }
                else
                {
                    tweenComponent.PlayOutTween(OnOutTweenComplete);
                }
            }
        }
    }
}