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
                            <asp:Button ID="btnNew" runat="server" Text="New" onclick="btnNew_Click" 
                                    CausesValidation="False" Width="60px" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnOpen" runat="server" Text="Open" onclick="btnOpen_Click" 
                                    CausesValidation="False" Width="60px" />
                            &nbsp;&nbsp;
                            </p>
                    </div>
                </div>
                
                                
                <div class="post">
                    <h1 class="title">
                        <asp:Label ID="TitleLabel" runat="server"></asp:Label></h1>
                    <div class="entry">
                        <asp:Panel ID="Panel1" runat="server">
                        <div>
                            <asp:DropDownList ID="OpenDropDownList" runat="server" 
                                DataSourceID="SqlDataSource1" DataTextField="IssueID" 
                                DataValueField="IssueID" Width="150px">
                            </asp:DropDownList>
                            <asp:Button ID="btnSelect" runat="server" Text="Select" 
                                onclick="btnSelect_Click" CausesValidation="False" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                                onclick="btnDelete_Click" CausesValidation="False" />
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                                SelectCommand="SELECT [IssueID] FROM [Issues] ORDER BY [ID]">
                            </asp:SqlDataSource>
                            
                        </div>
                        </asp:Panel>
                        <asp:Panel ID="IssuePanel" runat="server" Visible="false">
                            <div>
                            <div>                                
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="IssueNameLabel" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
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
                                            <div ID="upload" runat="server">
                                                Upload PDF file
                                            </div>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="FileUpload" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
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
                                            dpi</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Quality:</td>
                                        <td>
                                            <asp:TextBox ID="QualityTextBox" runat="server" Width="50px"></asp:TextBox>
                                            %</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                ControlToValidate="QualityTextBox" Display="Dynamic" 
                                                ErrorMessage="Required Field" ForeColor="White"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                                ControlToValidate="QualityTextBox" Display="Dynamic" 
                                                ErrorMessage="Value must be between 1-100" ForeColor="White" MaximumValue="100" 
                                                MinimumValue="1" Type="Integer"></asp:RangeValidator>
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
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            
                                            &nbsp;</td>
                                        <td align="right">
                                            <asp:Button ID="btnCreate" runat="server" onclick="btnCreate_Click" 
                                                Text="Create" Width="60px" />
                                            <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                                                Text="Update" Width="60px" />
                                            <asp:Button ID="btnPreview" runat="server" CausesValidation="False" 
                                                onclick="btnPreview_Click" Text="Preview" Width="60px" />
                                            <asp:Button ID="btnUploadOnServer" runat="server" CausesValidation="false" 
                                                onclick="btnUploadOnServer_Click" Text="Upload" Width="60px" />
                                        </td>
                                    </tr>
                                </table>
                                <div>
                                </div>
                            </div>
                        </div>
                        </asp:Panel>
                        <asp:Panel ID="UploadPanel" runat="server">
                        <table>
                            <tr>
                                <td>
                                    Issue name:
                                </td>
                                <td>
                                    <asp:DropDownList ID="IssuesDropDownList" runat="server" 
                                        DataSourceID="SqlDataSource1" DataTextField="IssueID" DataValueField="IssueID">
                                    </asp:DropDownList>
                                </td>
                                <td>                                    
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    Host:
                                </td>
                                <td>
                                    <asp:TextBox ID="HostTextBox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ErrorMessage="Required Field!" ControlToValidate="HostTextBox" 
                                        ForeColor="White"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    User:
                                </td>
                                <td>
                                    <asp:TextBox ID="UserTextBox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ErrorMessage="Required Field!" ControlToValidate="UserTextBox" 
                                        ForeColor="White"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Password:
                                </td>
                                <td>
                                    <asp:TextBox ID="PasswordTextBox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ErrorMessage="Required Field!" ControlToValidate="PasswordTextBox" 
                                        ForeColor="White"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    <asp:Button ID="btnUploadOnWebServer" runat="server" 
                                        onclick="btnUploadOnWebServer_Click" Text="Upload" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        </asp:Panel>
                        <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="clearboth"></div>
        <div id="footer">
            <p id="legal">(c) 2008 Online Magazine. Design by <a href="http://vertigo.net.mk" target="_blank">Vertigo Net</a>.</p>
        </div>
    </div>
    </form>
</body>
</html>
