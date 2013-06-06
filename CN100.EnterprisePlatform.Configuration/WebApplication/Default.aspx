<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebApplication._Default" %>
<%@ Import Namespace="CN100.EnterprisePlatform.Configuration" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Test ConfigurationSection
    </h2>
    <p>
       
    </p>
    <p>
            WebDomain<a href=""><%=DomainSection.Instance.Urls.GetItemByKey("Web").Url %></a>
            ImageDomain<a href="<%=Utils.ImageDomainHelper.ImageDomain %>"><%=Utils.ImageDomainHelper.ImageDomain %></a>
    </p>
</asp:Content>
