using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingBookingSystemAPI.Dtos;
using ParkingBookingSystemAPI.Dtos.Requests;
using ParkingBookingSystemAPI.Models;
using System.Net;
using System.Security.Claims;

namespace ParkingBookingSystemAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CarController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        [HttpPost]
        public async Task<bool> AddCar(AddCarRequest carRequest)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _context.Cars.Add(carRequest.ToBase(userId));

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
