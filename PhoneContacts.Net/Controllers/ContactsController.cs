using Microsoft.AspNetCore.Mvc;
using PhoneContacts.Net.Models;

namespace PhoneContacts.Net.Controllers;

[ApiController]
[Route ("/api/contacts")]
public class ContactsController : ControllerBase
{

    [HttpGet]
    public List<Contact> getAllContacts ()
    {
        List<Contact> contacts = new List<Contact> ();
        Contact contact = new Contact (1,"name1");
        contacts.Add (contact);
        return contacts;
    }
}