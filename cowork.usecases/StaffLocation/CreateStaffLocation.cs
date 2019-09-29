using cowork.domain.Interfaces;

namespace cowork.usecases.StaffLocation {

    public class CreateStaffLocation : IUseCase<long> {

        private readonly IStaffLocationRepository staffLocationRepository;
        public readonly domain.StaffLocation StaffLocation;

        public CreateStaffLocation(IStaffLocationRepository staffLocationRepository, domain.StaffLocation staffLocation) {
            this.staffLocationRepository = staffLocationRepository;
            StaffLocation = staffLocation;
        }


        public long Execute() {
            return staffLocationRepository.Create(StaffLocation);
        }

    }

}