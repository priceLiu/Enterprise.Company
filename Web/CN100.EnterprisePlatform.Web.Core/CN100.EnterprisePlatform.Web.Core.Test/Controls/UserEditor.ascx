<%@ Control Language="C#" AutoEventWireup="true"  %>
<% User item = (User)HTMLHelper.GetProperty("_userinfo");
   if (item == null)
       item = new User();
   %>

<table >
    <tr>
        <td>
           用户名:</td>
        <td>
            <%=HTMLHelper.Input(InputType.text,"username").Value(item.Name) %></td>
    </tr>
    <tr>
        <td>
            密码:</td>
        <td>
            <%=HTMLHelper.Input(InputType.password,"passwd").Value(item.PassWord) %></td>
    </tr>
    <tr>
        <td>
           邮件: </td>
        <td>
            <%=HTMLHelper.Input(InputType.text,"email").Value(item.EMail) %></td>
    </tr>
    <tr>
        <td>
            备注:</td>
        <td>
            <%=HTMLHelper.Textarea(item.Remark,"remark") %></td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>

