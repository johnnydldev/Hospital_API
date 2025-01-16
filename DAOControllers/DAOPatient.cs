using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAOControllers.ManagerControllers;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Numerics;


namespace DAOControllers
{
    public class DAOPatient : IGenericRepository<Patient>
    {
        private readonly string _connection = string.Empty;

        public DAOPatient(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("hospitalConnection").ToString();
        }//End contsructor
        
        public async Task<int> Create(Patient patient)
        {
            int generatedPatient = 0;

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Create_Patient", objConnection);
                    cmd.Parameters.AddWithValue("namePatient", patient.namePatient);
                    cmd.Parameters.AddWithValue("middleName", patient.middleName);
                    cmd.Parameters.AddWithValue("lastName", patient.lastName);
                    cmd.Parameters.AddWithValue("birthDate", patient.birthDate);
                    cmd.Parameters.AddWithValue("telephone", patient.telephone);
                    cmd.Parameters.AddWithValue("idAddress", patient.address.idAddress);
                    cmd.Parameters.Add("response", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    await objConnection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    generatedPatient = Convert.ToInt32(cmd.Parameters["response"].Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                generatedPatient = 0;
            }

            Console.WriteLine(generatedPatient);

            return generatedPatient;
        }//End create patient

        public async Task<bool> Delete(int idPatient)
        {
            bool response = false;
            string message = "Paciente eliminado con exito.";

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Delete_Patient", objConnection);
                    cmd.Parameters.AddWithValue("idPatient", idPatient);
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
        }//End delete patient

        public async Task<bool> Edit(Patient patient)
        {
            bool response = false;

            string message = "Paciente actualizado con exito.";

            try
            {

                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Update_Patient", objConnection);
                    cmd.Parameters.AddWithValue("idPatient", patient.idPatient);
                    cmd.Parameters.AddWithValue("namePatient", patient.namePatient);
                    cmd.Parameters.AddWithValue("middleName", patient.middleName);
                    cmd.Parameters.AddWithValue("lastName", patient.lastName);
                    cmd.Parameters.AddWithValue("birthDate", patient.birthDate);
                    cmd.Parameters.AddWithValue("telephone", patient.telephone);
                    cmd.Parameters.AddWithValue("idAddress", patient.address.idAddress);
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
        }//End edit patient

        public async Task<List<Patient>> GetAll()
        {
            List<Patient> allPatients = new List<Patient>();

            try
            {
                SqlDataReader reader;

                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_List_Patients", objConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    await objConnection.OpenAsync();
                    using (reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            allPatients.Add(new Patient
                            {
                                idPatient = Convert.ToInt32(reader["idPatient"].ToString()),
                                namePatient = reader["namePatient"].ToString(),
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
                allPatients = new List<Patient>();
            }

            return allPatients;
        }//End get all patients

        public Task<List<Patient>> GetAllMatchedBy(int idPatient)
        {
            throw new NotImplementedException();
        }//End get all patients matches by

        public Task<List<Patient>> GetAllMatches(int idPatient, string name)
        {
            throw new NotImplementedException();
        }//End get all patients matches

        public Task<List<Patient>> GetAllMatchesWith(string name)
        {
            throw new NotImplementedException();
        }//End get all patients matches with

        public async Task<Patient> GetById(int idPatient)
        {
            Patient patient = new Patient();
            string message = "Paciente encontrado.";

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlDataReader reader;
                    SqlCommand cmd;

                    cmd = new SqlCommand("SP_Search_Patient_By_Id", objConnection);
                    cmd.Parameters.AddWithValue("idPatient", idPatient);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    await objConnection.OpenAsync();

                    using (reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            patient = new Patient
                            {
                                idPatient = Convert.ToInt32(reader["idPatient"].ToString()),
                                namePatient = reader["namePatient"].ToString(),
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
                patient = new Patient(); ;
            }

            Console.WriteLine(message);

            return patient;
        }//End get patient by id

        public Task<int> GetMaxId()
        {
            throw new NotImplementedException();
        }//End get max patient id


    }//End Patient class
}//End namespace
