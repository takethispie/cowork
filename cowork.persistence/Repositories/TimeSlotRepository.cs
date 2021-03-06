using System.Collections.Generic;
using System.Data.Common;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.persistence.Datamappers;
using cowork.persistence.Handlers;
using cowork.persistence.ModelBuilders;
using Npgsql;

namespace cowork.persistence.Repositories {

    public class TimeSlotRepository : ITimeSlotRepository {

        private const string InnerJoin = " INNER JOIN \"Place\" P on \"TimeSlot\".\"PlaceId\" = P.\"Id\" ";

        private readonly SqlDataMapper<TimeSlot> datamapper;


        public TimeSlotRepository(string connection) {
            datamapper = new SqlDataMapper<TimeSlot>(SqlDbType.Postgresql, connection, new TimeSlotBuilder());
        }


        public List<TimeSlot> GetAll() {
            const string sql = "SELECT * FROM public.\"TimeSlot\"" + InnerJoin + ";";
            return datamapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public TimeSlot GetById(long id) {
            const string sql = "SELECT * FROM public.\"TimeSlot\"" + InnerJoin + "WHERE \"TimeSlot\".\"Id\"=@id;";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return datamapper.OneItemCommand(sql, parameters);
        }


        public List<TimeSlot> GetAllOfPlace(long placeId) {
            const string sql = "SELECT * FROM \"TimeSlot\"" + InnerJoin + "WHERE \"TimeSlot\".\"PlaceId\"= @id";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", placeId)
            };
            return datamapper.MultiItemCommand(sql, par);
        }


        public List<TimeSlot> GetAllByPaging(int page, int amount) {
            const string sql = "SELECT * FROM \"TimeSlot\"" + InnerJoin +
                               " ORDER BY \"TimeSlot\".\"PlaceId\" ASC LIMIT @amount OFFSET @skip;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("amount", amount),
                new NpgsqlParameter("skip", page * amount)
            };
            return datamapper.MultiItemCommand(sql, par);
        }


        public bool Delete(long id) {
            const string sql = "DELETE FROM public.\"TimeSlot\" where \"Id\"=@id RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return datamapper.NoQueryCommand(sql, parameters) > -1;
        }


        public long Create(TimeSlot timeSlot) {
            const string sql =
                "INSERT INTO public.\"TimeSlot\" (\"Id\", \"Day\", \"StartHour\", \"StartMinutes\", \"EndHour\", \"EndMinutes\", \"PlaceId\") VALUES (DEFAULT, @day, @startHour, @startMinutes, @endHour, @endMinutes, @placeId) RETURNING  \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("startHour", timeSlot.StartHour),
                new NpgsqlParameter("startMinutes", timeSlot.StartMinutes),
                new NpgsqlParameter("endHour", timeSlot.EndHour),
                new NpgsqlParameter("endMinutes", timeSlot.EndMinutes),
                new NpgsqlParameter("placeId", timeSlot.PlaceId),
                new NpgsqlParameter("day", (long) timeSlot.Day)
            };
            return datamapper.NoQueryCommand(sql, parameters);
        }


        public long Update(TimeSlot timeSlot) {
            const string sql =
                "UPDATE public.\"TimeSlot\" SET \"Day\"= @day, \"StartHour\"= @startHour, \"StartMinutes\"= @startMinutes, \"EndHour\"= @endHour, \"EndMinutes\"= @endMinutes, \"PlaceId\"= @placeId WHERE \"Id\" = @id RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", timeSlot.Id),
                new NpgsqlParameter("startHour", timeSlot.StartHour),
                new NpgsqlParameter("startMinutes", timeSlot.StartMinutes),
                new NpgsqlParameter("endHour", timeSlot.EndHour),
                new NpgsqlParameter("endMinutes", timeSlot.EndMinutes),
                new NpgsqlParameter("placeId", timeSlot.PlaceId),
                new NpgsqlParameter("day", (long) timeSlot.Day)
            };
            return datamapper.NoQueryCommand(sql, parameters);
        }

    }

}