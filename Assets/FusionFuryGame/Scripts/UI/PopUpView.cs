using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FusionFuryGame
{



    public class PopUpView : BaseView
    {
        public TextMeshProUGUI titleTxt;
        public TextMeshProUGUI detialsTxt;

        public Button OkButton;
        public Button cancelButton;
        public Button AdsButton;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            UIManager.Instance.RegisterView<PopUpView>(this);
        }

        public override void ShowView()
        {
            base.ShowView();
        }


        public void ShowMessage(string title = "Title", string details = "Details", MessageType messageType = MessageType.MESSAGE, UnityAction okAction = null, UnityAction cancelAction = null)
        {
            titleTxt.text = title;
            detialsTxt.text = details;
            SetupMessage(messageType);
            OkButton.onClick.AddListener(() => OnOkPressed(okAction));
            cancelButton.onClick.AddListener(() => OnCancelPressed(cancelAction));
            AdsButton.onClick.AddListener(() => OnAdsPressed(okAction));
        }


        private void SetupMessage(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.CONFIRM:
                    OkButton.gameObject.SetActive(true);
                    cancelButton.gameObject.SetActive(true);
                    AdsButton.gameObject.SetActive(false);
                    break;
                case MessageType.MESSAGE:
                    OkButton.gameObject.SetActive(true);
                    cancelButton.gameObject.SetActive(false);
                    AdsButton.gameObject.SetActive(false);
                    break;
                case MessageType.ADS:
                    cancelButton.gameObject.SetActive(true);
                    OkButton.gameObject.SetActive(false);
                    AdsButton.gameObject.SetActive(true);
                    break;

            }
        }

        private void OnOkPressed(UnityAction okAction)
        {
            OkButton.onClick.RemoveAllListeners();
            okAction?.Invoke();
            Hide();
        }

        private void OnCancelPressed(UnityAction cancelAction)
        {
            cancelButton.onClick.RemoveAllListeners();
            cancelAction?.Invoke();
            Hide();
        }

        private void OnAdsPressed(UnityAction okAction)
        {
            AdsButton.onClick.RemoveAllListeners();
            okAction?.Invoke();
            Hide();
        }

    }


    public enum MessageType
    {
        NONE,
        CONFIRM,
        MESSAGE,
        ADS
    }
}