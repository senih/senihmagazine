<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Administration.aspx.cs" Inherits="Administration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administration section</title>
    <link href="style.css" rel="Stylesheet" type="text/css" />
    </head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="header">
            <div id="logo">
                <h1>online magazine</h1>
            </div>            
        </div>
        <div id="page">
            <div id="content">
            
                <div class="post">
                    <h1 class="title">Welcome to Online Magazine</h1>
                    <div class="entry">
                        <p>Application for creating online magazines.</p>
                    </div>
                </div>
                
                <div class="post">
                    <h1 class="title">Select option</h1>
                    <div class="entry">
                        <p>Options:&nbsp;&nbsp;
                        <asp:Button ID="btnNew" runat="server" Text="New" onclick="btnNew_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnOpen" runat="server" Text="Open" onclick="btnOpen_Click" />
                        </p>
                    </div>
                </div>
                
                <asp:Panel ID="IssuePanel" runat="server" Visible="false">                
                <div class="post">
                    <h1 class="title">
                        <asp:Label ID="TitleLabel" runat="server"></asp:Label></h1>
                    <div class="entry">
                        <p>Properties</p>
                            <div>
                                <div>
                                    Upload PDF file
                                    <asp:FileUpload ID="FileUpload" runat="server" />
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" 
                                        onclick="btnUpload_Click" CausesValidation="False" />
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </div>
                            <div>                                
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            Issue name:
                                            <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                                            <asp:DropDownList ID="OpenDropDownList" runat="server" AutoPostBack="True" 
                                                DataSourceID="SqlDataSource1" DataTextField="IssueID" DataValueField="IssueID" 
                                                onselectedindexchanged="OpenDropDownList_SelectedIndexChanged" Width="150px">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                                                SelectCommand="SELECT [IssueID] FROM [Issues] ORDER BY [ID]">
                                            </asp:SqlDataSource>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                ControlToValidate="NameTextBox" ErrorMessage="Enter folder name" 
                                                ForeColor="White"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image settings</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Resolution:</td>
                                        <td>
                                            <asp:DropDownList ID="ResolutionDropDownList" runat="server" Width="50px">
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>150</asp:ListItem>
                                                <asp:ListItem>200</asp:ListItem>
                                                <asp:ListItem>300</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            dpi</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Quality:</td>
                                        <td>
                                            <asp:TextBox ID="QualityTextBox" runat="server" Width="50px"></asp:TextBox>
                                        </td>
                                        <td>
                                            %</td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                ControlToValidate="QualityTextBox" ErrorMessage="Required Field" 
                                                ForeColor="White"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                                ControlToValidate="QualityTextBox" ErrorMessage="Value must be between 1-100" 
                                                ForeColor="White" MaximumValue="100" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Text antialiasing:</td>
                                        <td>
                                            <asp:DropDownList ID="TxtAliasingDropDownList" runat="server" Width="50px">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Graphics antialiasing:</td>
                                        <td>
                                            <asp:DropDownList ID="GraphAliasingDropDownList" runat="server" Width="50px">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="3">
                                            <asp:Button ID="btnCreate" runat="server" Text="Create" 
                                                onclick="btnCreate_Click" />
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" onclick="btnUpdate_Click" />
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" 
                                                onclick="btnPreview_Click" />
                                        </td>
                                        <td align="right">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="3">
                                    <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                                        </td>
                                        <td align="right">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <div>
                                </div>
                            </div>
                        </div>
                        <div>
                            
                        </div>
                    </div>
                </div>
                </asp:Panel>
            </div>
        </div>
        <div class="clearboth"></div>
        <div id="footer"></div>
    </div>
    </form>
</body>
</html>
