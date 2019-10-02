using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Room {

    public class GetAllRooms : IUseCase<IEnumerable<domain.Room>> {

        private readonly IRoomRepository roomRepository;

        public GetAllRooms(IRoomRepository roomRepository) {
            this.roomRepository = roomRepository;
        }


        public IEnumerable<domain.Room> Execute() {
            return roomRepository.GetAll();
        }

    }

}