namespace OmnioCore.Save
{

    public interface IDataSerializer<T> where T : class
    {
        T Data { get; }
        void SaveData();
        void LoadData();
        void ClearData();
    }

}
