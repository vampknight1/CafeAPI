using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using CafeAPI.Models;

namespace CafeAPI.Repo
{
    public class MenusRepo : IRepo<Menus>
    {
        private string _connStr;
        public MenusRepo(IConfiguration configuration)
        {
            _connStr = configuration.GetValue<string>("localPGsql:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(_connStr);
            }

        }

        public void Add(Menus itemObj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"INSERT INTO mst_menu (
                                                --id, 
                                                menu_name, 
                                                category,
                                                menu_price,
                                                menu_stock,
                                                menu_desc,
                                                menu_img,
                                                menu_type,
                                                created_at, 
                                                updated_at,
                                                is_deleted,
                                                updated_by
                                        )
                                            VALUES (
                                                --@ID, 
                                                @MenuName, 
                                                @Category,
                                                @MenuPrice,
                                                @MenuStock,
                                                @MenuDesc,
                                                @MenuImg, 
                                                @MenuType, 
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
                catch(NpgsqlException ex)
                {
                    throw ex;
                }
            }
        }


        public List<Menus> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id, 
                                                    menu_name AS MenuName, 
                                                    category AS Category,
                                                    menu_price AS MenuPrice,
                                                    menu_stock AS MenuStock,
                                                    menu_desc AS MenuDesc,
                                                    menu_img AS MenuImg,
                                                    menu_type AS MenuType,
                                                    created_at AS CreatedAt, 
                                                    updated_at AS UpdatedAt
                                         FROM mst_menu
                                         WHERE is_deleted <> '1'
                                         ORDER BY category, menu_name ASC";

                dbConnection.Open();
                var result = dbConnection.Query<Menus>(sQuery).ToList();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }


        public Menus FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id, 
                                                    menu_name AS MenuName, 
                                                    category AS Category,
                                                    menu_price AS MenuPrice,
                                                    menu_stock AS MenuStock,
                                                    menu_desc AS MenuDesc,
                                                    menu_img AS MenuImg,
                                                    menu_type AS MenuType,
                                                    created_at AS CreatedAt, 
                                                    updated_at AS UpdatedAt
                                         FROM mst_menu
                                         WHERE id = @ID AND is_deleted <> '1'";

                dbConnection.Open();
                var result = dbConnection.Query<Menus>(sQuery, new { @ID = id }).FirstOrDefault();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE mst_menu SET 
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
                catch (Exception ex) {
                    throw ex;
                }
            }
        }

        public void Update(Menus itemObj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE mst_menu SET 
                                                    menu_name = @MenuName, 
                                                    category = @Category,
                                                    menu_price = @MenuPrice,
                                                    menu_stock = @MenuStock,
                                                    menu_desc = @MenuDesc,
                                                    menu_img = @MenuImg,
                                                    menu_type = @MenuType,
                                                    updated_at = CURRENT_TIMESTAMP,
                                                    updated_by = 212
                                               WHERE id = @ID";
                try {
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, itemObj);
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                catch (NpgsqlException ex) {
                    throw ex;
                }
            }
        }

        public void ApiUpdate(Menus itemObj, int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE mst_menu SET 
                                                    menu_name = @menu_name, 
                                                    category = @category,
                                                    menu_price = @menu_price,
                                                    menu_stock = @menu_stock,
                                                    menu_desc = @menu_desc,
                                                    menu_img = @menu_img,
                                                    menu_type = @menu_type,
                                                    updated_at = CURRENT_TIMESTAMP,
                                                    updated_by = 212
                                               WHERE id = @ID";

                try {
                    string strDtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var dtNow = strDtNow;

                    dbConnection.Open();
                    var result = dbConnection.Execute(sQuery, new {
                        itemObj.MenuName,
                        itemObj.MenuType,
                        //itemObj.Created_At,
                        dtNow,
                        id
                    });
                }
                catch (NpgsqlException ex)
                {
                    throw ex;
                }
            }
        }

        public List<Menus> GetJoinWith()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT m.id, 
				                                    m.menu_name AS MenuName,
				                                    c.category_name AS CategoryName,
				                                    m.menu_price AS MenuPrice,
				                                    m.menu_stock AS MenuStock,
                                                    m.menu_desc AS MenuDesc,
                                                    m.menu_img AS MenuImg,
                                                    m.menu_type AS MenuType,
				                                    m.created_at AS CreatedAt, 
				                                    m.updated_at AS UpdatedAt
                                        FROM mst_menu m
                                        LEFT JOIN mst_category c ON m.category  = c.id
                                        WHERE m.is_deleted <> '1'";

                dbConnection.Open();
                //var result = dbConnection.Query<Customers, Area, Customers>(sQuery, (c, a) => { c.City = a.ID; return c; }, splitOn: "city").ToList();
                var result = dbConnection.Query<Menus>(sQuery).ToList();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

    }
}