using cowork.domain.Interfaces;

namespace cowork.usecases.User {

    public class UpdateUser : IUseCase<long> {

        private readonly IUserRepository repository;
        public readonly domain.User User;

        public UpdateUser(IUserRepository repository, domain.User user) {
            this.repository = repository;
            User = user;
        }


        public long Execute() {
            return repository.Update(User);
        }

    }

}