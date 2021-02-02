namespace Salary.Core.DTO
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public decimal Salary { get; init; }
        public int PositionId { get; init; }
    }
}