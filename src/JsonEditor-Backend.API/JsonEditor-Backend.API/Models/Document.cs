using System;
using NPoco;

namespace JsonEditor_Backend.API.Models
{
    [TableName("Documents")]
    [PrimaryKey("Id")]
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public string Data { get; set; }
    }
}