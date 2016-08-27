Imports System.Data.OleDb
Imports System.Web.UI.Page
Imports System.Data
Imports System.Configuration

Partial Class SHIP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load, ShiftTM.DataBound

        If Not IsPostBack Then
            TMorSumm.Text = "Verify Team Members"
            ShiftTM.Visible = False
            SideNavigation.Visible = False
        End If

    End Sub

    Protected Sub ShiftTM_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ShiftTM.RowCommand

        If e.CommandName = "Select" Then
            EditTM.Visible = True
            ShiftTM.Visible = False
            DeptLabel.Text = "Edit Team Member"
            SideNavigation.Visible = False
            sideNavPanel.Visible = False
            EditTM.DataSourceID = "EditTMDataSource"
            EditTM.DataBind()
            TMCountLabel.Text = ""

        End If

    End Sub


    Protected Sub TMorSumm_click(ByVal sender As Object, ByVal e As EventArgs)

        If TMorSumm.Text = "Verify Team Members" Then
            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection Strings

            Dim TMCount As Integer = 0

            Dim CountSqlString As String
            CountSqlString = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE (TMs.DepartmentID)=4 AND (TMs.ShiftID)=2"

            Dim dbComm As New OleDbCommand(CountSqlString, dbConn)

            Try
                dbConn.Open()
                TMCount = Convert.ToInt32(dbComm.ExecuteScalar())
                TMCountLabel.Text = TMCount & " Team Members"

                dbConn.Close()

            Finally
                dbConn.Close()
            End Try

            SummPanel.Visible = False
            sideNavPanel.Visible = True
            SideNavigation.Visible = True

            ShiftTM.Visible = True
            ShiftTM.Columns(3).Visible = True
            ShiftTM.Columns(4).Visible = True
            TMorSumm.Text = "View Summary"
        Else
            sideNavPanel.Visible = True
            SideNavigation.Visible = False
            TMCountLabel.Text = ""
            SummPanel.Visible = True
            ShiftTM.Visible = False
            TMorSumm.Text = "Verify Team Members"
        End If

    End Sub

    'Team Member Functions

    Protected Sub FindTMButton_Click(ByVal sender As Object, ByVal e As EventArgs)


        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        Dim TMCount As Integer = 0

        Dim CountSqlString As String
        CountSqlString = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE TMs.LastName =@LastName"

        Dim LastName As TextBox = Page.FindControl("ctl00$MainContent$FindTM")

        Dim dbComm As New OleDbCommand(CountSqlString, dbConn)
        dbComm.Parameters.Add("@LastName", OleDbType.VarWChar)
        dbComm.Parameters("@LastName").Value = LastName.Text


        Try
            dbConn.Open()
            TMCount = Convert.ToInt32(dbComm.ExecuteScalar())
            TMCountLabel.Text = TMCount & " Team Members"

        Finally
            dbConn.Close()
        End Try

        ShiftTM.DataSourceID = "FindTMDataSource"
        ShiftTM.DataBind()



    End Sub
    Protected Sub FindTMButtonByID_Click(ByVal sender As Object, ByVal e As EventArgs)

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        Dim TMCount As Integer = 0

        Dim CountSqlString As String
        CountSqlString = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE TMs.EmployeeID =@EmployeeID"

        Dim EmployeeID As TextBox = Page.FindControl("ctl00$MainContent$FindTMByID")

        Dim dbComm As New OleDbCommand(CountSqlString, dbConn)
        dbComm.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
        dbComm.Parameters("@EmployeeID").Value = EmployeeID.Text

        Try
            dbConn.Open()
            TMCount = Convert.ToInt32(dbComm.ExecuteScalar())
            TMCountLabel.Text = TMCount & " Team Members"

        Finally
            dbConn.Close()
        End Try

        ShiftTM.DataSourceID = "FindTMDataSourceByID"
        ShiftTM.DataBind()


    End Sub

    Protected Sub ClearButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        FindTM.Text = ""
        FindTMByID.Text = ""


        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        Dim TMCount As Integer = 0

        Dim CountSqlString As String
        CountSqlString = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE (TMs.DepartmentID)=4 AND (TMs.ShiftID)=2"

        Dim dbComm As New OleDbCommand(CountSqlString, dbConn)

        Try
            dbConn.Open()
            TMCount = Convert.ToInt32(dbComm.ExecuteScalar())
            TMCountLabel.Text = TMCount & " Team Members"

        Finally
            dbConn.Close()
        End Try

        ShiftTM.DataSourceID = "TMDataSource"
        ShiftTM.DataBind()


    End Sub

    Protected Sub AddButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddButton.Click

        ShiftTM.Visible = False
        AddTM.Visible = True
        SideNavigation.Visible = False
        DeptLabel.Text = "Add a Team Member"
        TMCountLabel.Text = ""
        sideNavPanel.Visible = False

        Dim EmpID As TextBox = Page.FindControl("ctl00$MainContent$AddTM$DetEmployeeID")
        Dim FirstName As TextBox = Page.FindControl("ctl00$MainContent$AddTM$DetFirstName")
        Dim LastName As TextBox = Page.FindControl("ctl00$MainContent$AddTM$DetLastName")

        EmpID.Text = ""
        FirstName.Text = ""
        LastName.Text = ""


    End Sub

    Protected Sub EditTM_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewCommandEventArgs)

        If e.CommandName = "Cancel" Then

            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection Strings

            Dim TMCount As Integer = 0

            Dim CountSqlString As String
            CountSqlString = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE (TMs.DepartmentID)=4 AND (TMs.ShiftID)=2"

            Dim dbComm As New OleDbCommand(CountSqlString, dbConn)

            Try
                dbConn.Open()
                TMCount = Convert.ToInt32(dbComm.ExecuteScalar())
                TMCountLabel.Text = TMCount & " Team Members"

            Finally
                dbConn.Close()
            End Try


            ShiftTM.DataSourceID = "TMDataSource"
            ShiftTM.DataBind()

            EditTM.Visible = False
            ShiftTM.Visible = True

            DeptLabel.Text = "Shipping 2nd Shift"
            DeptLabel.Visible = True
            SideNavigation.Visible = True
            sideNavPanel.Visible = True
            FindTM.Text = ""
            FindTMByID.Text = ""


        Else

            If e.CommandName = "DeleteThis" Then
                'Code to check whether TM is in the productivity table if not then delete

                'Connection Strings to Access DB
                Dim connectionStringAccess As String = _
               ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
                Dim dbConn As New OleDbConnection(connectionStringAccess)
                'end Connection Strings

                Dim DeleteSqlString As String
                DeleteSqlString = "DELETE FROM TMS WHERE (EmployeeID = @EmployeeID) "

                Dim sqlStrTMCount As String
                sqlStrTMCount = "SELECT Count(PROD.EmployeeID) AS CountOfEmployeeID FROM PROD WHERE(PROD.EmployeeID)=@EmployeeID"

                Dim RecordCount As Int32 = 0

                Dim TMDetailsView As DetailsView = EditTM
                Dim TMParam As String = TMDetailsView.SelectedValue

                Dim dbCommTMCount As New OleDbCommand(sqlStrTMCount, dbConn)
                dbCommTMCount.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
                dbCommTMCount.Parameters("@EmployeeID").Value = TMParam

                Dim dbCommDelete As New OleDbCommand(DeleteSqlString, dbConn)
                dbCommDelete.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
                dbCommDelete.Parameters("@EmployeeID").Value = TMParam

                Try
                    dbConn.Open()
                    RecordCount = Convert.ToInt32(dbCommTMCount.ExecuteScalar())

                    If RecordCount <> 0 Then

                        DeptLabel.Text = "Cannot Delete Team Member because Productivity Data Exists for that Team Member."
                        TMCountLabel.Text = ""
                        ShiftTM.Visible = False
                        Exit Sub
                    End If

                Finally
                    dbConn.Close()
                End Try



                Try
                    dbConn.Open()
                    dbCommDelete.ExecuteNonQuery()
                Finally
                    dbConn.Close()
                End Try

                Dim TMCount As Integer = 0

                Dim CountSqlString As String
                CountSqlString = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE (TMs.DepartmentID)=4 AND (TMs.ShiftID)=2"

                Dim dbComm As New OleDbCommand(CountSqlString, dbConn)

                Try
                    dbConn.Open()
                    TMCount = Convert.ToInt32(dbComm.ExecuteScalar())
                    TMCountLabel.Text = TMCount & " Team Members"

                Finally
                    dbConn.Close()
                End Try


                ShiftTM.DataSourceID = "TMDataSource"
                ShiftTM.DataBind()
                EditTM.Visible = False
                ShiftTM.Visible = True
                DeptLabel.Text = "Shipping 2nd Shift"
                DeptLabel.Visible = True
                SideNavigation.Visible = True
                sideNavPanel.Visible = True
                FindTM.Text = ""
                FindTMByID.Text = ""

            End If
        End If

    End Sub

    Protected Sub EditTM_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdatedEventArgs)

        EditTM.Visible = False
        ShiftTM.Visible = True
        DeptLabel.Visible = True
        SideNavigation.Visible = True


        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        Dim TMCount As Integer = 0

        Dim CountSqlString As String
        CountSqlString = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE (TMs.DepartmentID)=4 AND (TMs.ShiftID)=2"

        Dim dbComm As New OleDbCommand(CountSqlString, dbConn)

        Try
            dbConn.Open()
            TMCount = Convert.ToInt32(dbComm.ExecuteScalar())
            TMCountLabel.Text = TMCount & " Team Members"

        Finally
            dbConn.Close()
        End Try


        ShiftTM.DataSourceID = "TMDataSource"
        ShiftTM.DataBind()
        DeptLabel.Text = "Shipping 2nd Shift"
        FindTM.Text = ""
        FindTMByID.Text = ""

        SideNavigation.Visible = True
        sideNavPanel.Visible = True


    End Sub

    Protected Sub AddTM_ItemInserting(ByVal sender As Object, _
         ByVal e As DetailsViewInsertEventArgs)

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        Dim RecordCount As Int32

        Dim InsertSqlString As String
        InsertSqlString = "INSERT INTO TMs (EmployeeID, FirstName, LastName,  DepartmentID, ShiftID) VALUES (@EmployeeID, @FirstName, @LastName,  @DepartmentID, @ShiftID)"

        Dim sqlStrEmpID As String
        sqlStrEmpID = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE (TMs.EmployeeID) = @EmployeeID"

        Dim EmpID As TextBox = Page.FindControl("ctl00$MainContent$AddTM$DetEmployeeID")
        Dim FirstName As TextBox = Page.FindControl("ctl00$MainContent$AddTM$DetFirstName")
        Dim LastName As TextBox = Page.FindControl("ctl00$MainContent$AddTM$DetLastName")
        Dim DepartmentID As DropDownList = Page.FindControl("ctl00$MainContent$AddTM$DeptDownList")
        Dim ShiftID As DropDownList = Page.FindControl("ctl00$MainContent$AddTM$ShiftDropDownList")

        Dim dbCommEmpID As New OleDbCommand(sqlStrEmpID, dbConn)
        dbCommEmpID.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
        dbCommEmpID.Parameters("@EmployeeID").Value = EmpID.Text

        Dim dbCommInsert As New OleDbCommand(InsertSqlString, dbConn)
        dbCommInsert.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
        dbCommInsert.Parameters("@EmployeeID").Value = EmpID.Text
        dbCommInsert.Parameters.Add("@FirstName", OleDbType.VarWChar)
        dbCommInsert.Parameters("@FirstName").Value = FirstName.Text
        dbCommInsert.Parameters.Add("@LastName", OleDbType.VarWChar)
        dbCommInsert.Parameters("@LastName").Value = LastName.Text
        dbCommInsert.Parameters.Add("@DepartmentID", OleDbType.Integer)
        dbCommInsert.Parameters("@DepartmentID").Value = Convert.ToInt32(DepartmentID.SelectedValue)
        dbCommInsert.Parameters.Add("@ShiftID", OleDbType.Integer)
        dbCommInsert.Parameters("@ShiftID").Value = Convert.ToInt32(ShiftID.SelectedValue)

        Try
            dbConn.Open()
            RecordCount = Convert.ToInt32(dbCommEmpID.ExecuteScalar())

            If RecordCount = 1 Then

                DeptLabel.Text = "Employee ID already exists. Click cancel to search for Team Member."
                ShiftTM.Visible = False
                TMCountLabel.Text = ""

                Exit Sub
            End If

        Finally
            dbConn.Close()
        End Try

        Try
            dbConn.Open()
            dbCommInsert.ExecuteNonQuery()

        Finally
            dbConn.Close()
        End Try


        Dim TMCount As Integer = 0

        Dim CountSqlString As String
        CountSqlString = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE (TMs.DepartmentID)=4 AND (TMs.ShiftID)=2"

        Dim dbComm As New OleDbCommand(CountSqlString, dbConn)

        Try
            dbConn.Open()
            TMCount = Convert.ToInt32(dbComm.ExecuteScalar())
            TMCountLabel.Text = TMCount & " Team Members"

        Finally
            dbConn.Close()
        End Try

        ShiftTM.DataSourceID = "TMDataSource"
        ShiftTM.DataBind()
        DeptLabel.Text = "Shipping 2nd Shift"

        ShiftTM.Visible = True
        AddTM.Visible = False
        SideNavigation.Visible = True
        sideNavPanel.Visible = True


    End Sub

    Protected Sub AddTM_ModeChanging(ByVal sender As Object, ByVal e As DetailsViewModeEventArgs)

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        Dim TMCount As Integer = 0

        Dim CountSqlString As String
        CountSqlString = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE (TMs.DepartmentID)=4 AND (TMs.ShiftID)=2"

        Dim dbComm As New OleDbCommand(CountSqlString, dbConn)

        Try
            dbConn.Open()
            TMCount = Convert.ToInt32(dbComm.ExecuteScalar())
            TMCountLabel.Text = TMCount & " Team Members"

        Finally
            dbConn.Close()
        End Try

        ShiftTM.DataSourceID = "TMDataSource"
        ShiftTM.DataBind()
        DeptLabel.Text = "Shipping 2nd Shift"

        ShiftTM.Visible = True
        AddTM.Visible = False
        SideNavigation.Visible = True
        sideNavPanel.Visible = True
    End Sub

    Protected Sub AddTM_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewCommandEventArgs)

        If e.CommandName = "Cancel" Then

            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection Strings

            Dim TMCount As Integer = 0

            Dim CountSqlString As String
            CountSqlString = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE (TMs.DepartmentID)=4 AND (TMs.ShiftID)=2"

            Dim dbComm As New OleDbCommand(CountSqlString, dbConn)

            Try
                dbConn.Open()
                TMCount = Convert.ToInt32(dbComm.ExecuteScalar())
                TMCountLabel.Text = TMCount & " Team Members"

            Finally
                dbConn.Close()
            End Try


            ShiftTM.DataSourceID = "TMDataSource"
            ShiftTM.DataBind()

            EditTM.Visible = False
            ShiftTM.Visible = True
            DeptLabel.Text = "Shipping 2nd Shift"
            DeptLabel.Visible = True
            SideNavigation.Visible = True
            sideNavPanel.Visible = True
            FindTM.Text = ""
            FindTMByID.Text = ""



        End If

    End Sub

End Class