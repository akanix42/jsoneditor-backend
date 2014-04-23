using JsonEditor_Backend.API.Models;

namespace JsonEditor_Backend.API.Repositories
{
    public interface IDocumentsRepository
    {
        int Add(Document document);
        Document Get(int documentId);
        void Update(Document document);
        void Delete(int documentId);
    }
}