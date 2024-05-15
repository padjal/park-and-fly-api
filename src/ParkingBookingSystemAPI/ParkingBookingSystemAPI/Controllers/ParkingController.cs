using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingBookingSystemAPI.Dtos.Requests;
using ParkingBookingSystemAPI.Models;
using System.Security.Claims;

namespace ParkingBookingSystemAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ParkingController : ControllerBase
    {
        private ApplicationDbContext _context;

        public ParkingController(ApplicationDbContext applicationDbContext) {
            _context = applicationDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parking>>> GetParkings()
        {
            return await _context.Parkings.ToListAsync();
        }

        [HttpGet("{parkingId}")]
        public async Task<ActionResult<Parking>> GetParkingById(int parkingId)
        {
            return await _context.Parkings.Where(p => p.Id == parkingId).FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<bool> AddParking(AddParkingRequest parkingRequest) { 

            _context.Parkings.Add(parkingRequest.ToBase());

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                // TODO: Log
                return false;
            }

            return true;
        }

        [HttpDelete("{parkingId}")]
        public async Task<IActionResult> DeleteParkingById(int parkingId)
        {
            try
            {
                var parking = await _context.Parkings.FirstOrDefaultAsync(p => p.Id == parkingId);

                _context.Parkings.Remove(parking);

                return Ok();
            }catch
            {
                return BadRequest();
            }
            
        }
    }
}
