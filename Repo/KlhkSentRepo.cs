using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using CafeAPI.Models;

namespace CafeAPI.Repo
{
    public class KlhkSentRepo : IRepo<KlhkSents>
    {
        private string _connStr;
        public KlhkSentRepo(IConfiguration configuration)
        {
            //_connStr = configuration.GetValue<string>("localPGsql:ConnectionString");
            _connStr = configuration.GetValue<string>("dbPG:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get {
                return new NpgsqlConnection(_connStr);
            }
        }

        public List<KlhkSents> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id, 
                                                    uid AS TableCode, 
                                                    datetime AS DateTime,
                                                    ph AS pH, 
                                                    cod AS COD,
                                                    tss AS TSS,
                                                    nh3 AS NH3,
                                                    debit AS Debit
                                         FROM klhksents";

                dbConnection.Open();
                var result = dbConnection.Query<KlhkSents>(sQuery).ToList();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

        public KlhkSents FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id, 
                                                    uid AS TableCode, 
                                                    datetime AS DateTime,
                                                    ph AS pH, 
                                                    cod AS COD,
                                                    tss AS TSS,
                                                    nh3 AS NH3,
                                                    debit AS Debit
                                         FROM klhksents
                                        WHERE id = @ID";

                dbConnection.Open();
                var result = dbConnection.Query<KlhkSents>(sQuery, new { @ID = id }).FirstOrDefault();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"DELETE FROM klhksents WHERE id = @ID";

                dbConnection.Open();
                dbConnection.Execute(sQuery, new { ID = id });
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }

        public void Add(KlhkSents itemObj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"INSERT INTO klhksents (
                                            uid,
                                            datetime,
                                            ph,
                                            cod,
                                            tss,
                                            nh3,
                                            debit
                                        ) 
                                        VALUES (
                                            @TableCode,
                                            @DateTime,
                                            @pH,
                                            @COD,
                                            @TSS,
                                            @NH3,
                                            @Debit
                                        )";

                dbConnection.Open();
                dbConnection.Execute(sQuery, itemObj);
                dbConnection.Close();
                dbConnection.Dispose();
            }

        }        

        public void Update(KlhkSents itemObj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE klhksents SET 
                                                uid = @TableCode,
                                                datetime = @DateTime,
                                                ph = @pH, 
                                                cod = @COD, 
                                                tss = @TSS,
                                                nh3 = @NH3,
                                                debit = @Debit
                                        WHERE id = @ID";
                try {
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new
                    {
                        itemObj.TableCode,
                        itemObj.DateTime,
                        itemObj.pH,
                        itemObj.COD,
                        itemObj.TSS,
                        itemObj.NH3,
                        itemObj.Debit,                        
                        itemObj.ID
                    });
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        ////  for Function ApiUpdate must have at least 2 Parameter
        ////  itemObj   -->  Class 
        ////  id            -->  from Routing Parameter
        public void ApiUpdate(KlhkSents itemObj, int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE klhksents SET 
                                                uid = @TableCode,
                                                datetime = @DateTime,
                                                ph = @pH, 
                                                cod = @COD, 
                                                tss = @TSS,
                                                nh3 = @NH3,
                                                debit = @Debit
                                        WHERE id = @ID";
                try {
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new
                    {
                        itemObj.TableCode,
                        itemObj.DateTime,
                        itemObj.pH,
                        itemObj.COD,
                        itemObj.TSS,
                        itemObj.NH3,
                        itemObj.Debit,
                        id
                    });
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<KlhkSents> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}