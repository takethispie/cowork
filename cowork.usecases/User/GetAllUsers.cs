using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.User {

    public class GetAllUsers : IUseCase<IEnumerable<domain.User>> {

        private readonly IUserRepository userRepository;

        public GetAllUsers(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }


        public IEnumerable<domain.User> Execute() {
            return userRepository.GetAll();
        }

    }

}