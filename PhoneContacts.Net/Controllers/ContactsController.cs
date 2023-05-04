using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        ContactsService cs = new ContactsService ();
        return cs.getAllContacts ();
    }

    [HttpGet("{contactId}")]
    public Contact getContact (int contactId)
    {
        ContactsService cs = new ContactsService();
        Contact contact = cs.getContact (contactId);
        return contact;

    }

    [HttpPost]
    public Contact createContact (Contact contact)
    {
        ContactsService cs = new ContactsService ();
        return cs.createContact (contact);
    }

    [HttpPut]
    public Contact updateContact (Contact contact)
    {
        ContactsService cs = new ContactsService ();
        return cs.updateContact (contact);
    }

    [HttpDelete ("{contactId}")]
    public Contact deleteContact (int contactId)
    {
        ContactsService cs = new ContactsService ();
        return cs.deleteContact (contactId);
    }
}