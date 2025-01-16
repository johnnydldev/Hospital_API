using Microsoft.AspNetCore.Mvc;
using Models;
using DAOControllers.ManagerControllers;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController(ILogger<DoctorController> logger,
        IGenericRepository<Doctor> doctor) : Controller
    {

        private readonly ILogger<DoctorController> _logger = logger;
        private readonly IGenericRepository<Doctor> _doctorRepository = doctor;


        [HttpGet]
        public async Task<List<Doctor>> List()
        {
            List<Doctor> _listDoctor = await _doctorRepository.GetAll();

            if (_listDoctor.Count > 0)
            {
                Console.WriteLine(Ok(_listDoctor));
            }

            return _listDoctor;
        }//End list doctor

        //Get
        [HttpGet]
        [Route("GetDoctor/{id}")]
        public async Task<Doctor> GetMedicament(int idDoctor)
        {
            Doctor doctor = await _doctorRepository.GetById(idDoctor);

            return doctor;
        }//End get doctor by id

        //Post
        [HttpPost]
        public async Task<IActionResult> CreateMedicament([FromBody] Doctor doctor)
        {
            int response = await _doctorRepository.Create(doctor);

            return Ok(new { response });
        }//End create doctor

        [HttpPut]
        [Route("UpdateDoctor")]
        public async Task<IActionResult> UpdateDoctor([FromBody] Doctor doctor)
        {
            bool response = await _doctorRepository.Edit(doctor);

            return Ok(new { response });
        }//End update doctor

        [HttpDelete]
        [Route("DeleteDoctor/{id}")]
        public async Task<IActionResult> DeleteDoctor(int idDoctor)
        {
            bool response = await _doctorRepository.Delete(idDoctor);

            return Ok(new { response });
        }//End delete doctor




    }//End doctor controller class
}//End namespace
