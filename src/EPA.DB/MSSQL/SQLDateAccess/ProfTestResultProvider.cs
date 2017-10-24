using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.DB.MSSQL.SQLDateAccess
{
    public class ProfTestResultProvider : IProfTestResultProvider
    {
        public ProfTestResult GetUserResult(int points, int testId)
        {
            return new ProfTestResult();
        }
    }
}
