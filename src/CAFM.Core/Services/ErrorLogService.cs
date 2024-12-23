using CAFM.Database;
using CAFM.Database.Entities;

namespace CAFM.Core.Services
{
    public class ErrorLogService
    {
        private readonly CAFMDbContext _dbContext;

        public ErrorLogService(CAFMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LogErrorAsync(string userName, int companyId, string? localHost, string errSource, string errMsg, byte errFrom, string? serial, string? notes = "")
        {
            try
            {
                var errorLog = new ErrorsLog
                {
                    UserName = userName,
                    CompanyID = companyId,
                    LocalHost = localHost,
                    ErrSource = errSource,
                    ErrMsg = errMsg,
                    ErrFrom = errFrom,
                    Serial = serial,
                    Notes = notes
                };

                _dbContext.ErrorsLogs.Add(errorLog);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while logging: {ex.Message}");
            }
        }
    }

}
