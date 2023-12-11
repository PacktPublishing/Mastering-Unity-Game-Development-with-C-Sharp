namespace CommandPattern
{
    public abstract class Command
    {
        protected IEntity _entity;
        public Command(IEntity entity)
        {
            _entity = entity;
        }
        public abstract void Excute();
        public abstract void Undo();
    }
}