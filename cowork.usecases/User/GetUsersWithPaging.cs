using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.User {

    public class GetUsersWithPaging : IUseCase<IEnumerable<domain.User>> {

        private readonly IUserRepository userRepository;
        public readonly int Page;
        public readonly int Amount;

        public GetUsersWithPaging(IUserRepository userRepository, int page, int amount) {
            this.userRepository = userRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.User> Execute() {
            return userRepository.GetAllWithPaging(Page, Amount);
        }

    }

}