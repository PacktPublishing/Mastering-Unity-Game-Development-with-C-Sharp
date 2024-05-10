namespace Chapter5
{
    public class TopBarView : BaseView
    {
        protected override void Start()
        {
            base.Start();
            StartUIManager.Instance.RegisterView<TopBarView>(this);
            Show();
        }
       //Add here logic for displaying the currencies for the player 
    }
}