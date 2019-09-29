namespace cowork.usecases.Ware.Models {

    public class CreateWareInput {

        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public long PlaceId { get; set; }
        public bool InStorage { get; set; }

    }

}