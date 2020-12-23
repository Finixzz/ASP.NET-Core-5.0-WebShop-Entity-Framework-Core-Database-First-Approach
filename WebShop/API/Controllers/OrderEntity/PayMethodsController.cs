using AutoMapper;
using DAL.Dtos.PayMethodDTOS;
using DAL.Models;
using DAL.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{


    /*
        <summary>
               PayMethods controller enables only 2 methods:
               GetAllPayMethodsAsync and GetPayMethodByIdAsync(id).
               Other api calls are not allowed!
        </summary>
    */
    [Route("api/[controller]")]
    [ApiController]
    public class PayMethodsController : ControllerBase
    {
        private IPayMethodSQLRepository _payMethodRepository;
        private IMapper _mapper;


        public PayMethodsController(IPayMethodSQLRepository _payMethodRepository,
                                    IMapper _mapper)
        {
            this._payMethodRepository = _payMethodRepository;
            this._mapper = _mapper;
        }


        /*
            <summary>
                   Returns all paymethods
            </summary>
            <remarks>
            Sample request:
                GET /api/paymethods
           </remarks>
           <response code="200">Returns paymethod info if okay</response>
        */
        [HttpGet]
        public async Task<IActionResult> GetAllPayMethodsAsync()
        {
            var payMethodDTOs = _mapper.Map<IEnumerable<PayMethod>, IEnumerable<PayMethodDTO>>
                                    (await _payMethodRepository.GetAllAsync());
            return Ok(payMethodDTOs);
        }



        /*
            <summary>
                   Returns single pay method
            </summary>
            <remarks>
            Sample request:
                GET /api/paymethods/1
           </remarks>
           <response code="200">Returns paymethod info if found</response>
           <response code="404">If something goes wrong</response> 
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayMethodByIdAsync(int id)
        {
            PayMethod payMethodInDb = await _payMethodRepository.GetById(id);

            if (payMethodInDb == null)
                return NotFound();

            return Ok(_mapper.Map<PayMethod, PayMethodDTO>(payMethodInDb));
        }
    }
}
