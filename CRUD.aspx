<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="CRUD.aspx.cs" Inherits="CRUD.CRUD" %>
<html>
<head runat="server">
    <title>Employee Form</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:Label runat="server" Text="Employee Name"></asp:Label>
                <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>
            
                <asp:Label runat="server" Text="Employee Code"></asp:Label>
                <asp:TextBox ID="txtEmpCode" runat="server"></asp:TextBox>
            </div>
            <br />

            <div>
                <asp:Label runat="server" Text="Employee Designation"></asp:Label>
                <asp:TextBox ID="txtDesignation" runat="server"></asp:TextBox>
            
                <asp:Label runat="server" Text="Employee Age"></asp:Label>
                <asp:TextBox ID="txtAge" runat="server" TextMode="Number"></asp:TextBox>
            </div>
            <br />
            <div>
                <asp:Label runat="server" Text="Gender"></asp:Label>
                <asp:DropDownList ID="GenderDropdown" runat="server"
                     DataTextField="gender_name" DataValueField="gender_id"
                    ></asp:DropDownList>

            </div>
            <br />
            <div>
                <asp:Label runat="server" Text="Employee Salary"></asp:Label>
                <asp:TextBox ID="txtSalary" runat="server" TextMode="Number"></asp:TextBox>
            
                <asp:Label runat="server" Text="Department"></asp:Label>
                <asp:DropDownList ID="DepartmentDropdown" runat="server"
                                DataTextField="dept_name"    
                                DataValueField="dept_id">
                </asp:DropDownList>
            </div>
            <br />
            <div>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>
                <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click"/>
                <asp:button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
            </div>
            <br />
            <asp:GridView runat="server" ID="GridEmployee" AutoGenerateColumns="false" OnRowCommand="GridEmployee_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="EmployeeId" DataField="emp_code"/>
                    <asp:BoundField HeaderText="Name" DataField="name"/>
                    <asp:BoundField HeaderText="Designation" DataField="designation"/>
                    <asp:BoundField HeaderText="Age" DataField="age"/>
                    <asp:BoundField HeaderText="Gender" DataField="gender"/>
                    <asp:BoundField HeaderText="Salary" DataField="salary"/>
                    <asp:BoundField HeaderText="Department" DataField="department"/>

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button runat="server" CommandName="EditRow" CommandArgument='<%# Eval("emp_code") %>' Text="Edit"/>
                            <asp:Button runat="server" CommandName="DeleteRow" CommandArgument='<%# Eval("emp_code") %>' Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"/>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>

            </asp:GridView>

        </div>
    </form>
</body>
</html>