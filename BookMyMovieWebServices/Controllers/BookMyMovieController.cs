using BookMyMovieDataAccessLayer;
using BookMyMovieDataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyMovieWebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookMyMovieController : Controller
    {
        private BookMyMovieRepository repository;
        public BookMyMovieController()
        {
            repository = new BookMyMovieRepository();
        }

        [HttpGet]
        public JsonResult GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();
            try
            {
                bookings = repository.GetAllBookings();
            }
            catch (Exception ex)
            {
                bookings = null;
            }

            return Json(bookings);
        }

        [HttpGet]
        public IActionResult GetUserDetails(int userId)
        {
            User user = new User();
            try
            {
                if(userId <= 0)
                {
                    return BadRequest("User Id must be greater than 0!");
                }
                user = repository.GetUserDetails(userId);
                if(user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("User not found!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public JsonResult AddMovie(Models.Movie movie)
        {
            bool result = false;
            try
            {
                if(ModelState.IsValid)
                {
                    Movie movieObj = new Movie();
                    movieObj.Title = movie.Title;
                    movieObj.Duration = movie.Duration;
                    movieObj.Genre = movie.Genre;
                    movieObj.ReleaseDate = movie.ReleaseDate;

                    result = repository.AddMovie(movieObj);
                }

            }
            catch (Exception ex)
            {
                result = false;
            }

            return Json(result);
        }

        [HttpPost]
        public IActionResult AddTheatre(Models.Theater theater)
        {
            bool result = false;
            try
            {
                if( ModelState.IsValid )
                {
                    Theater theaterObj = new Theater();
                    theaterObj.TotalSeats = theater.TotalSeats;
                    theaterObj.Name = theater.Name;
                    theaterObj.Location = theater.Location;

                    result = repository.AddTheatre(theaterObj);

                    if(result)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("Theatre Details could not be added!");
                    }

                }
                else
                {
                    return BadRequest("Input not in proper format!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpPut]
        public JsonResult UpdateUserDetails(Models.UserDetails user)
        {
            int result = 0;
            try
            {
                if(ModelState.IsValid)
                {
                    User userObj = new User();
                    userObj.Email = user.Email;
                    userObj.UserId = user.UserId;
                    userObj.ContactNumber = user.ContactNumber;

                    result = repository.UpdateUserDetails(userObj);
                }
            }
            catch (Exception ex)
            {
                result = -99;
            }

            return Json(result);
            
        }

        [HttpDelete]
        public IActionResult CancelBooking(int bookingId)
        {
            int result = 0;
            try
            {
                result = repository.CancelBooking(bookingId);
                if(result == 1)
                {
                    return Ok("Booking cancelled successfully!");
                }
                else if(result == -1)
                {
                    return NotFound("Booking not found!");
                }
                else
                {
                    return BadRequest("Booking could not be cancelled!");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public JsonResult DeleteUser(int userId)
        {
            bool result = false;
            try
            {
                result = repository.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                result = false;
            }

            return Json(result);
        }
    }
}
