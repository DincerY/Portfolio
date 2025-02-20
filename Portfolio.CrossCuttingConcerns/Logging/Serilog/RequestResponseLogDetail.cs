using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.CrossCuttingConcerns.Logging.Serilog;

public class RequestResponseLogDetail : LogDetail
{
    
    //Request veya response için ekstra bir tip gerekirse buradan alıp kullanırız

    public RequestResponseLogDetail() : base()
    {
    }

    public RequestResponseLogDetail(string user, string traceId, string httpMethod, string path, Dictionary<string, string> queryParams, int? statusCode, string userAgent, string controller) : base(user, traceId, httpMethod, path, queryParams, statusCode, userAgent, controller)
    {
    }
}