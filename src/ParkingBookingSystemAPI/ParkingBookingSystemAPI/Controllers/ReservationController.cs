using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingBookingSystemAPI.Dtos;
using ParkingBookingSystemAPI.Models;

namespace ParkingBookingSystemAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ReservationController(ApplicationDbContext context, ILogger<ReservationController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Reservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations(int? parkingId)
        {
            if(parkingId == null)
            {
                return await _context.Reservations.ToListAsync();
            }

            return await _context.Reservations.Where(r => r.ParkingId==parkingId) .ToListAsync();
        }

        /*[HttpGet("{parkingId:int}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsByParking([FromQuery] int parkingId)
        {
            var reservations = await _context.Reservations.Where(r => r.ParkingId == parkingId).ToListAsync();

            return reservations;
        }*/
            
        // GET: api/Reservation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservation(string id)
        {
            var reservation = await _context.Reservations.FindAsync(new Guid(id));

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation.ToDto();
        }

        // PUT: api/Reservation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(Guid id, ReservationDto reservationDto)
        {
            var reservation = _context.Reservations.Find(id);

            if(reservation == null)
            {
                return NotFound();
            }

            reservation.UserId = "dsfsdf";
            reservation.ParkingId = reservationDto.ParkingId;
            reservation.CarId = reservationDto.CarId;
            reservation.From = reservationDto.From;
            reservation.To = reservationDto.To;

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(ReservationDto reservationDto)
        {
            //Get userId from
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var reservation = reservationDto.ToBase(userId);

            _context.Reservations.Add(reservation);

            //Check if parking exists
            var foundParking = await _context.Parkings.SingleOrDefaultAsync(p => p.Id  == reservationDto.ParkingId);

            if (foundParking == null)
            {
                return BadRequest("Parking with this id not found.");
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                // TODO: Log
                return null;
            }

            _logger.LogInformation($"New reservation added --> {reservation}");
            return Ok();
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(string id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(Guid id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
