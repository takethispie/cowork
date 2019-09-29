namespace cowork.usecases.Place.Models {

    public class CreatePlaceInput {

        public long Id { get; set; }
        public string Name { get; set; }
        public bool HighBandwidthWifi { get; set; }
        public bool UnlimitedBeverages { get; set; }
        public bool MembersOnlyArea { get; set; }
        public int CosyRoomAmount { get; set; }
        public int PrinterAmount { get; set; }
        public int LaptopAmount { get; set; }

    }

}