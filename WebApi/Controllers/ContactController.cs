using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary;
using WebApi.Model;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {

        private readonly IContactRepository _ContactRepository;
        private readonly ILogger _logger;

        public ContactController(IContactRepository ContactRepository, ILogger<ContactController> logger)
        {
            _ContactRepository = ContactRepository;
            _logger = logger;
        }
       
        
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            try
            {
                return _ContactRepository.GetAll();
            }
            catch(Exception ex)
            {
                _logger.LogError( "Getting all item. ex: ",ex);
                return null;
            }
            
        }

        [HttpGet("{id}/{Name}/{email}/{phone}", Name = "GetContact")]
        public IActionResult GetById(int id, string name, string email, int phone)
        {
            try
            {
                IEnumerable<Contact> items = null;
                if (id > 0)
                    items = _ContactRepository.Find(id);
                else if (!string.IsNullOrEmpty(name) && name != "0")
                    items = _ContactRepository.FindByName(name);
                else if (!string.IsNullOrEmpty(email) && email != "0")
                    items = _ContactRepository.FindByEmail(email);
                else if (phone > 0)
                    items = _ContactRepository.FindByPhone(phone);

                if (items == null || items.Count() == 0)
                {
                    return NotFound();
                }


                return new ObjectResult(items);
            }
            catch (Exception ex)
            {
                _logger.LogError("Getting item. ex: ", ex);
                return NotFound();
            }
           
        }
        [HttpGet]
        [Route("email")] // <- no route parameters specified
        public IActionResult GetByCoordinates([FromQuery]decimal xCoordinate, [FromQuery]decimal yCoordinate)
        {
            // will be matched by e.g.
            // /api/1.0/availabilities?xCoordinate=34.3444&yCoordinate=66.3422
            return null;
        }


        [HttpPost]
        public IActionResult Create([FromBody] Contact Contact)
        {
            try
            {
                if (Contact == null)
                {
                    return BadRequest();
                }

                _ContactRepository.Add(Contact);

                return CreatedAtRoute("GetContact", new { id = Contact.ID }, Contact);
            }
            catch (Exception ex)
            {
                _logger.LogError("Create item. ex: ", ex);
                return null;
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Contact Contact)
        {
            try
            {
                if (Contact == null || Contact.ID != id)
                {
                    return BadRequest();
                }

                var ContactToUpd = _ContactRepository.Find(id).FirstOrDefault();
                if (ContactToUpd == null)
                {
                    return NotFound();
                }

                ContactToUpd.Name = Contact.Name;
                ContactToUpd.Gender = Contact.Gender;
                ContactToUpd.Address = Contact.Address;
                ContactToUpd.BirthDate = Contact.BirthDate;
                ContactToUpd.PhoneNumber = Contact.PhoneNumber;
                ContactToUpd.Email = Contact.Email;

                _ContactRepository.Update(ContactToUpd);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError("Update item. ex: ", ex);
                return null;
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ContactToDel = _ContactRepository.Find(id);
                if (ContactToDel == null)
                {
                    return NotFound();
                }

                _ContactRepository.Remove(id);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError("Delete item. ex: ", ex);
                return null;
            }
           
        }


    }
}
