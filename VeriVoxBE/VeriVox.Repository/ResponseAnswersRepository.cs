using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VeriVox.Database.Context;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Repository.Access_DB
{
    public class ResponsesAnswersRepository
    {
        private readonly CFA_DbContext dbContext;

        public ResponsesAnswersRepository(CFA_DbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void CreateResponseAnswer(ResponseAnswers responseAnswer)
        {
            dbContext.ResponsesAnswers.Add(responseAnswer);
            dbContext.SaveChanges();
        }

        public IEnumerable<ResponseAnswers> GetResponseAnswers()
        {
            return dbContext.ResponsesAnswers.ToList();
        }
    }
}