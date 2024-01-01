
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAutomation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        GalleryManager _gMan = new GalleryManager(new EfGalleryRepository());
        [HttpGet]
        public IActionResult GetGallery()
        {
            var values = _gMan.GetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult GetGallery(Gallery gallery)
        {
            _gMan.TAdd(gallery);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetPhoto(int id)
        {
            var values = _gMan.GetById(id);
            if (values == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(values);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePhoto(int id)
        {
            var values = _gMan.GetById(id);
            if (values == null)
            {
                return NotFound();
            }
            else
            {
                _gMan.TDelete(values);
                return Ok(values);
            }
        }
        [HttpPut]
        public IActionResult PhotoUpdate(Gallery gallery)
        {
            var values = _gMan.GetById(gallery.PhotoId);
            if (values == null)
            {
                return NotFound();
            }
            else
            {
                values.PhotoName = gallery.PhotoName;
                values.PhotoImage = gallery.PhotoImage;
                _gMan.TUpdate(values);
                return Ok(values);
            }
        }
    }
}
