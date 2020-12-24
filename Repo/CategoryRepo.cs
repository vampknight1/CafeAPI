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
    public class CategoryRepo : IRepo<Category>
    {
        private string _strConn;
        public CategoryRepo(IConfiguration configuration)
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


        public void Add(Category itemObj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"INSERT INTO mst_category (
                                                                        --id,
                                                                        category_name,
                                                                        category_desc,
                                                                        created_at,
                                                                        updated_at,
                                                                        is_deleted,
                                                                        updated_by
                                                                    )
                                                                    VALUES (
                                                                        --@ID,
                                                                        @CategoryName,
                                                                        @CategoryDesc,
                                                                        CURRENT_TIMESTAMP,
                                                                        CURRENT_TIMESTAMP,
                                                                        '0',
                                                                        212
                                                                    )";
                try {
                    dbConnection.Open();
                    var result = dbConnection.Execute(sQuery, itemObj);
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                catch (NpgsqlException ex)
                {
                    throw ex;
                }
            }

        }

        public void ApiUpdate(Category itemObj, int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id, 
                                                    category_name AS CategoryName, 
                                                    category_desc AS CategoryDesc,
                                                    created_at AS CreatedAt, 
                                                    updated_at AS UpdatedAt
                                         FROM mst_category
                                        WHERE is_deleted <> '1'";

                dbConnection.Open();
                var result = dbConnection.Query<Category>(sQuery).ToList();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

        public Category FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id, 
                                                    category_name AS CategoryName, 
                                                    category_desc AS CategoryDesc,     
                                                    created_at AS CreatedAt, 
                                                    updated_at AS UpdatedAt
                                         FROM mst_category
                                        WHERE id = @ID AND is_deleted <> '1'";

                dbConnection.Open();
                var result = dbConnection.Query<Category>(sQuery, new { @ID = id }).FirstOrDefault();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE mst_category SET 
                                                    is_deleted = '1',
                                                    updated_at = CURRENT_TIMESTAMP
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

        public void Update(Category itemObj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string strUpdate = @"UPDATE mst_category SET 
                                                    category_name = @CategoryName,
                                                    category_desc = @CategoryDesc,                                                    
                                                    updated_at = CURRENT_TIMESTAMP,
                                                    updated_by = 212
                                            WHERE id = @ID";
                try {
                    dbConnection.Open();
                    dbConnection.Execute(strUpdate, itemObj);
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
        }

        public List<Category> GetJoinWith()
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
                                        FROM mst_category c
                                        LEFT JOIN area AS a ON a.id  = c.city
                                        WHERE c.is_deleted <> '1'";

                dbConnection.Open();
                //var result = dbConnection.Query<Customers, Area, Customers>(sQuery, (c, a) => { c.City = a.ID; return c; }, splitOn: "city").ToList();
                var result = dbConnection.Query<Category>(sQuery).ToList();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

    }
}