using DAOControllers.ManagerControllers;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOControllers
{
    public class DAOLaboratoryResult : IGenericRepository<LaboratoryResult>
    {
        private readonly string _connection = string.Empty;

        public DAOLaboratoryResult(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("hospitalConnection").ToString();
        }//End contsructor

        public async Task<int> Create(LaboratoryResult labResult)
        {
            int generatedLabResult = 0;

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Create_Lab_Result", objConnection);
                    cmd.Parameters.AddWithValue("test", labResult.test);
                    cmd.Parameters.AddWithValue("resultValue", labResult.resultValue);
                    cmd.Parameters.AddWithValue("dateDone", DateTime.Now);
                    cmd.Parameters.Add("response", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    await objConnection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    generatedLabResult = Convert.ToInt32(cmd.Parameters["response"].Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                generatedLabResult = 0;
            }

            Console.WriteLine(generatedLabResult);

            return generatedLabResult;
        }//End create lab result

        public async Task<bool> Delete(int idLabResult)
        {
            bool response = false;
            string message = "Resultado de laboratorio eliminado con exito.";

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Delete_Lab_Result", objConnection);
                    cmd.Parameters.AddWithValue("idLabResult", idLabResult);
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
        }//End delete lab result

        public async Task<bool> Edit(LaboratoryResult labResult)
        {
            bool response = false;

            string message = "Resultado de laboratorio actualizado con exito.";

            try
            {

                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Update_Lab_Result", objConnection);
                    cmd.Parameters.AddWithValue("idLaboratoryResult", labResult.idLaboratoryResult);
                    cmd.Parameters.AddWithValue("test", labResult.test);
                    cmd.Parameters.AddWithValue("resultValue", labResult.resultValue);
                    cmd.Parameters.AddWithValue("dateDone", DateTime.Now); 
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
        }//End edit lab result

        public async Task<List<LaboratoryResult>> GetAll()
        {
            List<LaboratoryResult> allLabResults = new List<LaboratoryResult>();

            try
            {
                SqlDataReader reader;

                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_List_Lab_Results", objConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    await objConnection.OpenAsync();
                    using (reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            allLabResults.Add(new LaboratoryResult
                            {
                                idLaboratoryResult = Convert.ToInt32(reader["idLaboratoryResult"].ToString()),
                                test = reader["test"].ToString(),
                                resultValue = reader["resultValue"].ToString(),
                                dateDone = reader["dateDone"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                allLabResults = new List<LaboratoryResult>();
            }

            return allLabResults;
        }//End get all lab results

        public Task<List<LaboratoryResult>> GetAllMatchedBy(int idLabResult)
        {
            throw new NotImplementedException();
        }//End get all matched by

        public Task<List<LaboratoryResult>> GetAllMatches(int idLabResult, string name)
        {
            throw new NotImplementedException();
        }//End all matches

        public Task<List<LaboratoryResult>> GetAllMatchesWith(string name)
        {
            throw new NotImplementedException();
        }//End get all matchess with

        public async Task<LaboratoryResult> GetById(int idLabResult)
        {
            LaboratoryResult labResult = new LaboratoryResult();
            string message = "Resultado de laboratorio encontrado.";

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlDataReader reader;
                    SqlCommand cmd;

                    cmd = new SqlCommand("SP_Get_LabResult_By_Id", objConnection);
                    cmd.Parameters.AddWithValue("idLabResult", idLabResult);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    await objConnection.OpenAsync();

                    using (reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            labResult = new LaboratoryResult
                            {
                                idLaboratoryResult = Convert.ToInt32(reader["idLaboratoryResult"].ToString()),
                                test = reader["test"].ToString(),
                                resultValue = reader["resultValue"].ToString(),
                                dateDone = reader["dateDone"].ToString()
                            };

                        }

                    }
                }


            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                labResult = new LaboratoryResult(); ;
            }

            Console.WriteLine(message);

            return labResult;
        }//End get by id

        public Task<int> GetMaxId()
        {
            throw new NotImplementedException();
        }//End get max id

    }//End laboratory class
}//End namespace
