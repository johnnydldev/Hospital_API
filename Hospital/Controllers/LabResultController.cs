using DAOControllers.ManagerControllers;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LabResultController(ILogger<LabResultController> logger,
        IGenericRepository<LaboratoryResult> labResult) : Controller
    {

        private readonly ILogger<LabResultController> _logger = logger;
        private readonly IGenericRepository<LaboratoryResult> _labResultRepository = labResult;

        [HttpGet]
        public async Task<List<LaboratoryResult>> List()
        {
            List<LaboratoryResult> _listLabResults = await _labResultRepository.GetAll();

            if (_listLabResults.Count > 0)
            {
                Console.WriteLine(Ok(_listLabResults));
            }

            return _listLabResults;
        }//End list medicament

        //Get
        [HttpGet]
        [Route("GetLabResult/{id}")]
        public async Task<LaboratoryResult> GetLabResult(int idlabResult)
        {
            LaboratoryResult labResult = await _labResultRepository.GetById(idlabResult);

            return labResult;
        }//End get medicament by id

        //Post
        [HttpPost]
        public async Task<IActionResult> CreateLabResult([FromBody] LaboratoryResult labResult)
        {
            int response = await _labResultRepository.Create(labResult);

            return Ok(new { response });
        }//End create medicament

        [HttpPut]
        [Route("UpdateLabResult")]
        public async Task<IActionResult> UpdateLabResult([FromBody] LaboratoryResult labResult)
        {
            bool response = await _labResultRepository.Edit(labResult);

            return Ok(new { response });
        }

        [HttpDelete]
        [Route("DeleteLabResult/{id}")]
        public async Task<IActionResult> DeleteMedicament(int idLabResult)
        {
            bool response = await _labResultRepository.Delete(idLabResult);

            return Ok(new { response });
        }//End delete Lab Result


    }//End lab result controller class
}//End namespace
