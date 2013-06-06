<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CN100.EnterprisePlatform.WebApplication._Default" EnableViewState="false" %>
<%--<%@ OutputCache Duration="86400" Location="Client" VaryByParam="None" %>--%>
<%@ OutputCache CacheProfile="Cache1Day" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to ASP.NET!
    </h2>
    <p>
        To learn more about ASP.NET visit <a href="http://www.asp.net" title="ASP.NET Website">www.asp.net</a>.
    </p>
    <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
            <img height="500" width="600" src="http://img190.cn100.com/001201207040010cd23432333246ffb21e9a532748e7b7.jpg" alt="" />
    </p>
</asp:Content>
