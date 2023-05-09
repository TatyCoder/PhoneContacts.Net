using System;
using Npgsql;
using PhoneContacts.Net.Models;
using PhoneContacts.Net.Controllers;

namespace PhoneContacts.Net.Services
{
    public class PhonesService
    {
        string connectionString = "Host=localhost;Port=5432;Database=phone_contacts_net;Username=postgres;Password=admintc";

        public PhonesService ()
        {
        }

        public Phone createPhone (Phone phone)
        {
            using (var conn = new NpgsqlConnection (connectionString)) {
                conn.Open ();
                using (var cmd = new NpgsqlCommand ("INSERT INTO phones (phone_id, phone, contact_id) VALUES (@phone_id, @phone, @contact_id)", conn)) {
                    cmd.Parameters.AddWithValue ("@phone_id", phone.phoneId);
                    cmd.Parameters.AddWithValue ("@phone", phone.phone);
                    cmd.Parameters.AddWithValue ("@contact_id", phone.contactId);
                    var reader = cmd.ExecuteNonQuery ();
                    return phone;
                }
            }
        }

        public Phone updatePhone (Phone phone)
        {
            using (var conn = new NpgsqlConnection (connectionString)) {
                conn.Open ();
                using (var cmd = new NpgsqlCommand ("UPDATE phones SET phone = @phone WHERE phone_id = @phone_id", conn)) {
                    cmd.Parameters.AddWithValue ("@phone_id", phone.phoneId);
                    cmd.Parameters.AddWithValue ("@phone", phone.phone);
                    cmd.Parameters.AddWithValue ("@contact_id", phone.contactId);
                    var reader = cmd.ExecuteNonQuery ();
                    return phone;
                }
            }
        }
    }
}