using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Room {

    public class GetRoomsFromPlace : IUseCase<IEnumerable<domain.Room>> {

        private readonly IRoomRepository roomRepository;
        public readonly long PlaceId;

        public GetRoomsFromPlace(IRoomRepository roomRepository, long placeId) {
            this.roomRepository = roomRepository;
            PlaceId = placeId;
        }


        public IEnumerable<domain.Room> Execute() {
            return roomRepository.GetAllFromPlace(PlaceId);
        }

    }

}