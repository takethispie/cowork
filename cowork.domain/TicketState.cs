namespace cowork.domain {

    public enum TicketState {

        New,

        //ticket accepté comme traitable
        Open,

        //ticket en cours de résolution
        Doing,

        //ticket résolu
        Closed,

        //resolution du ticket en retard
        Late

    }

}