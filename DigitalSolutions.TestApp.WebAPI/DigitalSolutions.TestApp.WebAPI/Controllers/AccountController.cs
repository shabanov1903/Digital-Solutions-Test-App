using AutoMapper;
using DigitalSolutions.TestApp.WebAPI.DataBase;
using DigitalSolutions.TestApp.WebAPI.DataBase.DBContext;
using DigitalSolutions.TestApp.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DigitalSolutions.TestApp.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IData _dataController;
        private readonly IMapper _mapper;
        public AccountController(IData dataController, IMapper mapper)
        {
            _dataController = dataController;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("get/filter")]
        public async Task<IActionResult> GetAccountList([FromBody] FilterDTO filter)
        {
            var data = await _dataController.GetAccountList(_mapper.Map<AccountFilter>(filter));
            return Ok(data);
        }

        [HttpPost]
        [Route("get/{id}")]
        public async Task<IActionResult> GetAccountById([FromRoute] int id)
        {
            var data = await _dataController.GetAccountById(id);
            return Ok(data);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAccount([FromBody] AccountDTO account)
        {
            var result = await _dataController.AddAccount(_mapper.Map<AccountContext>(account));
            return Ok(new OperationDTO(result));
        }

        [HttpPost]
        [Route("change")]
        public async Task<IActionResult> ChangeAccount([FromBody] AccountDTO account)
        {
            await _dataController.ChangeAccount(_mapper.Map<AccountContext>(account));
            return Ok();
        }
    }
}
