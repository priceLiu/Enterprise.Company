<%@ Control Language="C#" AutoEventWireup="true" %>
<%
    IList<User> users = (IList<User>)HTMLHelper.GetProperty("_users");
    if (users != null)
    {
        %>
        <ul>
        <%
        foreach (User item in users)
        {%>
        <li><%=item.Name %>(<%=item.EMail %>)</li>
        <%}%>
        </ul>
<% }%>