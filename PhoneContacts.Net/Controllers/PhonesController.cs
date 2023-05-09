using System;
using Microsoft.AspNetCore.Mvc;
using PhoneContacts.Net.Models;
using PhoneContacts.Net.Services;

namespace PhoneContacts.Net.Controllers;


[ApiController]
[Route ("/api/contacts/phones")]
public class PhonesController : ControllerBase
{
    public PhonesController()
    {
    }

    [HttpGet ("{phoneId}")]
    public Phone getPhone (int phoneId)
    {
        PhonesService ps = new PhonesService ();
        return ps.getPhone (phoneId);
    }

    [HttpPost]
    public Phone createPhone (Phone phone)
    {
        PhonesService ps = new PhonesService ();
        return ps.createPhone (phone);
    }

    [HttpPut]
    public Phone updatePhone (Phone phone)
    {
        PhonesService ps = new PhonesService ();
        return ps.updatePhone (phone);
    }

    [HttpDelete ("{phoneId}")]
    public Phone deletePhone (int phoneId)
    {
        PhonesService ps = new PhonesService ();
        return ps.deletePhone (phoneId);
    }
}