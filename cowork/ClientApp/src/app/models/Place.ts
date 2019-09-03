import {TimeSlot} from "./TimeSlot";
import {Room} from "./Room";

export class Place {
    public Id: number;
    public Name: string;
    public HighBandwidthWifi: boolean;
    public UnlimitedBeverages: boolean;
    public MembersOnlyArea: boolean;
    public CosyRoomAmount: number;
    public PrinterAmount: number;
    public OpenedTimes: TimeSlot[];
    public BookableRooms: Room[];
    public LaptopAmount: number;
}
