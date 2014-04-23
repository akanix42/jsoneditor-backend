using System;
using System.Collections.Generic;
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
            document.DateCreated = DateTime.Now;
            document.LastUpdated = DateTime.Now;
            using (var db = database)
                db.Insert(document);
            return document.Id;
        }

        public void Delete(int documentId)
        {
            using (var db = database)
                db.Delete<Document>(documentId);
        }

        public List<Document> GetAll()
        {
            using (var db = database)
                return db.Fetch<Document>();
        }

        public Document Get(int documentId)
        {
            using (var db = database)
                return db.SingleOrDefaultById<Document>(documentId);
        }

        public void Update(Document document)
        {
            document.LastUpdated = DateTime.Now;
            using (var db = database)
                db.Update(document);
        }
    }
}