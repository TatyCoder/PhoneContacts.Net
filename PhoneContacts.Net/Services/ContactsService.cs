using System;
using Npgsql;
using PhoneContacts.Net.Models;
using PhoneContacts.Net.Controllers;

namespace PhoneContacts.Net.Services;

public class ContactsService {
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

    public Contact getContact (int contactId)
    {
        using (var conn = new NpgsqlConnection (connectionString)) {
            conn.Open ();

            // Write a query to retrieve data from the table for a given ID
            using (var cmd = new NpgsqlCommand ("SELECT contacts.contact_id, name, phone, phone_id FROM contacts, phones " +
                "WHERE contacts.contact_id = phones.contact_id AND contacts.contact_id = @contact_id", conn)) {
                // populate the contact_id from the query with an actual ID passed to us (contactId parameter)
                cmd.Parameters.AddWithValue ("@contact_id", contactId);
                // execute the query with the parameter
                var reader = cmd.ExecuteReader ();

                // create a list to store the phone objects
                List<Phone> phones = new List<Phone> ();

                string name = "";

                // iterate through all the records returned by the query and create a new Phone object for each
                while (reader.Read ()) {
                    int contact_id = reader.GetInt16 (0);
                    name = reader.GetString (1);
                    string phoneNumber = reader.GetString (2);
                    int phone_id = reader.GetInt16 (3);

                    Phone phone = new Phone (phone_id, phoneNumber, contact_id);
                    phones.Add (phone);
                }

                // create a new Contact object and set the list the Phones as well as the name and contactId
                Contact contact = new Contact (contactId, name, phones);
                return contact;

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

    public Contact deleteContact (int contactId)
    {
        using (var conn = new NpgsqlConnection (connectionString)) {
            conn.Open ();

            using (var cmd = new NpgsqlCommand ("DELETE FROM contacts WHERE contact_id = @contact_id", conn)) {
                cmd.Parameters.AddWithValue ("@contact_id", contactId);
                Contact deletedContact = this.getContact (contactId);
                var reader = cmd.ExecuteNonQuery ();
                return deletedContact;
            }
        }
    }
}

