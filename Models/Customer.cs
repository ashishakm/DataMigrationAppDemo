namespace Data_Migration_App.Models
{
    public class Customer
    {
        public int CustomerId
        {
            set;
            get;
        }
        public string CustomerName
        {
            set;
            get;
        }
        public string Address
        {
            set;
            get;
        }
    }
    public class Employee
    {
        public int EmployeIeId
        {
            set;
            get;
        }
        public string EmployeeName
        {
            set;
            get;
        }
        public string Address
        {
            set;
            get;
        }
    }
    public class Common
    {
        public Customer customer
        {
            set;
            get;
        }
        public Employee employee
        {
            set;
            get;
        }
    }
}
