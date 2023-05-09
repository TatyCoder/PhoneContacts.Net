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

    [HttpPost]
    public Phone createPhone (Phone phone)
    {
        PhonesService ps = new PhonesService ();
        return ps.createPhone (phone);
    }
}