using Microsoft.AspNetCore.Mvc;
using PhoneContacts.Net.Models;

using PhoneContacts.Net.Services;

namespace PhoneContacts.Net.Controllers;

[ApiController]
[Route ("/api/contacts")]
public class ContactsController : ControllerBase
{

    [HttpGet]
    public List<Contact> getAllContacts ()
    {

        //Without the Service:
        //List<Contact> contacts = new List<Contact> ();
        //Contact contact = new Contact (1,"name1");
        //contacts.Add (contact);
        //return contacts;

        //With the Service:
        ContactsService cs = new ContactsService ();
        return cs.getAllContacts ();

    }
}