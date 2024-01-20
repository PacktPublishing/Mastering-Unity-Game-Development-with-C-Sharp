using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class UIManager : Singlton<UIManager>
    {
        public GenericDictionary<Type, BaseView> views = new GenericDictionary<Type, BaseView>();

        private BaseView lastActiveView;

        protected override void Awake()
        {
            base.Awake();
        }

        // Register a view with the UIManager
        public void RegisterView<T>(T view) where T : BaseView
        {
            if (view != null && !views.ContainsKey(typeof(T)))
            {
                views.Add(typeof(T), view);
            }
        }

        public void CreateView<T>()
        {

        }
        // Show a view
        public void ShowView<T>() where T : BaseView
        {
            if (views.ContainsKey(typeof(T)))
            {
                var view = views[typeof(T)];
                // Show the new view
                view.Show();
                lastActiveView = view;
            }
            else
            {
                Debug.LogError("The View Of Type is Not Exist " + typeof(T).ToString());
            }

        }

        public void HideView<T>()
        {
            if (views.ContainsKey(typeof(T)))
            {
                var view = views[typeof(T)];

                if (view.IsVisible())
                    view.Hide();
            }
        }



        // Hide the currently active view
        public void HideActiveView()
        {
            if (lastActiveView != null)
            {
                lastActiveView.Hide();
                lastActiveView = null;
            }
        }

        public BaseView GetView(Type viewType)
        {
            if (views.ContainsKey(viewType)) return views[viewType];
            else return null;
        }

        public T GetView<T>()
        {
            if (views.ContainsKey(typeof(T))) return (T)Convert.ChangeType(views[typeof(T)], typeof(T));
            else return (T)Convert.ChangeType(null, typeof(T));
        }
    }
}