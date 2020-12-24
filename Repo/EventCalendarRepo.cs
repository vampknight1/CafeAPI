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
    public class EventCalendarRepo : IRepo<EventCalendar>
    {
        private readonly string _strConn;
        public EventCalendarRepo(IConfiguration configuration)
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


        public void Add(EventCalendar itemObj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"INSERT INTO event_calendar (
                                                --id,
                                                subject,
                                                description,
                                                start_event,
                                                end_event,
                                                theme_color,
                                                is_fullday,
                                                created_at,
                                                updated_at,
                                                is_deleted,
                                                updated_by
                                            )
                                            VALUES (
                                                --@ID,
                                                @Subject,
                                                @Description,
                                                @StartEvent,
                                                @EndEvent,
                                                @ThemeColor,
                                                @IsFullday,
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

        public void ApiUpdate(EventCalendar itemObj, int id)
        {
            throw new NotImplementedException();
        }

        public List<EventCalendar> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id,
                                                    subject AS Subject ,
                                                    description AS Description,
                                                    start_event AS StartEvent,
                                                    end_event AS EndEvent,
                                                    theme_color AS ThemeColor,
                                                    is_fullday AS IsFullday,
                                                    created_at AS CreatedAt,
                                                    updated_at AS UpdatedAt
                                         FROM event_calendar
                                         WHERE is_deleted <> '1'
                                         ORDER BY start_event ASC";

                dbConnection.Open();
                var result = dbConnection.Query<EventCalendar>(sQuery).ToList();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

        public EventCalendar FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT id,
                                                    subject Subject,
                                                    description Description,
                                                    start_event StartEvent,
                                                    end_event EndEvent,
                                                    theme_color ThemeColor,
                                                    is_fullday IsFullday,
                                                    created_at CreatedAt,
                                                    updated_at UpdatedAt
                                           FROM event_calendar
                                           WHERE id = @ID AND is_deleted <> '1'";

                dbConnection.Open();
                var result = dbConnection.Query<EventCalendar>(sQuery, new { @ID = id }).FirstOrDefault();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE event_calendar SET 
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

        public void Update(EventCalendar itemObj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string strUpdate = @"UPDATE event_calendar SET 
                                                    subject = @Subject ,
                                                    description = @Description,
                                                    start_event = @StartEvent,
                                                    end_event = @EndEvent,
                                                    theme_color = @ThemeColor,
                                                    is_fullday = @IsFullday,
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

        public List<EventCalendar> GetJoinWith()
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
                                        FROM event_calendar c
                                        LEFT JOIN area AS a ON a.id  = c.city
                                        WHERE c.is_deleted <> '1'";

                dbConnection.Open();
                var result = dbConnection.Query<EventCalendar>(sQuery).ToList();
                dbConnection.Close();
                dbConnection.Dispose();

                return result;
            }
        }
    }
}