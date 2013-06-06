<%@ Control Language="C#" AutoEventWireup="true" %>
<%
    User item = (User)HTMLHelper.GetProperty("_user");
    %>



<table >
    <tr>
        <td>
            用户名</td>
        <td>
            邮件地址</td>
    </tr>
    <tr>
        <td>
            <%=item.Name %></td>
        <td>
            <%=item.EMail %></td>
    </tr>
</table>


