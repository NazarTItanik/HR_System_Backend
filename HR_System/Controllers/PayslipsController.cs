using HR_System.Data;
using HR_System.Enums;
using HR_System.Models;
using HR_System.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayslipsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PayslipsController(AppDbContext context)
        {
            _context = context;
        }
        //[HttpPost("generatePayslip")]
        //public async Task<IActionResult> GenerateBatchCustom([FromBody] GenerateCustomPayslipsDto request)
        //{
        //    // 1. Validation
        //    if (request.StartDate > request.EndDate)
        //        return BadRequest("Start date cannot be after end date.");

        //    // 2. Get active contracts
        //    var contractsQuery = _context.EmploymentContracts.Where(c => c.IsActive);
        //    if (request.EmployeeIds != null && request.EmployeeIds.Any())
        //    {
        //        contractsQuery = contractsQuery.Where(c => request.EmployeeIds.Contains(c.EmployeeId));
        //    }
        //    var activeContracts = await contractsQuery.ToListAsync();
        //    var targetEmployeeIds = activeContracts.Select(c => c.EmployeeId).ToList();

        //    if (!activeContracts.Any()) return BadRequest("No active contracts found.");


        //    Console.WriteLine("Start date: " + request.StartDate);
        //    Console.WriteLine("End date: " + request.EndDate);

        //    // 3. BULK FETCH: Filter strictly by the Custom Date Range
        //    var allAttendances = await _context.Attendances
        //        .Where(a => targetEmployeeIds.Contains(a.EmployeeId)
        //                 && a.Date >= request.StartDate
        //                 && a.Date <= request.EndDate)
        //        .ToListAsync();

        //    var allLeaves = await _context.Leaves
        //        .Where(l => targetEmployeeIds.Contains(l.EmployeeId)
        //                 && l.StartDate >= request.StartDate
        //                 && l.EndDate <= request.EndDate)
        //        .ToListAsync();

        //    var grossPayments = new List<double>();

        //    // Get standard monthly hours to calculate the base hourly rate (e.g., 160)
        //    var standardMonthlyHours = 160;

        //    // 4. Process each employee
        //    foreach (var contract in activeContracts)
        //    {
        //        // Sum hours within the custom range
        //        var workedHours = allAttendances
        //            .Where(a => a.EmployeeId == contract.EmployeeId)
        //            .Sum(a => a.TotalHoursWorked);

        //        Console.WriteLine("workedHours ----------------: " + workedHours);


        //        var paidLeaveHours = allLeaves
        //            .Where(l => l.EmployeeId == contract.EmployeeId && l.IsPaid)
        //            .Sum(l => l.TotalDays * 8);
        //        Console.WriteLine("paidLeaveHours ----------------: " + paidLeaveHours);

        //        var totalPayableHours = (decimal)workedHours + paidLeaveHours;

        //        //Console.WriteLine("PAyable hours ----------------: " + totalPayableHours);

        //        // Calculate the value of 1 hour based on their monthly contract
        //        var hourlyRate = contract.BaseSalary / standardMonthlyHours;

        //        // Pay them only for the hours they accumulated in this specific date range
        //        double calculatedGross = (double)hourlyRate * (double)totalPayableHours;

        //        //var newPayslip = new Payslip
        //        //{
        //        //    Id = Guid.NewGuid(),
        //        //    EmployeeId = contract.EmployeeId,
        //        //    GrossSalary = Math.Round(calculatedGross, 2),
        //        //    NetSalary = Math.Round(calculatedGross, 2),
        //        //    GenerationDate = DateTime.UtcNow,
        //        //    LineItems = new List<PayslipLineItem>()
        //        //};

        //        //generatedPayslips.Add(newPayslip);
        //        grossPayments.Add(calculatedGross);
        //    }

        //    //_context.Payslips.AddRange(generatedPayslips);
        //    //await _context.SaveChangesAsync();

        //    return Ok(grossPayments);
        //}



        [HttpPost("generatePayslip")]
        public async Task<IActionResult> GenerateBatchCustom([FromBody] GenerateCustomPayslipsDto request)
        {
            if (request.StartDate > request.EndDate)
                return BadRequest("Start date cannot be after end date.");

            var contractsQuery = _context.EmploymentContracts.Where(c => c.IsActive);
            if (request.EmployeeIds != null && request.EmployeeIds.Any())
            {
                contractsQuery = contractsQuery.Where(c => request.EmployeeIds.Contains(c.EmployeeId));
            }

            var activeContracts = await contractsQuery.ToListAsync();
            var targetEmployeeIds = activeContracts.Select(c => c.EmployeeId).ToList();

            if (!activeContracts.Any()) return BadRequest("No active contracts found.");

            var allAttendances = await _context.Attendances
                .Where(a => targetEmployeeIds.Contains(a.EmployeeId)
                         && a.Date.Date >= request.StartDate.Date
                         && a.Date.Date <= request.EndDate.Date
                         && a.Status == AttendanceStatus.Validated)
                .ToListAsync();

            var allLeaves = await _context.Leaves
                .Where(l => targetEmployeeIds.Contains(l.EmployeeId)
                         && l.StartDate.Date >= request.StartDate.Date
                         && l.EndDate.Date <= request.EndDate.Date)
                .ToListAsync();

            // Changed from a list of decimals to a list of Payslip objects
            var generatedPayslips = new List<Payslip>();

            // Process each employee
            foreach (var contract in activeContracts)
            {
                var workedHours = allAttendances
                    .Where(a => a.EmployeeId == contract.EmployeeId)
                    .Sum(a => (decimal)a.TotalHoursWorked);

                var paidLeaveHours = allLeaves
                    .Where(l => l.EmployeeId == contract.EmployeeId && l.IsPaid)
                    .Sum(l => (decimal)l.TotalDays * 8m);

                var totalPayableHours = workedHours + paidLeaveHours;

                decimal expectedWeeklyHours = contract.WorkType == Enums.WorkType.PartTime ? 20m : 40m;
                decimal expectedMonthlyHours = expectedWeeklyHours * 4m;
                decimal effectiveHourlyRate = 0m;

                switch (contract.WageType)
                {
                    case Enums.WageType.Hourly:
                        effectiveHourlyRate = contract.BaseSalary;
                        break;
                    case Enums.WageType.Weekly:
                        effectiveHourlyRate = contract.BaseSalary / expectedWeeklyHours;
                        break;
                    case Enums.WageType.Monthly:
                    default:
                        effectiveHourlyRate = contract.BaseSalary / expectedMonthlyHours;
                        break;
                }

                // 1. Calculate Gross
                decimal calculatedGross = effectiveHourlyRate * totalPayableHours;

                // 2. Calculate Net (Example: Flat 20% tax deduction. Adjust this to your real tax logic)
                decimal taxRate = 0.20m;
                decimal calculatedNet = calculatedGross - (calculatedGross * taxRate);

                // 3. Create the Payslip Record
                var newPayslip = new Payslip
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = contract.EmployeeId,
                    PeriodStart = request.StartDate,
                    PeriodEnd = request.EndDate,
                    GrossSalary = Math.Round(calculatedGross, 2),
                    NetSalary = Math.Round(calculatedNet, 2),
                    GenerationDate = DateTime.UtcNow,
                    Status = PayslipStatus.Unpaid
                };

                generatedPayslips.Add(newPayslip);
            }

            // 4. Save to Database
            _context.Payslips.AddRange(generatedPayslips);
            await _context.SaveChangesAsync();

            // 5. Return the created objects so the frontend can display them immediately
            return Ok(generatedPayslips);
        }

        private int GetWorkingDays(DateTime startDate, DateTime endDate)
        {
            int workingDays = 0;
            for (DateTime date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                // Skip weekends
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays++;
                }
            }
            return workingDays;
        }
    }
}
