using System.Collections.Generic;

namespace cowork.domain {

    public class Place {

        public Place(long id, string name, bool highBandwidthWifi, bool unlimitedBeverages, bool membersOnlyArea,
                     int cosyRoomAmount, int printerAmount, int laptopAmount) {
            Id = id;
            Name = name;
            HighBandwidthWifi = highBandwidthWifi;
            UnlimitedBeverages = unlimitedBeverages;
            MembersOnlyArea = membersOnlyArea;
            CosyRoomAmount = cosyRoomAmount;
            PrinterAmount = printerAmount;
            LaptopAmount = laptopAmount;
            OpenedTimes = null;
            BookableRooms = null;
        }


        public Place() { }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool HighBandwidthWifi { get; set; }
        public bool UnlimitedBeverages { get; set; }
        public bool MembersOnlyArea { get; set; }
        public int CosyRoomAmount { get; set; }
        public int PrinterAmount { get; set; }
        public List<TimeSlot> OpenedTimes { get; set; }
        public List<Room> BookableRooms { get; set; }
        public int LaptopAmount { get; set; }

    }

}