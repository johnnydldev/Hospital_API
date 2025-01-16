using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DAOControllers.ManagerControllers;
using Microsoft.Extensions.Configuration;
using Models;


namespace DAOControllers
{
    public class DAODoctor : IGenericRepository<Doctor>
    {

        private readonly string _connection = string.Empty;

        public DAODoctor(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("hospitalConnection").ToString();
        }//End contsructor

        public async Task<int> Create(Doctor doctor)
        {
            int generatedDoctor = 0;

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Create_Doctor", objConnection);
                    cmd.Parameters.AddWithValue("nameDoctor", doctor.nameDoctor);
                    cmd.Parameters.AddWithValue("middleName", doctor.middleName);
                    cmd.Parameters.AddWithValue("lastName", doctor.lastName);
                    cmd.Parameters.AddWithValue("birthDate", doctor.birthDate);
                    cmd.Parameters.AddWithValue("telephone", doctor.telephone);
                    cmd.Parameters.AddWithValue("idAddress", doctor.address.idAddress);
                    cmd.Parameters.Add("response", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    await objConnection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    generatedDoctor = Convert.ToInt32(cmd.Parameters["response"].Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                generatedDoctor = 0;
            }

            Console.WriteLine(generatedDoctor);

            return generatedDoctor;
        }//End create doctor

        public async Task<bool> Delete(int idDoctor)
        {
            bool response = false;
            string message = "Doctor eliminado con exito.";

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Delete_Doctor", objConnection);
                    cmd.Parameters.AddWithValue("idDoctor", idDoctor);
                    cmd.Parameters.Add("response", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    await objConnection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    response = Convert.ToBoolean(cmd.Parameters["response"].Value);

                }

            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                response = false;
            }

            Console.WriteLine(message);

            return response;
        }//End delete doctor

        public  async Task<bool> Edit(Doctor doctor)
        {
            bool response = false;

            string message = "Doctor actualizado con exito.";

            try
            {

                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Update_Doctor", objConnection);
                    cmd.Parameters.AddWithValue("idDoctor", doctor.idDoctor);
                    cmd.Parameters.AddWithValue("nameDoctor", doctor.nameDoctor);
                    cmd.Parameters.AddWithValue("middleName", doctor.middleName);
                    cmd.Parameters.AddWithValue("lastName", doctor.lastName);
                    cmd.Parameters.AddWithValue("birthDate", doctor.birthDate);
                    cmd.Parameters.AddWithValue("telephone", doctor.telephone);
                    cmd.Parameters.AddWithValue("idAddress", doctor.address.idAddress);
                    cmd.Parameters.Add("response", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    await objConnection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    response = Convert.ToBoolean(cmd.Parameters["response"].Value);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                response = false;
            }

            Console.WriteLine(message);

            return response;
        }//End edit doctor

        public async Task<List<Doctor>> GetAll()
        {
            List<Doctor> allDoctors = new List<Doctor>();

            try
            {
                SqlDataReader reader;

                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_List_Doctors", objConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    await objConnection.OpenAsync();
                    using (reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            allDoctors.Add(new Doctor
                            {
                               idDoctor = Convert.ToInt32(reader["idDoctor"].ToString()),
                               nameDoctor = reader["nameDoctor"].ToString(),
                               middleName = reader["middleName"].ToString(),
                               lastName = reader["lastName"].ToString(),
                               birthDate = reader["birthDate"].ToString(),
                               telephone = reader["telephone"].ToString(),
                               address = new Address
                               {
                                   idAddress = Convert.ToInt32(reader["idAddress"].ToString()),
                                   street = reader["street"].ToString(),
                                   suburb = reader["suburb"].ToString(),
                                   city = reader["city"].ToString(),
                                   state = reader["state"].ToString()
                               }

                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                allDoctors = new List<Doctor>();
            }

            return allDoctors;
        }//End list all doctors

        public Task<List<Doctor>> GetAllMatchedBy(int idDoctor)
        {
            throw new NotImplementedException();
        }//End get all doctors that matches by

        public Task<List<Doctor>> GetAllMatches(int idDoctor, string name)
        {
            throw new NotImplementedException();
        }//End get all doctors that matches

        public Task<List<Doctor>> GetAllMatchesWith(string name)
        {
            throw new NotImplementedException();
        }//End get all doctors that matches with

        public async Task<Doctor> GetById(int idDoctor)
        {
            Doctor doctor = new Doctor();
            string message = "Doctor encontrado.";

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlDataReader reader;
                    SqlCommand cmd;

                    cmd = new SqlCommand("SP_Search_Doctor_By_Id", objConnection);
                    cmd.Parameters.AddWithValue("idDoctor", idDoctor);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    await objConnection.OpenAsync();

                    using (reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            doctor = new Doctor
                            {
                                idDoctor = Convert.ToInt32(reader["idDoctor"].ToString()),
                                nameDoctor = reader["nameDoctor"].ToString(),
                                middleName = reader["middleName"].ToString(),
                                lastName = reader["lastName"].ToString(),
                                birthDate = reader["birthDate"].ToString(),
                                telephone = reader["telephone"].ToString(),
                                address = new Address
                                {
                                    idAddress = Convert.ToInt32(reader["idAddress"].ToString()),
                                    street = reader["street"].ToString()
                                }
                            };

                        }

                    }
                }


            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                doctor = new Doctor(); ;
            }

            Console.WriteLine(message);

            return doctor;
        }//End get doctor by id

        public Task<int> GetMaxId()
        {
            throw new NotImplementedException();
        }//End get max id doctor

    }//End Doctor class
}//End namespace
