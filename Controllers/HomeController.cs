using Microsoft.AspNetCore.Mvc;
using RaumReservierungsSystem.Data;
using System.IO;
using System.Text;

namespace RaumReservierungsSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Hole alle Reservationen aus der Datenbank
            var reservations = _context.Reservations.ToList();

            // Baue die Tabelle dynamisch als HTML
            var tableRows = new StringBuilder();
            foreach (var reservation in reservations)
            {
                tableRows.AppendLine($@"
                    <tr>
                        <td>{reservation.Participants}</td>
                        <td>{reservation.RoomNumber}</td>
                        <td>{reservation.Date:dd.MM.yyyy}</td>
                        <td>{reservation.StartTime}</td>
                        <td>{reservation.EndTime}</td>
                        <td>{reservation.Remarks}</td>
                        <td>{reservation.PrivateKey}</td>
                        <td>{reservation.PublicKey}</td>
                        <td>
                            <a href='/Reservation/Edit/{reservation.Id}' class='btn btn-warning btn-sm'>Bearbeiten</a>
                        </td>
                    </tr>");
            }

            // Lade die HTML-Datei und füge die Tabelle ein
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Home", "Index.html");
            var htmlContent = System.IO.File.ReadAllText(filePath);
            htmlContent = htmlContent.Replace("<!--DYNAMISCHE_TABELLE-->", tableRows.ToString());

            return Content(htmlContent, "text/html");
        }
    }
}
