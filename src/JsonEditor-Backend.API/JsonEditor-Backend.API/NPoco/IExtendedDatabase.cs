namespace JsonEditor_Backend.API.NPoco
{
    public interface IExtendedDatabase
    {
        string DefaultPrimaryKeyColumn { get; set; }
        object Insert<T>(string tableName, T poco);
        object Insert<T>(string tableName, T poco, bool autoIncrement);
    }
}