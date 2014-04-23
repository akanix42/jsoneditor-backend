using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using JsonEditor_Backend.API.NPoco;
using NPoco;
using NPoco.Expressions;
using NUnit.Framework;

namespace JsonEditor_Backend.API.Tests.Integration.Repositories
{
    [TestFixture]
    class Documents_Repository_Tests
    {
        private void ClearDocumentsTable()
        {
            using (var db = new JsonEditorDb())
                db.Execute(new Sql("TRUNCATE Documents;"));
        }

        private Document CreateDocument()
        {
            var document = new Document();
            document.Name = "test";
            document.LastUpdated = DateTime.Now;
            document.DateCreated = DateTime.Now;
            document.Data = "data";

            return document;
        }

        [SetUp]
        public void Setup()
        {
            ClearDocumentsTable();
        }

        [TearDown]
        public void Teardown()
        {
            ClearDocumentsTable();
        }

        [Test]
        public void Should_add_document_to_database()
        {
            var document = CreateDocument();
            IDocumentsRepository documentsRepository = new NPocoDocumentsRepository(new JsonEditorDb());

            var documentId = documentsRepository.Add(document);

            using (var db = new JsonEditorDb())
            {
                var documentFromDb = db.FirstOrDefault<Document>(new Sql("SELECT * FROM Documents WHERE Id = @documentId", new { documentId }));
                documentFromDb.Id.Should().Be(documentId);
            }
        }

        [Test]
        public void Should_retrieve_document_from_database()
        {
            var document = CreateDocument();
            IDocumentsRepository documentsRepository = new NPocoDocumentsRepository(new JsonEditorDb());
            documentsRepository.Add(document);

            var documentfromDb = documentsRepository.Get(document.Id);

            documentfromDb.Id.Should().Be(document.Id);
        }

        [Test]
        public void Should_update_document_in_database()
        {
            var document = CreateDocument();
            IDocumentsRepository documentsRepository = new NPocoDocumentsRepository(new JsonEditorDb());
            documentsRepository.Add(document);

            documentsRepository.Update(document);

            var documentfromDb = documentsRepository.Get(document.Id);
            documentfromDb.Name.Should().Be(document.Name);
        }

        [Test]
        public void Should_delete_document_from_database()
        {
            var document = CreateDocument();
            IDocumentsRepository documentsRepository = new NPocoDocumentsRepository(new JsonEditorDb());
            documentsRepository.Add(document);

            documentsRepository.Delete(document.Id);

            var documentfromDb = documentsRepository.Get(document.Id);
            documentfromDb.Should().BeNull();
        }
    }

    public class NPocoDocumentsRepository : IDocumentsRepository
    {
        private readonly IExtendedDatabase database;

        public NPocoDocumentsRepository(IExtendedDatabase database)
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

    public interface IDocumentsRepository
    {
        int Add(Document document);
        Document Get(int documentId);
        void Update(Document document);
        void Delete(int documentId);
    }

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
