using System;
using System.Xml.Linq;

namespace PhoneContacts.Net.Models;

public class Phone
{
    public int phoneId { get; set; }
    public String phone { get; set; }
    public int contactId { get; set; }

    public Phone(int phoneId, String phone, int contactId)
    {
        this.phoneId = phoneId;
        this.phone = phone;
        this.contactId = contactId;
    }
}

