using System;

using Npgsql;
using PhoneContacts.Net.Models;
using PhoneContacts.Net.Controllers;

namespace PhoneContacts.Net.Services;

public class ContactsService
{
    string connectionString = "Host=localhost;Port=5432;Database=phone_contacts_net;Username=postgres;Password=admintc";

    public ContactsService ()
    {
    }

    public List<Contact> getAllContacts ()
    {
        List<Contact> contacts = new List<Contact> ();

        using (var conn = new NpgsqlConnection (connectionString)) {
            conn.Open ();
            // Retrieve data from the "contacts" table
            using (var cmd = new NpgsqlCommand ("SELECT contact_id, name FROM contacts", conn)) {
                var reader = cmd.ExecuteReader ();
                {
                    while (reader.Read ()) {
                        // Retrieve data from the current row
                        int contactId = reader.GetInt16 (0);
                        string name = reader.GetString (1);

                        // Create a Contact object and display the data
                        Contact contact = new Contact (contactId, name);
                        contacts.Add (contact);
                        Console.WriteLine ("ContactId: {contact.contactId}, Name: {contact.name}");
                    }
                }
            }
        }
        return contacts;
    }

    public Contact getContact(int contactId)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand("SELECT contact_id, name FROM contacts WHERE contact_id = @contact_id", conn))
            {
                cmd.Parameters.AddWithValue("@contact_id", contactId);
                var reader = cmd.ExecuteReader();
                {
                    reader.Read();
                    Contact contact = new Contact(reader.GetInt16(0), reader.GetString(1));
                    return contact;
                }
            }
        }
    }

    public Contact createContact (Contact contact)
    {
        using (var conn = new NpgsqlConnection (connectionString)) {
            conn.Open ();
            using (var cmd = new NpgsqlCommand ("INSERT INTO contacts (contact_id, name) VALUES (@contact_id, @name)", conn)) {
                cmd.Parameters.AddWithValue ("@contact_id", contact.contactId);
                cmd.Parameters.AddWithValue ("@name", contact.name);
                var reader = cmd.ExecuteNonQuery ();
                return getContact (contact.contactId);
            }
        }
    }

    public Contact updateContact (Contact contact)
    {
        using (var conn = new NpgsqlConnection (connectionString)) {
            conn.Open ();
            using (var cmd = new NpgsqlCommand ("UPDATE contacts SET name = @name WHERE contact_id = @contact_id", conn)) {
                cmd.Parameters.AddWithValue ("@contact_id", contact.contactId);
                cmd.Parameters.AddWithValue ("@name", contact.name);
                var reader = cmd.ExecuteNonQuery ();
                return getContact (contact.contactId);
            }
        }
    }
}

