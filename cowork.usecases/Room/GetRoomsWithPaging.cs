using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Room {

    public class GetRoomsWithPaging : IUseCase<IEnumerable<domain.Room>> {

        private readonly IRoomRepository roomRepository;
        public readonly int Page;
        public readonly int Amount;

        public GetRoomsWithPaging(IRoomRepository roomRepository, int page, int amount) {
            this.roomRepository = roomRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.Room> Execute() {
            return roomRepository.GetAllWithPaging(Page, Amount);
        }

    }

}