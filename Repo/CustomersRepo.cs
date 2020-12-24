using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Npgsql;
using CafeAPI.Models;
using CafeAPI.Repo;

namespace CafeAPI.Repo
{
    public class CustomersRepo : IRepo<Customers>
    {
        private string _strConn;
        public CustomersRepo(IConfiguration configuration)
        {
            _strConn = configuration.GetValue<string>("localPGsql:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(_strConn);
            }
        }


        public void Add(Customers itemObj)
        {
            using (IDbConnection dbConnection = Connection)     // Connection = dapConn
            {
                string sQuery = @"INSERT INTO customer (
                                            --id,
                                            customer_name,
                                            table_code,
                                            customer_address,
                                            city,
                                            customer_phone,
                                            created_at,
                                            updated_at,
                                            is_deleted,
                                            updated_by
                                        )
                                        VALUES(
                                            --@ID,
                                            @CustomerName,
                                            @TableCode,
                                            @CustomerAddress,
                                            @City,
                                            @CustomerPhone,
                                            CURRENT_TIMESTAMP,
                                            CURRENT_TIMESTAMP,
                                            '0',
                                            212
                                        )";

                dbConnection.Open();
                dbConnection.Execute(sQuery, itemObj);
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }

        public void ApiUpdate(Customers itemObj, int id)
        {
            throw new NotImplementedException();
        }

        public List<Customers> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id, 
                                                    table_code AS TableCode, 
                                                    customer_name AS CustomerName,
                                                    customer_address AS CustomerAddress,
                                                    customer_phone AS CustomerPhone,
                                                    city AS City,
                                                    created_at AS CreatedAt, 
                                                    updated_at AS UpdatedAt
                                         FROM customer
                                         WHERE is_deleted <> '1'";

                dbConnection.Open();
                var result = dbConnection.Query<Customers>(sQuery).ToList();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

        public Customers FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id, 
                                                    table_code AS TableCode, 
                                                    customer_name AS CustomerName,
                                                    customer_address AS CustomerAddress,
                                                    customer_phone AS CustomerPhone,
                                                    city AS City,
                                                    created_at AS CreatedAt, 
                                                    updated_at AS UpdatedAt
                                         FROM customer
                                         WHERE id = @ID AND is_deleted <> '1'";

                dbConnection.Open();
                var result = dbConnection.Query<Customers>(sQuery, new { @ID = id }).FirstOrDefault();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE customer SET 
                                                    is_deleted = '1',
                                                    updated_at = CURRENT_TIMESTAMP,
                                                    updated_by = 212
                                         WHERE id = @ID";
                try {
                    dbConnection.Open();
                    //dbConnection.Execute(@"DELETE FROM mst_menu WHERE id=@ID", new { Id = id });
                    dbConnection.Execute(sQuery, new { ID = id });
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Update(Customers itemObj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE customer SET 
                                                    customer_name = @CustomerName,
                                                    table_code = @TableCode,                                                    
                                                    customer_phone = @CustomerPhone, 
                                                    customer_address = @CustomerAddress,
                                                    city = @City,
                                                    updated_at = CURRENT_TIMESTAMP,
                                                    updated_by = 212
                                         WHERE id = @ID";
                try {
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, itemObj);
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
        }

        public List<Customers> GetJoinWith()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT c.id, 
				                                    c.table_code AS TableCode, 
				                                    c.customer_name AS CustomerName,
				                                    c.customer_address AS CustomerAddress,
				                                    c.customer_phone AS CustomerPhone,
				                                    a.name AS CityName,
				                                    c.created_at AS CreatedAt, 
				                                    c.updated_at AS UpdatedAt
                                        FROM customer c
                                        LEFT JOIN area AS a ON a.id  = c.city
                                        WHERE c.is_deleted <> '1'";

                dbConnection.Open();
                //var result = dbConnection.Query<Customers, Area, Customers>(query, (c, a) => { c.City = a.ID; return c; }, splitOn: "city").ToList();
                var result = dbConnection.Query<Customers>(sQuery).ToList();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

    }
}