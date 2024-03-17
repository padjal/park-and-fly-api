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
    }
}
