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
    public class TablesRepo : IRepo<Tables>
    {
        private readonly string _strConn;
        public TablesRepo(IConfiguration configuration)
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

        public void Add(Tables itemObj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"INSERT INTO mst_table (
                                                --id,
                                                table_no,
                                                table_code,
                                                note,
                                                created_at,
                                                updated_at,
                                                is_deleted,
                                                updated_by
                                        )
                                         VALUES(
                                                --@ID,
                                                @TableNo,
                                                @TableCode,
                                                @Note,
                                                CURRENT_TIMESTAMP,
                                                CURRENT_TIMESTAMP,
                                                '0',
                                                212
                                        )";
                try
                {
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

        public void ApiUpdate(Tables itemObj, int id)
        {
            throw new NotImplementedException();
        }

        public List<Tables> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id, 
                                                    table_no AS TableNo, 
                                                    table_code AS TableCode,
                                                    note AS Note,
                                                    created_at AS CreatedAt, 
                                                    updated_at AS UpdatedAt
                                         FROM mst_table
                                         WHERE is_deleted <> '1'";

                dbConnection.Open();
                var result = dbConnection.Query<Tables>(sQuery).ToList();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

        public Tables FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id, 
                                                    table_no AS TableNo, 
                                                    table_code AS TableCode,
                                                    note AS Note,
                                                    created_at AS CreatedAt, 
                                                    updated_at AS UpdatedAt
                                         FROM mst_table
                                         WHERE id = @ID AND is_deleted <> '1'";

                dbConnection.Open();
                var result = dbConnection.Query<Tables>(sQuery, new { @ID = id }).FirstOrDefault();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE mst_table SET 
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

        public void Update(Tables itemObj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE mst_table SET 
                                                    table_code = @TableCode,
                                                    table_no = @TableNo,
                                                    note = @Note,
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

        public List<Tables> GetJoinWith()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT t.id, 
				                                    t.table_no AS TableNo, 
                                                    t.table_code AS TableCode,
                                                    t.note AS Note,
				                                    a.name AS CityName,
				                                    t.created_at AS CreatedAt, 
				                                    t.updated_at AS UpdatedAt
                                        FROM mst_table t
                                        LEFT JOIN area AS a ON a.id  = c.city
                                        WHERE c.is_deleted <> '1'";

                dbConnection.Open();
                //var result = dbConnection.Query<Customers, Area, Customers>(sQuery, (c, a) => { c.City = a.ID; return c; }, splitOn: "city").ToList();
                var result = dbConnection.Query<Tables>(sQuery).ToList();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

    }
}