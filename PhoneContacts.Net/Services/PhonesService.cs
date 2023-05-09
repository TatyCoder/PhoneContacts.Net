using System;
using Npgsql;
using PhoneContacts.Net.Models;
using PhoneContacts.Net.Controllers;
using System.Numerics;
using System.Xml.Linq;

namespace PhoneContacts.Net.Services {
    public class PhonesService {
        string connectionString = "Host=localhost;Port=5432;Database=phone_contacts_net;Username=postgres;Password=admintc";

        public PhonesService ()
        {
        }

        public Phone getPhone (int phoneId)
        {
            using (var conn = new NpgsqlConnection (connectionString)) {
                conn.Open ();
                using (var cmd = new NpgsqlCommand ("SELECT phone_id, phone, contact_id FROM phones WHERE phone_id = @phone_id", conn)) {
                    cmd.Parameters.AddWithValue ("@phone_id", phoneId);
                    var reader = cmd.ExecuteReader ();
                    {
                        reader.Read ();
                        int phone_id = reader.GetInt16 (0);
                        string phone = reader.GetString (1);
                        int contact_id = reader.GetInt16 (2);

                        Phone phones = new Phone (phone_id, phone, contact_id);
                        return phones;
                    }

                }
            }
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

        public Phone deletePhone (int phoneId)
        {
            using (var conn = new NpgsqlConnection (connectionString)) {
                conn.Open ();
                using (var cmd = new NpgsqlCommand ("DELETE FROM phones WHERE phone_id = @phone_id", conn)) {
                    cmd.Parameters.AddWithValue ("@phone_id", phoneId);
                    Phone deletedPhone = this.getPhone (phoneId);
                    var reader = cmd.ExecuteNonQuery ();
                    return deletedPhone;
                }
            }
        }
    }
}