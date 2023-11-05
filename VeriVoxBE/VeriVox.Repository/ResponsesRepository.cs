using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VeriVox.Database.Context;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Repository.Access_DB
{
    public class ResponsesRepository
    {
        private readonly CFA_DbContext dbContext;

        public ResponsesRepository(CFA_DbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void CreateResponse(Responses response)
        {
            dbContext.Responses.Add(response);
            dbContext.SaveChanges();
        }

        public IEnumerable<Responses> GetResponses()
        {
            return dbContext.Responses.ToList();
        }
    }
}