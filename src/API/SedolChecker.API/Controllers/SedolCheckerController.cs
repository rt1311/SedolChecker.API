using Microsoft.AspNetCore.Mvc;
using SedolChecker.Core.Interfaces;

namespace SedolCheckerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SedolCheckerController : ControllerBase
    {
        private readonly ISedolValidator _sedolValidator;
        public SedolCheckerController(ISedolValidator sedolValidator)
        {
            _sedolValidator = sedolValidator;
        }


        [Route("{input}")]
        [HttpGet]
        public IActionResult CheckSedol(string input)
        {
            try
            {
                var validationResult=_sedolValidator.ValidateSedol(input);
                return Ok(validationResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
