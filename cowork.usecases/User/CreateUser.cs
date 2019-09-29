using cowork.domain.Interfaces;

namespace cowork.usecases.User {

    public class CreateUser : IUseCase<long> {

        private readonly IUserRepository userRepository;
        public readonly domain.User User;

        public CreateUser(IUserRepository userRepository, domain.User user) {
            this.userRepository = userRepository;
            User = user;
        }


        public long Execute() {
            return userRepository.Create(User);
        }

    }

}