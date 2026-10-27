namespace Colections.Common
{
    public interface IMultime<T> : IColectie<T>
    {
        IMultime<T> Reuniune(IColectie<T> alta);
        IMultime<T> Intersectie(IColectie<T> alta);
    }
}
