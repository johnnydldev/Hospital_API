using DAOControllers.ManagerControllers;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController(ILogger<PatientController> logger,
        IGenericRepository<Patient> patient) : Controller
    {

        private readonly ILogger<PatientController> _logger = logger;
        private readonly IGenericRepository<Patient> _patientRepository = patient;


        [HttpGet]
        public async Task<List<Patient>> List()
        {
            List<Patient> _listPatient = await _patientRepository.GetAll();

            if (_listPatient.Count > 0)
            {
                Console.WriteLine(Ok(_listPatient));
            }

            return _listPatient;
        }//End list patient

        //Get
        [HttpGet]
        [Route("GetPatient/{id}")]
        public async Task<Patient> GetMedicament(int idPatient)
        {
            Patient patient = await _patientRepository.GetById(idPatient);

            return patient;
        }//End get patient by id

        //Post
        [HttpPost]
        public async Task<IActionResult> CreateMedicament([FromBody] Patient patient)
        {
            int response = await _patientRepository.Create(patient);

            return Ok(new { response });
        }//End create patient

        [HttpPut]
        [Route("UpdatePatient")]
        public async Task<IActionResult> UpdateDoctor([FromBody] Patient patient)
        {
            bool response = await _patientRepository.Edit(patient);

            return Ok(new { response });
        }//End update patient

        [HttpDelete]
        [Route("DeletePatient/{id}")]
        public async Task<IActionResult> DeleteDoctor(int idPatient)
        {
            bool response = await _patientRepository.Delete(idPatient);

            return Ok(new { response });
        }//End delete patient


    }//End patient controller class
}//End namespace
