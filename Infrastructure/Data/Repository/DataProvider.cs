using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Infrastructure.Data.Repsitory
{
    public sealed class DataProvider
    {
        private static DataProvider instance;
        private DataProvider()
        {
            Companies = new CompanyRepository();
            Addresses = new AddressRepository();
            Accounts = new AccountRepsitory();
            Employees = new EmployeeRepository();
            Transactions = new TransactionRepository();
        }
        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider();
                }
                return instance;
            }
        }

        public CompanyRepository Companies { get; set; }
        public AddressRepository Addresses { get; set; }
        public AccountRepsitory Accounts { get; set; }
        public EmployeeRepository Employees { get; set; }
        public TransactionRepository Transactions { get; set; }
    }
}