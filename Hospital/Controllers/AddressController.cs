using Microsoft.AspNetCore.Mvc;
using DAOControllers.ManagerControllers;
using Models;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController(ILogger<AddressController> logger,
        IGenericRepository<Address> address) : Controller
    {

        private readonly ILogger<AddressController> _logger = logger;
        private readonly IGenericRepository<Address> _addressRepository = address;

        [HttpGet]
        public async Task<List<Address>> List()
        {
            List<Address> _listAddress = await _addressRepository.GetAll();

            if (_listAddress.Count > 0)
            {
                Console.WriteLine(Ok(_listAddress));
            }

            return _listAddress;
        }//End list address

        //Get
        [HttpGet]
        [Route("GetAddress/{id}")]
        public async Task<Address> GetAddress(int idAddress)
        {
            Address address = await _addressRepository.GetById(idAddress);

            return address;
        }//End get address by id

        //Post
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] Address address)
        {
            int response = await _addressRepository.Create(address);

            return Ok(new { response });
        }//End create address

        [HttpPut]
        [Route("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress([FromBody] Address address)
        {
            bool response = await _addressRepository.Edit(address);

            return Ok(new { response });
        }

        [HttpDelete]
        [Route("DeleteAddress/{id}")]
        public async Task<IActionResult> DeleteAddress(int idAddress)
        {
            bool response = await _addressRepository.Delete(idAddress);

            return Ok(new { response });
        }


    }//End address controller class
}//End namespace
