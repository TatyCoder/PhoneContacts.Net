using System;

namespace PhoneContacts.Net.Models;

    public class Contact
    {
        public int contactId { get; set; }
        public String name { get; set; }

        public Contact(int contactId, String name)
        {
            this.contactId = contactId;
            this.name = name;
        }

    }