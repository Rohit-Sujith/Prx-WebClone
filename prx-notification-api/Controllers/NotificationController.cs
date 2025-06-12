using Microsoft.AspNetCore.Mvc;
using SimpleNotificationMvcApi.Models;

namespace SimpleNotificationMvcApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationsController : ControllerBase
    {
        // Simple in-memory store for demo purpose
        private static readonly List<Notification> Notifications = new();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Notifications);
        }

        [HttpPost]
        public IActionResult Create([FromBody] NotificationCreateRequest request)
        {
            var newNotification = new Notification
            {
                Id = Notifications.Count + 1,
                Title = request.Title,
                Message = request.Message,
                CreatedAt = DateTime.UtcNow
            };
            Notifications.Add(newNotification);
            return CreatedAtAction(nameof(GetById), new { id = newNotification.Id }, newNotification);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var notification = Notifications.FirstOrDefault(n => n.Id == id);
            if (notification == null)
                return NotFound();

            return Ok(notification);
        }
    }

   
}
