using cowork.domain.Interfaces;

namespace cowork.usecases.User {

    public class DeleteUser : IUseCase<bool> {

        private readonly IUserRepository userRepository;
        public readonly long Id;

        public DeleteUser(IUserRepository userRepository, long id) {
            this.userRepository = userRepository;
            Id = id;
        }


        public bool Execute() {
            return userRepository.DeleteById(Id);
        }

    }

}