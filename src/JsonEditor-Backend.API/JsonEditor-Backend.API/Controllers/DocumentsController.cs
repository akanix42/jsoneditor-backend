using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JsonEditor_Backend.API.Models;
using JsonEditor_Backend.API.Repositories;

namespace JsonEditor_Backend.API.Controllers
{
    public class DocumentsController : ApiController
    {
        private readonly IDocumentsRepository documentsRepository;

        public DocumentsController(IDocumentsRepository documentsRepository)
        {
            this.documentsRepository = documentsRepository;
        }

        // GET api/documents
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/documents/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/documents
        public int Post(Document document)
        {
            return documentsRepository.Add(document);
        }

        // PUT api/documents/5
        public void Put(int id, Document document)
        {
        }

        // DELETE api/documents/5
        public void Delete(int id)
        {
        }
    }
}
