using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using JsonEditor_Backend.API.Models;
using JsonEditor_Backend.API.NPoco;
using JsonEditor_Backend.API.Repositories;
using NPoco;
using NUnit.Framework;
using SmartFormat;

namespace JsonEditor_Backend.API.Tests.Integration.Controllers
{
    [TestFixture]
    class Documents_Controller_Tests
    {
        private string baseUri = "http://local.api.jsoneditor.com";
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

        [SetUpAttribute]
        public void Setup()
        {
            ClearDocumentsTable();
        }

        [TearDownAttribute]
        public void Teardown()
        {
            ClearDocumentsTable();
        }

        private HttpClient GetHttpClient()
        {
            return new HttpClient() { BaseAddress = new Uri(baseUri) };
        }

        [Test]
        public async void Should_add_document_to_database()
        {
            var document = CreateDocument();

            using (var httpClient = GetHttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(Smart.Format("api/documents/"), document);
                var contentString = await response.Content.ReadAsStringAsync();
                var documentId = await response.Content.ReadAsAsync<int>();

                documentId.Should().NotBe(0);

                var documentsRepository = new NPocoDocumentsRepository(new JsonEditorDb());
                var documentfromDb = documentsRepository.Get(documentId);
                documentfromDb.Should().NotBeNull();
            }
        }
  
        [Test]
        public async void Should_retrieve_document_from_database()
        {
            var document = CreateDocument();
            var documentsRepository = new NPocoDocumentsRepository(new JsonEditorDb());
            documentsRepository.Add(document);

            using (var httpClient = GetHttpClient())
            {
                var response = await httpClient.GetAsync(Smart.Format("api/documents/{documentId}", new { documentId = document.Id }));
                var contentString = await response.Content.ReadAsStringAsync();
                var retrievedDocument = await response.Content.ReadAsAsync<Document>();
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                retrievedDocument.Id.Should().Be(document.Id);
            }
        }

        [Test]
        public async void Should_update_document_in_database()
        {
            var document = CreateDocument();
            document.Data = "test";
            var documentsRepository = new NPocoDocumentsRepository(new JsonEditorDb());
            documentsRepository.Add(document);
            document.Data = "test-data";

            using (var httpClient = GetHttpClient())
            {
                var response = await httpClient.PutAsJsonAsync(Smart.Format("api/documents/{documentId}", new { documentId = document.Id }), document);

                response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            }

            var documentfromDb = documentsRepository.Get(document.Id);
            documentfromDb.Data.Should().Be(document.Data);
        }

        [Test]
        public async void Should_delete_document_from_database()
        {
            var document = CreateDocument();
            var documentsRepository = new NPocoDocumentsRepository(new JsonEditorDb());
            documentsRepository.Add(document);

            using (var httpClient = GetHttpClient())
            {
                var response = await httpClient.DeleteAsync(Smart.Format("api/documents/{documentId}", new { documentId = document.Id }));
                response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            }

            var documentfromDb = documentsRepository.Get(document.Id);
            documentfromDb.Should().BeNull();
        }
    }
}
