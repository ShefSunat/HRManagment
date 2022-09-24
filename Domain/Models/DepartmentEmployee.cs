namespace Domain.Models;
public class DepartmentEmployee
{
    public Int64 DepartmentId{get;set;}
    public  DateTime FromDate{get;set;}
    public DateTime ToDate{get;set;}
        public Int64 EmployeeId{get;set;}
}