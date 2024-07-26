
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using stajproje.Model;
using stajproje.Model.DTO;
using stajproje.Model.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace stajproje.Data
{
    public class ConnectionClass
    {
        private readonly string _connectionString;
        public Response response = new Response();
        List<NYO_SosyalYardimModel> lstNyo = new List<NYO_SosyalYardimModel>();
        SqlConnection con = new SqlConnection("Server=localhost;Database=Odemeler;User Id=SA;Password=reallyStrongPwd123;Encrypt=True;TrustServerCertificate=True;");



        public ConnectionClass(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<NYO_SosyalYardimModel>> CallStoredProcedureAsync(string tckn)//çalışıyor
        {
            var results = new List<NYO_SosyalYardimModel>();


            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SorgulaSosyalOdeme", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tckn", tckn);

                        await conn.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var model = new NYO_SosyalYardimModel
                                {
                                    ODEMENO = reader.GetInt32(reader.GetOrdinal("ODEMENO")),
                                    TCKIMLIKNO = reader.GetString(reader.GetOrdinal("TCKIMLIKNO")),
                                    MUSTERIAD = reader.GetString(reader.GetOrdinal("MUSTERIAD")),
                                    MUSTERISOYAD = reader.GetString(reader.GetOrdinal("MUSTERISOYAD")),
                                    ODEME_KD = reader.GetInt32(reader.GetOrdinal("ODEME_KD")),
                                    ODEME_TTR = reader.GetInt32(reader.GetOrdinal("ODEME_TTR")),
                                    ODEME_TR = reader.GetDateTime(reader.GetOrdinal("ODEME_TR")),
                                    ODEME_ACK = reader.GetString(reader.GetOrdinal("ODEME_ACK"))
                                };
                                results.Add(model);


                            }
                        }
                    }


                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Stored procedure çağrılırken bir hata oluştu: {ex.Message}");
                throw;
            }

            return results;
        }

        public async Task<List<NYO_SosyalYardimModel>> GetByOdemeno(int odemeno)//çalışıyor
        {
            var results = new List<NYO_SosyalYardimModel>();


            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_getbyodemeno", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@odemeno", odemeno);

                        await conn.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var model = new NYO_SosyalYardimModel
                                {
                                    ODEMENO = reader.GetInt32(reader.GetOrdinal("ODEMENO")),
                                    TCKIMLIKNO = reader.GetString(reader.GetOrdinal("TCKIMLIKNO")),
                                    MUSTERIAD = reader.GetString(reader.GetOrdinal("MUSTERIAD")),
                                    MUSTERISOYAD = reader.GetString(reader.GetOrdinal("MUSTERISOYAD")),
                                    ODEME_KD = reader.GetInt32(reader.GetOrdinal("ODEME_KD")),
                                    ODEME_TTR = reader.GetInt32(reader.GetOrdinal("ODEME_TTR")),
                                    ODEME_TR = reader.GetDateTime(reader.GetOrdinal("ODEME_TR")),
                                    ODEME_ACK = reader.GetString(reader.GetOrdinal("ODEME_ACK"))
                                };
                                results.Add(model);


                            }
                        }
                    }


                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Stored procedure çağrılırken bir hata oluştu: {ex.Message}");
                throw;
            }

            return results;
        }




        public bool OdemeSPAsync(string tckn, int odemeno)//çalışıyor
        {
            bool success = true;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdatePaymentStatus", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tckn", tckn);
                        cmd.Parameters.AddWithValue("@ODEMENO", odemeno);

                       
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            success = true;
                        }
                        catch (SqlException ex)
                        {
                            success = false;
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                success = false;
                Console.WriteLine(ex.Message);
            }
            return success;
        }





        public async Task<List<NYO_SosyalYardimModel>> GetAllStoredProcedureAsync()//ÇALIŞTI
        {
            var results = new List<NYO_SosyalYardimModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_getir", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        await conn.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var model = new NYO_SosyalYardimModel
                                {
                                    ODEMENO = reader.GetInt32(reader.GetOrdinal("ODEMENO")),
                                    TCKIMLIKNO = reader.GetString(reader.GetOrdinal("TCKIMLIKNO")),
                                    MUSTERIAD = reader.GetString(reader.GetOrdinal("MUSTERIAD")),
                                    MUSTERISOYAD = reader.GetString(reader.GetOrdinal("MUSTERISOYAD")),
                                    ODEME_KD = reader.GetInt32(reader.GetOrdinal("ODEME_KD")),
                                    ODEME_TTR = reader.GetInt32(reader.GetOrdinal("ODEME_TTR")),
                                    ODEME_TR = reader.GetDateTime(reader.GetOrdinal("ODEME_TR")),
                                    ODEME_ACK = reader.GetString(reader.GetOrdinal("ODEME_ACK"))
                                };
                                results.Add(model);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Stored procedure çağrılırken bir hata oluştu: {ex.Message}");
                throw;
            }

            return results;

        }




        public string InsertOdeme(NYOSYCreateDTO nyososyalyardim)//SUCCESS DÖNDÜ
        {
            string msg = string.Empty;
            try
            {
                SqlCommand com = new SqlCommand("sp_create2", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                com.Parameters.AddWithValue("@TCKIMLIKNO", nyososyalyardim.TCKIMLIKNO);
                com.Parameters.AddWithValue("@MUSTERIAD", nyososyalyardim.MUSTERIAD);
                com.Parameters.AddWithValue("@MUSTERISOYAD", nyososyalyardim.MUSTERISOYAD);
                com.Parameters.AddWithValue("@ODEME_KD", nyososyalyardim.ODEME_KD);
                com.Parameters.AddWithValue("@ODEME_TTR", nyososyalyardim.ODEME_TTR);
                com.Parameters.AddWithValue("@ODEME_TR", nyososyalyardim.ODEME_TR);
                com.Parameters.AddWithValue("@ODEME_ACK", nyososyalyardim.ODEME_ACK);

                con.Open();
                com.ExecuteNonQuery();
                msg = "SUCCESS";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open && true)
                {
                    con.Close();
                }
            }
            return msg;
        }

        public string Update(NYO_SosyalYardimModel nyososyalyardim)//SUCCESS DÖNDÜ
        {
            string msg = string.Empty;
            try

            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_UpdateNYOSosyalYardim", con)
                {
                    CommandType = CommandType.StoredProcedure


                };
                com.Parameters.AddWithValue("@ODEMENO", nyososyalyardim.ODEMENO);
                com.Parameters.AddWithValue("@TCKIMLIKNO", nyososyalyardim.TCKIMLIKNO);
                com.Parameters.AddWithValue("@MUSTERIAD", nyososyalyardim.MUSTERIAD);
                com.Parameters.AddWithValue("@MUSTERISOYAD", nyososyalyardim.MUSTERISOYAD);
                com.Parameters.AddWithValue("@ODEME_KD", nyososyalyardim.ODEME_KD);
                com.Parameters.AddWithValue("@ODEME_TTR", nyososyalyardim.ODEME_TTR);
                com.Parameters.AddWithValue("@ODEME_TR", nyososyalyardim.ODEME_TR);
                com.Parameters.AddWithValue("@ODEME_ACK", nyososyalyardim.ODEME_ACK);


                com.ExecuteNonQuery();
                msg = "SUCCESS";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return msg;
        }




        public async Task<List<NYO_SosyalYardimModel>> Delete(int odemeno)
        {
            var results = new List<NYO_SosyalYardimModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteNYOSosyalYardim", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ODEMENO", odemeno);


                        await conn.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var model = new NYO_SosyalYardimModel
                                {
                                    ODEMENO = reader.GetInt32(reader.GetOrdinal("ODEMENO")),
                                    TCKIMLIKNO = reader.GetString(reader.GetOrdinal("TCKIMLIKNO")),
                                    MUSTERIAD = reader.GetString(reader.GetOrdinal("MUSTERIAD")),
                                    MUSTERISOYAD = reader.GetString(reader.GetOrdinal("MUSTERISOYAD")),
                                    ODEME_KD = reader.GetInt32(reader.GetOrdinal("ODEME_KD")),
                                    ODEME_TTR = reader.GetInt32(reader.GetOrdinal("ODEME_TTR")),
                                    ODEME_TR = reader.GetDateTime(reader.GetOrdinal("ODEME_TR")),
                                    ODEME_ACK = reader.GetString(reader.GetOrdinal("ODEME_ACK"))
                                };
                                results.Add(model);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Stored procedure çağrılırken bir hata oluştu: {ex.Message}");
                throw;
            }

            return results;

        }
        public List<NYO_SosyalYardimModel> GetAllUsers()
        {
            var odemeler = new List<NYO_SosyalYardimModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_getir", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    odemeler.Add(new NYO_SosyalYardimModel
                    {

                        ODEMENO = (int)reader["ODEMENO"],
                        TCKIMLIKNO = reader["TCKIMLIKNO"].ToString(),
                        MUSTERIAD = reader["MUSTERIAD"].ToString(),
                        MUSTERISOYAD = reader["MUSTERISOYAD"].ToString(),
                        ODEME_KD = (int)reader["ODEME_KD"],
                        ODEME_TTR = (int)reader["ODEME_TTR"],
                        ODEME_TR = (DateTime)reader["ODEME_TR"],
                        ODEME_ACK = reader["ODEME_ACK"].ToString()
                    });
                }

                return odemeler;
            }
        }
    }
}

           

                           
                          


