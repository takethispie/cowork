using cowork.domain.Interfaces;

namespace cowork.usecases.User {

    public class DeleteUser : IUseCase<bool> {

        private readonly IUserRepository userRepository;
        private readonly ISubscriptionRepository subscriptionRepository;
        public readonly long Id;

        public DeleteUser(IUserRepository userRepository, ISubscriptionRepository subscriptionRepository, long id) {
            this.userRepository = userRepository;
            Id = id;
            this.subscriptionRepository = subscriptionRepository;
        }


        public bool Execute() {
            var userSub = subscriptionRepository.GetOfUser(Id);
            if (userSub != null) subscriptionRepository.Delete(userSub.Id);
            return userRepository.DeleteById(Id);
        }

    }

}