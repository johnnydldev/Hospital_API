using DAOControllers.ManagerControllers;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DAOControllers
{
    public class DAOAddress : IGenericRepository<Address>
    {

        private readonly string _connection = string.Empty;

        public DAOAddress(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("hospitalConnection").ToString();
        }

        public async Task<int> Create(Address ObjAddress)
        {
            int generatedAddress = 0;

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Create_Address", objConnection);
                    cmd.Parameters.AddWithValue("street", ObjAddress.street);
                    cmd.Parameters.AddWithValue("suburb", ObjAddress.suburb);
                    cmd.Parameters.AddWithValue("city", ObjAddress.city);
                    cmd.Parameters.AddWithValue("state", ObjAddress.state);
                    cmd.Parameters.Add("response", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    await objConnection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    generatedAddress = Convert.ToInt32(cmd.Parameters["response"].Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                generatedAddress = 0;
            }

            Console.WriteLine(generatedAddress);

            return generatedAddress;
        }//End create address

        public async Task<bool> Delete(int idAddress)
        {
            bool response = false;
            string message = "Dirección eliminada con exito.";

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Delete_Address", objConnection);
                    cmd.Parameters.AddWithValue("idAddress", idAddress);
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
        }//End delete address 

        public async Task<bool> Edit(Address objAddress)
        {
            bool response = false;

            string message = "Dirección actualizada con exito.";

            try
            {

                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_Update_Address", objConnection);
                    cmd.Parameters.AddWithValue("idAddress", objAddress.idAddress);
                    cmd.Parameters.AddWithValue("street", objAddress.street);
                    cmd.Parameters.AddWithValue("suburb", objAddress.suburb);
                    cmd.Parameters.AddWithValue("city", objAddress.city);
                    cmd.Parameters.AddWithValue("state", objAddress.state);
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
        }

        public async Task<List<Address>> GetAll()
        {
            List<Address> allAddresses = new List<Address>();

            try
            {
                SqlDataReader reader;

                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_List_Address", objConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    await objConnection.OpenAsync();
                    using (reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            allAddresses.Add(new Address
                            {
                                idAddress = Convert.ToInt32(reader["idAddress"].ToString()),
                                street = reader["street"].ToString(),
                                suburb = reader["suburb"].ToString(),
                                city = reader["city"].ToString(),
                                state = reader["state"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                allAddresses = new List<Address>();
            }

            return allAddresses;
        }//End list address

        public Task<List<Address>> GetAllMatchedBy(int idModel)
        {
            throw new NotImplementedException();
        }//End get all matches by

        public Task<List<Address>> GetAllMatches(int idModel, string name)
        {
            throw new NotImplementedException();
        }//End get all matches

        public Task<List<Address>> GetAllMatchesWith(string name)
        {
            throw new NotImplementedException();
        }//End get all matches with

        public async Task<Address> GetById(int idAddress)
        {
            Address address = new Address();
            string message = "Dirección encontrada.";

            try
            {
                using (var objConnection = new SqlConnection(_connection))
                {
                    SqlDataReader reader;
                    SqlCommand cmd;

                    cmd = new SqlCommand("SP_Get_Address_By_Id", objConnection);
                    cmd.Parameters.AddWithValue("idAddress", idAddress);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    await objConnection.OpenAsync();

                    using (reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            address = new Address
                            {
                                idAddress = Convert.ToInt32(reader["idAddress"].ToString()),
                                street = reader["street"].ToString(),
                                suburb = reader["suburb"].ToString(),
                                city = reader["city"].ToString(),
                                state = reader["state"].ToString()
                            };

                        }

                    }
                }


            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                address = new Address(); ;
            }

            Console.WriteLine(message);

            return address;
        }//End get address by id

        public Task<int> GetMaxId()
        {
            throw new NotImplementedException();
        }//End get max id address

    }//End Address class

}//End namespace
