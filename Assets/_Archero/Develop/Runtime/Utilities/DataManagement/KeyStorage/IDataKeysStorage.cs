namespace _Archero.Develop.Runtime.Utilities.DataManagement.KeyStorage
{
    public interface IDataKeysStorage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }

}
