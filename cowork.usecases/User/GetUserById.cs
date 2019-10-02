using cowork.domain.Interfaces;

namespace cowork.usecases.User {

    public class GetUserById : IUseCase<domain.User> {

        private readonly IUserRepository userRepository;
        public readonly long Id;

        public GetUserById(IUserRepository userRepository, long id) {
            this.userRepository = userRepository;
            Id = id;
        }


        public domain.User Execute() {
            return userRepository.GetById(Id);
        }

    }

}