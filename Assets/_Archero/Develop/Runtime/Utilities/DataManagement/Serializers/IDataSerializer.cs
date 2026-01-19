namespace _Archero.Develop.Runtime.Utilities.DataManagement.Serializers
{
    public interface IDataSerializer
    {
        string Serialize<TData>(TData data);
        TData Deserialize<TData>(string serializedData);
    }

}
