using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using CafeAPI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CafeAPI.Repo
{
    public class AreaRepo : IRepo<Area>
    {
        private string _strConn;
        public AreaRepo(IConfiguration configuration)
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

        public void Add(Area itemObj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"INSERT INTO area (id, code, name, created_at, updated_at, is_deleted, updated_by)
                                                        VALUES(@ID, @Code, @Name, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, '0', '212')";

                dbConnection.Open();
                dbConnection.Execute(sQuery, itemObj);
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }

        public List<Area> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id, code, name, created_at AS CreatedAt, updated_at AS UpdatedAt 
                                         FROM area 
                                         WHERE is_deleted <> '1'
                                         ORDER BY code ASC";

                dbConnection.Open();
                var result = dbConnection.Query<Area>(sQuery).ToList();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

        public Area FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id, code, name, created_at AS CreatedAt, updated_at AS UpdatedAt 
                                          FROM area 
                                          WHERE id = @ID AND is_deleted <> '1'";

                dbConnection.Open();
                var result = dbConnection.Query<Area>(sQuery, new { @ID = id }).FirstOrDefault();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE area SET 
                                                    is_deleted = '1',
                                                    updated_at = CURRENT_TIMESTAMP,
                                                    updated_by = 212
                                          WHERE id = @ID";
                try
                {
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

        public void Update(Area itemObj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE area SET 
                                                    code = @Code, 
                                                    name = @Name,
                                                    updated_at = CURRENT_TIMESTAMP,
                                                    updated_by = 212
                                         WHERE id = @ID";
                try {
                    //string strDtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //var dtNow = NpgsqlTypes.NpgsqlDbType.Timestamp;
                    //var dtNow = DateTime.Parse(strDtNow);
                    //var dtNow = @"CURRENT_TIMESTAMP";

                    dbConnection.Open();
                    var result = dbConnection.Execute(sQuery, new
                    {
                        itemObj.Code,
                        itemObj.Name,
                        //itemObj.CreatedAt,
                        //dtNow,
                        itemObj.ID
                    });
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                catch (NpgsqlException ex) {
                    throw ex;
                }
            }
        }

        public void ApiUpdate(Area itemObj, int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE area SET
                                                    code = @Code, 
                                                    name = @Name,
                                                    updated_at = CURRENT_TIMESTAMP,
                                                    updated_by = 212
                                         WHERE id = @ID";
                try
                {
                    dbConnection.Open();
                    var result = dbConnection.Execute(sQuery, new
                    {
                        itemObj.Code,
                        itemObj.Name,
                        //itemObj.Created_At,
                        //dtNow,
                        id
                    });
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                catch (NpgsqlException ex)
                {
                    throw ex;
                }
            }
        }

    }
}