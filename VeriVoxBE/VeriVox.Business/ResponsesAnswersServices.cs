using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Repository.Access_DB;

namespace VeriVox.Business
{
    public class ResponsesAnswersServices
    {
        private readonly ResponsesAnswersRepository responsesAnswersDAL;

        public ResponsesAnswersServices(ResponsesAnswersRepository _responseAnswersDAL)
        {
            responsesAnswersDAL = _responseAnswersDAL;
        }

        public void CreateResponsesAnswer(ResponsesAnswersDto responsesAnswersDto)
        {
            var responseAnswerEntity = new ResponseAnswers
            {
                ResponseId = responsesAnswersDto.ResponseId,
                FormQuestionId = responsesAnswersDto.FormQuestionId,
                Answer = responsesAnswersDto.Answer
            };

            responsesAnswersDAL.CreateResponseAnswer(responseAnswerEntity);
        }

        public IEnumerable<ResponsesAnswersDto> GetResponsesAnswers()
        {
            // Retrieve response answers from the database using the DAL
            var responsesAnswerEntities = responsesAnswersDAL.GetResponseAnswers();

            // Convert the entities to DTOs
            var responseAnswerDtos = new List<ResponsesAnswersDto>();

            foreach (var entity in responsesAnswerEntities)
            {
                responseAnswerDtos.Add(new ResponsesAnswersDto
                {
                    ResponseId = entity.ResponseId,
                    FormQuestionId = entity.FormQuestionId,
                    Answer = entity.Answer
                });
            }

            return responseAnswerDtos;
        }

    }
}