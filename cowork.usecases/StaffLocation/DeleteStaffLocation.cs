using cowork.domain.Interfaces;

namespace cowork.usecases.StaffLocation {

    public class DeleteStaffLocation : IUseCase<bool> {

        private readonly IStaffLocationRepository staffLocationRepository;
        public readonly long Id;

        public DeleteStaffLocation(IStaffLocationRepository staffLocationRepository, long id) {
            this.staffLocationRepository = staffLocationRepository;
            Id = id;
        }


        public bool Execute() {
            return staffLocationRepository.Delete(Id);
        }

    }

}