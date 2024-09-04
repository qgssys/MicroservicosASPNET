using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Services;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {


        private readonly ILogger<PersonController> _logger;

        //injetando na controller o serviço de implementation de ipersonservice
        private IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;

            //aqui recebo no construtor a injeçao de dependencia
            _personService = personService;
        }

        //
        [HttpGet]
        public IActionResult Get()
        {


            return Ok(_personService.FindAll());
        }

        //
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindByID(id);

            if(person == null) return NotFound(); 
            return Ok(person);

        }

        //
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {

            if (person == null) return BadRequest();
            return Ok(_personService.Create(person));

        }

        //
        [HttpPut]
        public IActionResult Put([FromBody] Person person)//put é atualizar - update
        {

            if (person == null) return BadRequest();
            return Ok(_personService.Update(person));

        }

        //
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();

        }





        ////SUBTRAÇÃO
        //[HttpGet("subtraction/{firstNumber}/{secondNumber}")]
        //public IActionResult GetSubtract(string firstNumber, string secondNumber)
        //{
        //    if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        //    {
        //        var subtraction = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);

        //        return Ok(subtraction.ToString());
        //    }

        //    return BadRequest("Invalid Input");
        //}

        ////MULTIPLICAÇÃO
        //[HttpGet("multiplication/{firstNumber}/{secondNumber}")]
        //public IActionResult GetMult(string firstNumber, string secondNumber)
        //{
        //    if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        //    {
        //        var multiplication = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);

        //        return Ok(multiplication.ToString());
        //    }

        //    return BadRequest("Invalid Input");
        //}

        ////DIVISAO
        //[HttpGet("division/{firstNumber}/{secondNumber}")]
        //public IActionResult GetDivisao(string firstNumber, string secondNumber)
        //{
        //    if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        //    {
        //        var division = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);

        //        return Ok(division.ToString());
        //    }

        //    return BadRequest("Invalid Input");
        //}


        ////MEDIA
        //[HttpGet("mean/{firstNumber}/{secondNumber}")]
        //public IActionResult GetMedia(string firstNumber, string secondNumber)
        //{
        //    if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        //    {
        //        var mean = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;

        //        return Ok(mean.ToString());
        //    }

        //    return BadRequest("Invalid Input");
        //}


        ////RAIZ QUADRADA
        //[HttpGet("square-root/{firstNumber}")]
        //public IActionResult GetPotencia(string firstNumber)
        //{
        //    if (IsNumeric(firstNumber) && IsNumeric(firstNumber))
        //    {
        //        var squareRoot = Math.Sqrt((double)ConvertToDecimal(firstNumber));

        //        return Ok(squareRoot.ToString());
        //    }

        //    return BadRequest("Invalid Input");
        //}

        ////VERIFICA SE É UM NUMERO
        //private bool IsNumeric(string strNumber)
        //{
        //    double number;
        //    bool isNumber = double.TryParse(
        //        strNumber,
        //        System.Globalization.NumberStyles.Any,
        //        System.Globalization.CultureInfo.InvariantCulture,
        //        out number
        //        );

        //    return isNumber;
        //}

        ////CONVERTE PARA DECIMAL
        //private decimal ConvertToDecimal(string strNumber)
        //{
        //    decimal decimalValue;

        //    if(decimal.TryParse(strNumber, out decimalValue))
        //    {
        //        return decimalValue;
        //    }

        //    return 0;
        //}


    }
}
