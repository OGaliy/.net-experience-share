namespace QuartzExample.Domain.Base;

public abstract class Entity
{
    private int _id;

    public virtual int Id
    {
        get => _id;
        protected set => _id = value;
    }
}
