using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class TopBarView : BaseView
    {
        protected override void Start()
        {
            base.Start();

            StartUIManager.Instance.RegisterView<TopBarView>(this);
            Show();
        }
    }
}