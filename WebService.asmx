<%@ WebService Language="C#" CodeBehind="~/App_Code/WebService.asmx.cs" Class="WebService" %>
using System;
using System.Web.Services;
using System.Xml.Serialization;
public class MyLocal {
    [WebMethod]
    public int Add(int a, int b) {
        return a + b;
    } 
}