using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Repository.Access_DB;

namespace VeriVox.Business
{
    public class ResponsesServices
    {
        private readonly ResponsesRepository responsesDAL;

        public ResponsesServices(ResponsesRepository _responsesDAL)
        {
            responsesDAL = _responsesDAL;
        }

        public void CreateResponse(ResponsesDto responsesDto)
        {
            var responseEntity = new Responses
            {
                LinkId = responsesDto.LinkId
            };

            responsesDAL.CreateResponse(responseEntity);
        }

        public IEnumerable<ResponsesDto> GetResponses()
        {
            // Retrieve responses from the database using the DAL
            var responseEntities = responsesDAL.GetResponses();

            // Convert the entities to DTOs
            var responseDtos = new List<ResponsesDto>();

            foreach (var entity in responseEntities)
            {
                responseDtos.Add(new ResponsesDto
                {
                    LinkId = entity.LinkId
                });
            }

            return responseDtos;
        }

    }
}