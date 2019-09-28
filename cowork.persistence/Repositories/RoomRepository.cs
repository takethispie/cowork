using System.Collections.Generic;
using System.Data.Common;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.persistence.Datamappers;
using cowork.persistence.Handlers;
using cowork.persistence.ModelBuilders;
using Npgsql;

namespace cowork.persistence.Repositories {

    public class RoomRepository : IRoomRepository {

        private const string InnerJoin = " INNER JOIN \"Place\" P on \"Room\".\"PlaceId\" = P.\"Id\" ";

        private readonly SqlDataMapper<Room> datamapper;


        public RoomRepository(string connection) {
            datamapper = new SqlDataMapper<Room>(SqlDbType.Postgresql, connection, new RoomBuilder());
        }


        public List<Room> GetAll() {
            const string sql = "SELECT * FROM public.\"Room\"" + InnerJoin + ";";
            return datamapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public Room GetById(long id) {
            const string sql = "SELECT * FROM public.\"Room\"" + InnerJoin + "WHERE \"Room\".\"Id\"=@id";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return datamapper.OneItemCommand(sql, parameters);
        }


        public Room GetByName(string name) {
            const string sql = "SELECT * FROM public.\"Room\"" + InnerJoin + "WHERE \"Room\".\"Name\"=@p";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("p", name)
            };
            return datamapper.OneItemCommand(sql, parameters);
        }


        public List<Room> GetAllFromPlace(long placeId) {
            const string sql = "SELECT * FROM public.\"Room\"" + InnerJoin + "WHERE \"PlaceId\"=@p";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("p", placeId)
            };
            return datamapper.MultiItemCommand(sql, parameters);
        }


        public List<Room> GetAllWithPaging(int page, int amount) {
            const string sql = "SELECT * FROM \"Room\"" + InnerJoin +
                               " ORDER BY \"Room\".\"Id\" ASC LIMIT @amount OFFSET @skip;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("amount", amount),
                new NpgsqlParameter("skip", page * amount)
            };
            return datamapper.MultiItemCommand(sql, par);
        }


        public long Create(Room room) {
            const string sql =
                "INSERT INTO public.\"Room\" (\"Id\", \"Name\", \"PlaceId\", \"RoomType\") VALUES (DEFAULT, @name, @placeId, @roomType) RETURNING \"Room\".\"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("name", room.Name),
                new NpgsqlParameter("placeId", room.PlaceId),
                new NpgsqlParameter("roomType", (long) room.Type)
            };
            return datamapper.NoQueryCommand(sql, parameters);
        }


        public bool Delete(long id) {
            const string sql = "DELETE FROM public.\"Room\" WHERE \"Id\"=@p RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("p", id)
            };
            return datamapper.NoQueryCommand(sql, parameters) > -1;
        }


        public long Update(Room room) {
            const string sql =
                "UPDATE public.\"Room\" SET \"Name\" = @name, \"PlaceId\" = @placeId, \"RoomType\" = @roomType WHERE \"Id\"=@id RETURNING  \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", room.Id),
                new NpgsqlParameter("name", room.Name),
                new NpgsqlParameter("placeId", room.PlaceId),
                new NpgsqlParameter("roomType", (long) room.Type)
            };
            return datamapper.NoQueryCommand(sql, parameters);
        }

    }

}