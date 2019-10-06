namespace cowork.usecases.Place.Models {

    public class CreatePlaceInput {

        public CreatePlaceInput(string name, bool highBandwidthWifi, bool unlimitedBeverages, bool membersOnlyArea, int cosyRoomAmount, int printerAmount, int laptopAmount) {
            Name = name;
            HighBandwidthWifi = highBandwidthWifi;
            UnlimitedBeverages = unlimitedBeverages;
            MembersOnlyArea = membersOnlyArea;
            CosyRoomAmount = cosyRoomAmount;
            PrinterAmount = printerAmount;
            LaptopAmount = laptopAmount;
        }

        public string Name { get; set; }
        public bool HighBandwidthWifi { get; set; }
        public bool UnlimitedBeverages { get; set; }
        public bool MembersOnlyArea { get; set; }
        public int CosyRoomAmount { get; set; }
        public int PrinterAmount { get; set; }
        public int LaptopAmount { get; set; }

    }

}