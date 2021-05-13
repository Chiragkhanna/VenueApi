using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VenueApi.Interfaces;
using VenueApi.Models;

namespace VenueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenueConfigController : ControllerBase
    {
        private readonly IVenueService _venueService;
        private readonly ILogger<VenueConfigController> _logger;

        public VenueConfigController(ILogger<VenueConfigController> logger, IVenueService venueService)
        {
            _logger = logger;
            _venueService = venueService;
        }



        //[HttpGet("GetAll")]
        //public ActionResult<List<VenueConfiguration>> Get() =>
        //    _venueService.Get();

        [HttpGet("GetVenueConfiguration")]
        public ActionResult<VenueConfigurationRead> Get(string id)
        {
            var book = _venueService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }
        [HttpGet("GetSections")]
        public ActionResult<List<SeatingSection>> GetSections(int venueId)
        {
            var venue = _venueService.GetSections(venueId);

            if (venue == null)
            {
                return NotFound();
            }

            return Ok(new ApiOkResponse(venue));
        }
        [HttpGet("GetVenueMap")]
        public ActionResult<VenueConfigurationRead> GetVenueMap(int venueId)
        {
            var venue = _venueService.GetVenueMap(venueId);

            if (venue == null)
            {
                return NotFound();
            }

            return Ok(new ApiOkResponse(venue));
        }
        [HttpPost("create")]
        public ActionResult<VenueConfiguration> Create(VenueConfigurationCreate venue)
        {
            _venueService.Create(venue);

            return CreatedAtRoute("GetVenueConfig", new { id = venue.id }, venue);
        }

        //[HttpPut()]
        //public IActionResult Update(int id, VenueConfiguration bookIn)
        //{
        //    var venue = _venueService.Get(id);

        //    if (venue == null)
        //    {
        //        return NotFound();
        //    }

        //    _venueService.Update(id, bookIn);

        //    return NoContent();
        //}

        [HttpDelete("delete")]
        public IActionResult Delete(string id)
        {
            var venue = _venueService.Get(id);

            if (venue == null)
            {
                return NotFound();
            }

            _venueService.Remove(venue.id);

            return NoContent();
        }
        [HttpPost("deletebyVenue")]
        public IActionResult DeletebyVenue(int id)
        {
            _venueService.RemoveByVenueId(id);

            return NoContent();
        }
    }
}
