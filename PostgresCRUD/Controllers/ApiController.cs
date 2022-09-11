using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PostgresCRUD.DataAccess;
using PostgresCRUD.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PostgresCRUD.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/v1/personeller")]
    public class ApiController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public ApiController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        /*[HttpGet("alljoin")]
        public IList GetAllJoinRecords()
        {
            return _dataAccessProvider.GetAllJoinRecords();
        }*/

        [HttpGet("{id}")]
        public Personels GetSinglePersonel(int id)
        {
            return _dataAccessProvider.GetSinglePersonel(id);
        }

        [HttpGet("all")]
        public IList GetAllPersonels()
        {
            return _dataAccessProvider.GetAllPersonels();
        }

        [HttpGet("egitim/{id}")]
        public IList GetAllEgitim(int id)
        {
            return _dataAccessProvider.GetAllEgitim(id);
        }

        [HttpGet("tablo/{tablo}")]
        public IList Kodlar(string tablo)
        {
            return _dataAccessProvider.Kodlar(tablo);
        }

        [HttpGet("birim")]
        public IList Birim(int Id)
        {
            return _dataAccessProvider.Birim();
        }

        [HttpGet("gorev")]
        public IList Gorev(int Id)
        {
            return _dataAccessProvider.Gorev();
        }

        [HttpGet("okul")]
        public IList Okul()
        {
            return _dataAccessProvider.Okul();
        }

        [HttpGet("bolum")]
        public IList Bolum()
        {
            return _dataAccessProvider.Bolum();
        }

        [HttpPost("add/personel")]
        public IActionResult AddPersonel([FromBody] Personels personel)
        {
            /*if (ModelState.IsValid)
            {
                _dataAccessProvider.AddPersonel(personel);
                return Ok();
            }
            return BadRequest();*/
            _dataAccessProvider.AddPersonel(personel);
            return Ok();
        }

        [HttpPut("update/personel")]
        public IActionResult UpdatePersonel([FromBody] Personels personel)
        {
            /*if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdatePatientRecord(patient);
                return Ok();
            }*/
            _dataAccessProvider.UpdatePersonel(personel);
            return Ok();
        }

        [HttpDelete("delete/personel/{id}")]
        public IActionResult DeletePersonel(int id)
        {
            _dataAccessProvider.DeletePersonel(id);
            return Ok();
        }

        [HttpPost("add/egitim")]
        public IActionResult AddEgitim([FromBody] Egitim egitim)
        {
            /*if (ModelState.IsValid)
            {
                _dataAccessProvider.AddPersonel(personel);
                return Ok();
            }
            return BadRequest();*/
            _dataAccessProvider.AddEgitim(egitim);
            return Ok();
        }

        [HttpPut("update/egitim")]
        public IActionResult UpdateEgitim([FromBody] Egitim egitim)
        {
            /*if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdatePatientRecord(patient);
                return Ok();
            }*/
            _dataAccessProvider.UpdateEgitim(egitim);
            return Ok();
        }

        [HttpDelete("delete/egitim/{id}")]
        public IActionResult DeleteEgitim(int id)
        {
            _dataAccessProvider.DeleteEgitim(id);
            return Ok();
        }

        [HttpPost("upload")]
        public string Upload(IFormFile file)
        {
            return _dataAccessProvider.Upload(file);
            //return Ok();
        }
    }
}