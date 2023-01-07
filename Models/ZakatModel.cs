using System;

namespace AsthaIslamicService.Models
{
    public class ZakatModel
    {
        public int Year { get; set; }
        public string Id { get; set; }
        public double Cash { get; set; }
        public double CashInBankaccount { get; set; }
        public double ValueOfGold { get; set; }
        public double ValueOfSilver { get; set; }
        public double StockMarketInvestment { get; set; }
        public double OtherInvestments { get; set; }
        public double HouseRent { get; set; }
        public double Property { get; set; }
        public double CashInBusiness { get; set; }
        public double Products { get; set; }
        public double Pension { get; set; }
        public double OtherCapital { get; set; }
        public double Agriculture { get; set; }
        public double CreditCardPayment { get; set; }
        public double CarPayment { get; set; }
        public double BusinessPayment { get; set; }
        public double FamilyLoan { get; set; }
        public double OtherLoans { get; set; }
        public double FamilyLoansAndOthers { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
