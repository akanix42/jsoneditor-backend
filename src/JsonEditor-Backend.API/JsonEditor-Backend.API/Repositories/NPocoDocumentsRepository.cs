using JsonEditor_Backend.API.Models;
using JsonEditor_Backend.API.NPoco;
using NPoco;

namespace JsonEditor_Backend.API.Repositories
{
    public class NPocoDocumentsRepository : IDocumentsRepository
    {
        private readonly IDatabase database;

        public NPocoDocumentsRepository(IDatabase database)
        {
            this.database = database;
        }

        public int Add(Document document)
        {
            using (var db = new JsonEditorDb())
                db.Insert(document);
            return document.Id;
        }

        public void Delete(int documentId)
        {
            using (var db = new JsonEditorDb())
                db.Delete<Document>(documentId);
        }

        public Document Get(int documentId)
        {
            using (var db = new JsonEditorDb())
                return db.SingleOrDefaultById<Document>(documentId);
        }

        public void Update(Document document)
        {
            using (var db = new JsonEditorDb())
                db.Update(document);
        }
    }
}