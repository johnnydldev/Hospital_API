using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DAOControllers.ManagerControllers;
using Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DAOControllers
{
    public class DAOMedicament : IGenericRepository<Medicament>
    {
        private readonly string _connection = string.Empty;

        public DAOMedicament(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("hospitalConnection").ToString();
        }//End contsructor

        public async Task<int> Create(Medicament medicament)
        {
            int generatedMedicament = 0;

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Create_Medicament", objConnection);
                    cmd.Parameters.AddWithValue("nameMedicament", medicament.nameMedicament);
                    cmd.Parameters.AddWithValue("dose", medicament.dose);
                    cmd.Parameters.AddWithValue("useInstruction", medicament.useInstruction);
                    cmd.Parameters.Add("response", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    await objConnection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    generatedMedicament = Convert.ToInt32(cmd.Parameters["response"].Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                generatedMedicament = 0;
            }

            Console.WriteLine(generatedMedicament);

            return generatedMedicament;
        }//End create

        public async Task<bool> Delete(int idMedicament)
        {
            bool response = false;
            string message = "Medicamento eliminado con exito.";

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Delete_Medicament", objConnection);
                    cmd.Parameters.AddWithValue("idMedicament", idMedicament);
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
        }//End delete

        public async Task<bool> Edit(Medicament medicament)
        {
            bool response = false;

            string message = "Medicamento actualizado con exito.";

            try
            {

                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Update_Medicament", objConnection);
                    cmd.Parameters.AddWithValue("idMedicament", medicament.idMedicament); 
                    cmd.Parameters.AddWithValue("nameMedicament", medicament.nameMedicament);
                    cmd.Parameters.AddWithValue("dose", medicament.dose);
                    cmd.Parameters.AddWithValue("useInstruction", medicament.useInstruction);
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
        }//End get edit

        public async Task<List<Medicament>> GetAll()
        {
            List<Medicament> allMedicaments = new List<Medicament>();

            try
            {
                SqlDataReader reader;

                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_List_medicaments", objConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    await objConnection.OpenAsync();
                    using (reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            allMedicaments.Add(new Medicament
                            {
                                idMedicament = Convert.ToInt32(reader["idMedicament"].ToString()),
                                nameMedicament = reader["nameMedicament"].ToString(),
                                dose = reader["dose"].ToString(),
                                useInstruction = reader["useInstruction"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                allMedicaments = new List<Medicament>();
            }

            return allMedicaments;
        }//End get all

        public Task<List<Medicament>> GetAllMatchedBy(int idMedicament)
        {
            throw new NotImplementedException();
        }//End Get all matched by

        public Task<List<Medicament>> GetAllMatches(int idMedicament, string name)
        {
            throw new NotImplementedException();
        }//End get all matches

        public Task<List<Medicament>> GetAllMatchesWith(string name)
        {
            throw new NotImplementedException();
        }//End get all matches with

        public async Task<Medicament> GetById(int idMedicament)
        {
            Medicament medicament = new Medicament();
            string message = "Medicamento encontrada.";

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlDataReader reader;
                    SqlCommand cmd;

                    cmd = new SqlCommand("SP_Get_Medicament_By_Id", objConnection);
                    cmd.Parameters.AddWithValue("idMedicament", idMedicament);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    await objConnection.OpenAsync();

                    using (reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            medicament = new Medicament
                            {
                                idMedicament = Convert.ToInt32(reader["idMedicament"].ToString()),
                                nameMedicament = reader["nameMedicament"].ToString(),
                                dose = reader["dose"].ToString(),
                                useInstruction = reader["useInstruction"].ToString()
                            };

                        }

                    }
                }


            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                medicament = new Medicament(); ;
            }

            Console.WriteLine(message);

            return medicament;
        }//End get by id

        public Task<int> GetMaxId()
        {
            throw new NotImplementedException();
        }//End get max id

    }//End Medicament class
}//End namespace
