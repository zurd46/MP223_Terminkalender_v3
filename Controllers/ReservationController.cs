using Microsoft.AspNetCore.Mvc;
using RaumReservierungsSystem.Data;
using RaumReservierungsSystem.Models;
using System.IO;
using System.Linq;

namespace RaumReservierungsSystem.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Seite für neue Reservation
        public IActionResult Create()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Reservation", "Create.html");
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Die Datei Create.html wurde nicht gefunden.");
            }

            var htmlContent = System.IO.File.ReadAllText(filePath);
            if (Request.Query.ContainsKey("error"))
            {
                string errorMessage = Request.Query["error"];
                htmlContent = htmlContent.Replace("{{ErrorMessage}}", errorMessage);
            }
            else
            {
                htmlContent = htmlContent.Replace("{{ErrorMessage}}", string.Empty);
            }

            return Content(htmlContent, "text/html");
        }

        [HttpPost]
        public IActionResult Create(Reservation reservation)
        {
            // Validierung der Zeitangaben
            if (reservation.StartTime >= reservation.EndTime)
            {
                return Redirect($"/Reservation/Create?error=Die Endzeit muss nach der Startzeit liegen.");
            }

            // Überprüfung auf Überschneidungen
            var overlappingReservation = _context.Reservations
                .Where(r => r.RoomNumber == reservation.RoomNumber && r.Date == reservation.Date)
                .AsEnumerable() // Daten aus der Datenbank holen
                .FirstOrDefault(r =>
                    (reservation.StartTime >= r.StartTime && reservation.StartTime < r.EndTime) ||
                    (reservation.EndTime > r.StartTime && reservation.EndTime <= r.EndTime) ||
                    (reservation.StartTime <= r.StartTime && reservation.EndTime >= r.EndTime));

            if (overlappingReservation != null)
            {
                return Redirect($"/Reservation/Create?error=Das Zimmer {reservation.RoomNumber} ist im angegebenen Zeitraum bereits reserviert.");
            }

            // Speichern der Reservation
            reservation.Id = Guid.NewGuid();
            reservation.PrivateKey = Guid.NewGuid().ToString();
            reservation.PublicKey = Guid.NewGuid().ToString();

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            TempData["PrivateKey"] = reservation.PrivateKey;
            TempData["PublicKey"] = reservation.PublicKey;

            return RedirectToAction("Success");
        }

        // Erfolgsseite nach Erstellung
        public IActionResult Success()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Reservation", "Success.html");
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Die Datei Success.html wurde nicht gefunden.");
            }

            var htmlContent = System.IO.File.ReadAllText(filePath);
            htmlContent = htmlContent.Replace("{{PrivateKey}}", TempData["PrivateKey"]?.ToString() ?? "Nicht verfügbar");
            htmlContent = htmlContent.Replace("{{PublicKey}}", TempData["PublicKey"]?.ToString() ?? "Nicht verfügbar");

            return Content(htmlContent, "text/html");
        }

        public IActionResult Edit(Guid id)
        {
            // Reservation anhand der ID aus der Datenbank suchen
            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound("Reservation nicht gefunden.");
            }

            // HTML-Datei laden
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Reservation", "Edit.html");
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Die Datei Edit.html wurde nicht gefunden.");
            }

            // Platzhalter in der HTML-Datei durch tatsächliche Werte ersetzen
            var htmlContent = System.IO.File.ReadAllText(filePath);
            htmlContent = htmlContent.Replace("{{Id}}", reservation.Id.ToString());
            htmlContent = htmlContent.Replace("{{Participants}}", reservation.Participants);
            htmlContent = htmlContent.Replace("{{RoomNumber}}", reservation.RoomNumber.ToString());
            htmlContent = htmlContent.Replace("{{Date}}", reservation.Date.ToString("yyyy-MM-dd"));
            htmlContent = htmlContent.Replace("{{StartTime}}", reservation.StartTime.ToString(@"hh\:mm"));
            htmlContent = htmlContent.Replace("{{EndTime}}", reservation.EndTime.ToString(@"hh\:mm"));
            htmlContent = htmlContent.Replace("{{Remarks}}", reservation.Remarks.ToString());
            htmlContent = htmlContent.Replace("{{PrivateKey}}", reservation.PrivateKey.ToString());
            htmlContent = htmlContent.Replace("{{PublicKey}}", reservation.PublicKey.ToString());

            // HTML-Seite zurückgeben
            return Content(htmlContent, "text/html");
        }

        [HttpPost]
        public IActionResult Edit(Reservation reservation)
        {
            var existingReservation = _context.Reservations.FirstOrDefault(r => r.Id == reservation.Id);
            if (existingReservation == null)
            {
                return NotFound("Reservation nicht gefunden.");
            }

            existingReservation.Participants = reservation.Participants;
            existingReservation.RoomNumber = reservation.RoomNumber;
            existingReservation.Date = reservation.Date;
            existingReservation.StartTime = reservation.StartTime;
            existingReservation.EndTime = reservation.EndTime;

            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound("Reservation nicht gefunden.");
            }

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult InvalidKey()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Reservation", "InvalidKey.html");
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Die Datei InvalidKey.html wurde nicht gefunden.");
            }

            var htmlContent = System.IO.File.ReadAllText(filePath);
            return Content(htmlContent, "text/html");
        }

        // Zugang basierend auf Schlüssel
        public IActionResult Access(string key)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.PrivateKey == key || r.PublicKey == key);
            if (reservation == null)
            {
                return RedirectToAction("InvalidKey");
            }

            if (reservation.PrivateKey == key)
            {
                return RedirectToAction("Edit", new { id = reservation.Id });
            }

            if (reservation.PublicKey == key)
            {
                return RedirectToAction("View", new { id = reservation.Id });
            }

            return RedirectToAction("InvalidKey");
        }

        // Ansicht nur für Public Key
        public IActionResult View(Guid id)
        {
            // Reservation anhand der ID aus der Datenbank suchen
            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound("Reservation nicht gefunden.");
            }

            // HTML-Datei laden
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Reservation", "View.html");
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Die Datei View.html wurde nicht gefunden.");
            }

            // Platzhalter in der HTML-Datei durch tatsächliche Werte ersetzen
            var htmlContent = System.IO.File.ReadAllText(filePath);
            htmlContent = htmlContent.Replace("{{Id}}", reservation.Id.ToString());
            htmlContent = htmlContent.Replace("{{Participants}}", reservation.Participants);
            htmlContent = htmlContent.Replace("{{RoomNumber}}", reservation.RoomNumber.ToString());
            htmlContent = htmlContent.Replace("{{Date}}", reservation.Date.ToString("yyyy-MM-dd"));
            htmlContent = htmlContent.Replace("{{StartTime}}", reservation.StartTime.ToString(@"hh\:mm"));
            htmlContent = htmlContent.Replace("{{EndTime}}", reservation.EndTime.ToString(@"hh\:mm"));
            htmlContent = htmlContent.Replace("{{Remarks}}", reservation.Remarks.ToString());
            htmlContent = htmlContent.Replace("{{PrivateKey}}", reservation.PrivateKey.ToString());
            htmlContent = htmlContent.Replace("{{PublicKey}}", reservation.PublicKey.ToString());

            // HTML-Seite zurückgeben
            return Content(htmlContent, "text/html");
        }
    }
}
