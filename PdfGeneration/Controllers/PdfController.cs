using Microsoft.AspNetCore.Mvc;
using PdfGeneration.Models;
using PdfGeneration.Service;

namespace PdfGeneration.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class PdfController : ControllerBase {

        private readonly IInputModelService _inputModelService;

        public PdfController(IInputModelService inputModelService) {

            _inputModelService = inputModelService;

        }

        [HttpPost]
        public IActionResult GeneratePdf([FromBody] InputModel input) {

            if(input == null)
                return BadRequest("Input Empty");

            var result = _inputModelService.GeneratePdf(input);

            if(result == null)
                return BadRequest("Generation Error");

            return File(result, "application/pdf", "generated.pdf");
        
        }

    }
}
