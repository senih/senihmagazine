<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Magazine - Administration Section</title>
    <link href="style.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!-- start header -->
            <div id="header">
	            <div id="logo">
		            <h1>online magazine</h1>
	            </div>            	
            </div>
            <!-- end header -->
            <!-- start page -->
            <div id="page">
	            <!-- start content -->
	            <div id="content">
		            <div class="post">
			            <h1 class="title">Welcome to Online Magazine</h1>
			            <div class="entry">
				            <p>Application for creating online magazines. To use this application you must have installed GhostScript. </p>
			            </div>
		            </div>
                                        
		            <div class="post">
			            <h2 class="title">1. Upload PDF file</h2>
			            <div class="entry">
			            <asp:Panel ID="Panel1" runat="server" Visible="false">
			                <p>Upload *.pdf file wich you want to converto to digital online magazine</p>
                            <asp:FileUpload ID="FileUpload" runat="server" />
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" 
                                onclick="btnUpload_Click" />
                            </asp:Panel>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
			            </div>
		            </div>
		                                                   
		            <div class="post">
		                <h2 class="title">2. Create images</h2>
		                <div class="entry">
		                <asp:Panel ID="Panel2" runat="server" Visible="false">
		                    <p>In this step the application converts *.pdf file in JPEG images. The speed of conversion depends of your system configuration</p>
                            <asp:Button ID="btnCreateImages" runat="server" Text="Create Images" 
                                onclick="btnCreateImages_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </asp:Panel>
                            <asp:Label ID="Label2" runat="server"></asp:Label>
		                </div>
		            </div>		            
                    
		            <div class="post">
		                <h2 class="title">3. Create XML file</h2>
		                <div class="entry">
		                <asp:Panel ID="Panel3" runat="server" Visible="false">
		                    <p>In this step XML file is created containing information about images like size and local paths.</p>
                            <asp:Button ID="btnCreateXML" runat="server" Text="Create XML" 
                                onclick="btnCreateXML_Click" />
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          </asp:Panel>
                            <asp:Label ID="Label3" runat="server"></asp:Label>  
		                </div>
		            </div>
		                                
		            <div class="post">
		                <h2 class="title">4. Preview (optional)</h2>
		                <div class="entry">
		                <asp:Panel ID="Panel4" runat="server" Visible="false">
		                    <p>This step is optional. You can preview how does the magazine look like locally before publishing it on a web server.</p>
                            <asp:Button ID="btnPreview" runat="server" Text="Preview" 
                                onclick="btnPreview_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnNext" runat="server" Text="Next Step" 
                                onclick="btnNext_Click" />
                        </asp:Panel>
                            <asp:Label ID="Label5" runat="server"></asp:Label>
		                </div>
		            </div>
		            
                    
		            <div class="post">
		                <h2 class="title">5. Upload on web server</h2>
		                <div class="entry">
		                <asp:Panel ID="Panel5" runat="server" Visible="false">
		                    <p>In this step publishing to a web server is made. Uploading files depends of the speed of your internet connection.</p>
                            <table>
                                <tr>
                                    <td>
                                        Host:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="HostTextBox" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ErrorMessage="Required field!" ControlToValidate="HostTextBox" 
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
                                            ErrorMessage="Required field!" ControlToValidate="UserTextBox" 
                                            ForeColor="White"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                            ErrorMessage="Required field!" ControlToValidate="PasswordTextBox" 
                                            ForeColor="White"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnUploadOnServer" runat="server" Text="Upload" 
                                            onclick="btnUploadOnServer_Click" />
                                    </td>
                                    <td align="right">
                                        &nbsp;</td>
                                </tr>
                            </table>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          </asp:Panel>
                            <asp:Label ID="Label4" runat="server"></asp:Label>
		                </div>
		            </div>
		            
	            </div>
	            <!-- end content -->
	            <div class="clearboth"></div>
            </div>
            <!-- end page -->
            <!-- start footer -->
            <div id="footer">
	            <p id="legal">(c) 2008 Online Magazine. Design by <a href="http://vertigo.net.mk" target="_blank">Vertigo Net</a>.</p>
            </div>
            <!-- end footer -->    
    </div>
    </form>
</body>
</html>
