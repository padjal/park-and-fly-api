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

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations(int? parkingId, DateTime? startTime, DateTime? endTime)
        {
            if (startTime == null)
            {
                startTime = DateTime.MinValue;
            }

            if (endTime == null)
            {
                endTime = DateTime.MaxValue;
            }

            if (parkingId == null)
            {
                return await _context.Reservations
                    .Where(r => r.From >= startTime &&
                       r.To <= endTime)
                    .ToListAsync();
            }

            return await _context.Reservations
                .Where(r => r.ParkingId == parkingId &&
                       r.From >= startTime &&
                       r.To <= endTime)
                .ToListAsync();
        }

        [HttpGet("available")]
        public async Task<ActionResult<bool>> IsSlotAvalable(int parkingId, DateTime? startTime, DateTime? endTime)
        {
            if (parkingId == 0 || startTime == null || endTime == null)
            {
                return BadRequest("Check request parameters.");
            }
            var parking = await _context.Parkings.FirstOrDefaultAsync(p => p.Id == parkingId);

            if(parking == null)
            {
                return NotFound($"Parking with id {parkingId} is not found.");
            }

            return await _context.Reservations.CountAsync(r => r.ParkingId == parkingId &&
                       r.From <= endTime &&
                       r.To >= startTime) < parking.MaxCars;
        }

        // GET: api/Reservation
        [HttpGet("current")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations(int parkingId, DateTime now)
        {
            return await _context.Reservations
                .Where(r => r.ParkingId == parkingId &&
                r.From <= now &&
                r.To >= now)
                .ToListAsync();
        }

        // GET: api/Reservation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservation(string id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

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
