namespace cowork.domain {

    public class SubscriptionType {

        public SubscriptionType() { }


        public SubscriptionType(long id, string name, int fixedContractDurationMonth,
                                double priceFirstHour, double priceNextHalfHour, double priceDay,
                                double priceDayStudent,
                                double monthlyFeeFixedContract, double monthlyFeeContractFree, string description) {
            Id = id;
            Name = name;
            FixedContractDurationMonth = fixedContractDurationMonth;
            PriceFirstHour = priceFirstHour;
            PriceNextHalfHour = priceNextHalfHour;
            PriceDay = priceDay;
            PriceDayStudent = priceDayStudent;
            MonthlyFeeFixedContract = monthlyFeeFixedContract;
            MonthlyFeeContractFree = monthlyFeeContractFree;
            Description = description;
        }


        public long Id { get; set; }
        public string Name { get; set; }
        public int FixedContractDurationMonth { get; set; }
        public double PriceFirstHour { get; set; }
        public double PriceNextHalfHour { get; set; }
        public double PriceDay { get; set; }
        public double PriceDayStudent { get; set; }
        public double MonthlyFeeFixedContract { get; set; }
        public double MonthlyFeeContractFree { get; set; }
        public string Description { get; set; }

    }

}