using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.StaffLocation {

    public class GetAllStaffLocations : IUseCase<IEnumerable<domain.StaffLocation>> {

        private readonly IStaffLocationRepository staffLocationRepository;

        public GetAllStaffLocations(IStaffLocationRepository staffLocationRepository) {
            this.staffLocationRepository = staffLocationRepository;
        }


        public IEnumerable<domain.StaffLocation> Execute() {
            return staffLocationRepository.GetAll();
        }

    }

}