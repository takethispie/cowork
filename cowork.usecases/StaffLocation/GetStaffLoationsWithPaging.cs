using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.StaffLocation {

    public class GetStaffLoationsWithPaging : IUseCase<IEnumerable<domain.StaffLocation>> {

        private readonly IStaffLocationRepository staffLocationRepository;
        public readonly int Page;
        public readonly int Amout;

        public GetStaffLoationsWithPaging(IStaffLocationRepository staffLocationRepository, int page, int amout) {
            this.staffLocationRepository = staffLocationRepository;
            Page = page;
            Amout = amout;
        }


        public IEnumerable<domain.StaffLocation> Execute() {
            return staffLocationRepository.GetAllWithPaging(Page, Amout);
        }

    }

}