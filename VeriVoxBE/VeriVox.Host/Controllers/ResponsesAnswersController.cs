using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Business;

namespace VeriVox.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsesAnswersController : ControllerBase
    {
        private readonly ResponsesAnswersServices responsesAnswersBLL;

        public ResponsesAnswersController(ResponsesAnswersServices _responseAnswersBLL)
        {
            responsesAnswersBLL = _responseAnswersBLL; 
        }

        [HttpPost]
        public async Task<ActionResult<ResponsesAnswersDto>> PostResponsesAnswers(ResponsesAnswersDto responsesAnswersDto)
        {
            try
            {
                responsesAnswersBLL.CreateResponsesAnswer(responsesAnswersDto);
                return Ok(responsesAnswersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<ResponsesAnswersDto>> GetResponsesAnswers()
        {
            try
            {
                var responsesAnswerDtos = responsesAnswersBLL.GetResponsesAnswers();
                return Ok(responsesAnswerDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}