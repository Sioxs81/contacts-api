using ContactsApi.Data;
using ContactsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ContactsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Contact>> GetAll()
    {
        return Ok(_db.Contacts.ToList());
    }
}
