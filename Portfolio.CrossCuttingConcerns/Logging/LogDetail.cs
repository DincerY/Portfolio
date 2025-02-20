﻿namespace Portfolio.CrossCuttingConcerns.Logging;

public class LogDetail
{
    public LogDetail()
    {
        
    }
    public LogDetail(string user, string traceId, string httpMethod, string path, Dictionary<string, string> queryParams, int? statusCode, string userAgent, string controller)
    {
        User = user;
        TraceId = traceId;
        HttpMethod = httpMethod;
        Path = path;
        QueryParams = queryParams;
        StatusCode = statusCode;
        UserAgent = userAgent;
        Controller = controller;
    }
    public string User { get; set; }
    public string TraceId { get; set; }  // İsteklerin birbirine bağlanabilmesi için
    public string HttpMethod { get; set; }
    public string Path { get; set; }
    public Dictionary<string, string> QueryParams { get; set; }
    public int? StatusCode { get; set; }
    public string UserAgent { get; set; }
    public string Controller { get; set; }

}