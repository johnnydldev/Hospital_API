using DAOControllers.ManagerControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicamentController(ILogger<MedicamentController> logger,
        IGenericRepository<Medicament> medicament) : Controller
    {
        private readonly ILogger<MedicamentController> _logger = logger;
        private readonly IGenericRepository<Medicament> _medicamentRepository = medicament;

        [HttpGet]
        public async Task<List<Medicament>> List()
        {
            List<Medicament> _listMedicament = await _medicamentRepository.GetAll();

            if (_listMedicament.Count > 0)
            {
                Console.WriteLine(Ok(_listMedicament));
            }

            return _listMedicament;
        }//End list medicament

        //Get
        [HttpGet]
        [Route("GetMedicament/{id}")]
        public async Task<Medicament> GetMedicament(int idMedicament)
        {
            Medicament medicament = await _medicamentRepository.GetById(idMedicament);

            return medicament;
        }//End get medicament by id

        //Post
        [HttpPost]
        public async Task<IActionResult> CreateMedicament([FromBody] Medicament medicament)
        {
            int response = await _medicamentRepository.Create(medicament);

            return Ok(new { response });
        }//End create medicament

        [HttpPut]
        [Route("UpdateMedicament")]
        public async Task<IActionResult> UpdateMedicamet([FromBody] Medicament medicament)
        {
            bool response = await _medicamentRepository.Edit(medicament);

            return Ok(new { response });
        }

        [HttpDelete]
        [Route("DeleteMedicament/{id}")]
        public async Task<IActionResult> DeleteMedicament(int idMedicament)
        {
            bool response = await _medicamentRepository.Delete(idMedicament);

            return Ok(new { response });
        }//End delete medicament


    }//End medicament controller class
}//End nanmespace
