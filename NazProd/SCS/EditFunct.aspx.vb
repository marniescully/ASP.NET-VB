
Imports System.Data.OleDb
Imports System.Web.UI.Page
Imports System.Data
Imports System.Configuration

Partial Class SCS_EditSCSFunct
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection Strings

            'Count the # of Functions listed for this department
            Dim CountSqlString As String
            CountSqlString = "SELECT Count(Functions.Function) AS CountOfFunctions FROM Functions WHERE Functions.Department =9"
            Dim FunctCount As Integer = 0
            Dim dbComm As New OleDbCommand(CountSqlString, dbConn)

            Try
                dbConn.Open()
                FunctCount = Convert.ToInt32(dbComm.ExecuteScalar())
                If FunctCount = 0 Then
                    'Make Add Details View visible everything else invisible
                    FunctionLabel.Text = "Add a Split Case Stock Function"
                    AddFunction.Visible = True
                    SCSFunct.Visible = False
                    SideNavigation.Visible = False

                    Dim FunctionTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetFunction")
                    Dim RateTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetRate")
                    Dim DescriptionTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetDescrip")

                    FunctionTextBox.Text = ""
                    RateTextBox.Text = ""
                    DescriptionTextBox.Text = ""

                Else
                    SCSFunct.DataSourceID = "SCSFunctions"
                    SCSFunct.DataBind()
                    FunctionLabel.Text = "Split Case Stock Functions"

                    SCSFunct.Visible = True
                    AddFunction.Visible = False
                    EditFunct.Visible = False
                    SideNavigation.Visible = True
                End If

            Finally
                dbConn.Close()
            End Try


        End If
    End Sub

    Protected Sub SCSFunct_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles SCSFunct.RowCommand

        If e.CommandName = "Add" Then
            'Make Add Details View visible everything else invisible
            FunctionLabel.Text = "Add a Split Case Stock Function"
            AddFunction.Visible = True
            SCSFunct.Visible = False
            SideNavigation.Visible = False

            Dim FunctionTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetFunction")
            Dim RateTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetRate")
            Dim DescriptionTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetDescrip")

            FunctionTextBox.Text = ""
            RateTextBox.Text = ""
            DescriptionTextBox.Text = ""

        End If

        If e.CommandName = "Select" Then
            'Make Edit Details View visible everything else invisible
            EditFunct.Visible = True

            EditFunct.DataSourceID = "EditFunctDataSource"
            EditFunct.DataBind()

            SCSFunct.Visible = False
            FunctionLabel.Text = "Edit Function"
            SideNavigation.Visible = False

        End If



    End Sub
    Protected Sub EditFunct_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewCommandEventArgs)

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection String

        'Count the # of Functions listed for this department
        Dim CountSqlString As String
        CountSqlString = "SELECT Count(Functions.Function) AS CountOfFunctions FROM Functions WHERE Functions.Department =9"
        Dim FunctCount As Integer = 0
        Dim dbComm As New OleDbCommand(CountSqlString, dbConn)

        If e.CommandName = "DeleteThis" Then
            'Code to check whether function is in SCS productivity table if not then delete

            Dim DeleteSqlString As String
            DeleteSqlString = "DELETE FROM Functions WHERE (Function = @Function) AND (Department=9) "

            Dim sqlStrFunction As String
            sqlStrFunction = "SELECT Count(PROD.Function) AS CountOfFunction FROM PROD WHERE (PROD.Function) = @Function AND (PROD.DEPARTMENT=9)"

            Dim RecordCount As Int32 = 0

            Dim FunctionDetailsView As DetailsView = EditFunct
            Dim FunctParam As String = FunctionDetailsView.SelectedValue

            Dim dbCommFunction As New OleDbCommand(sqlStrFunction, dbConn)
            dbCommFunction.Parameters.Add("@Function", OleDbType.VarWChar)
            dbCommFunction.Parameters("@Function").Value = FunctParam

            Dim dbCommDelete As New OleDbCommand(DeleteSqlString, dbConn)
            dbCommDelete.Parameters.Add("@Function", OleDbType.VarWChar)
            dbCommDelete.Parameters("@Function").Value = FunctParam

            Try
                dbConn.Open()
                RecordCount = Convert.ToInt32(dbCommFunction.ExecuteScalar())

                If RecordCount <> 0 Then

                    FunctionLabel.Text = "Cannot Delete Function because Productivity Data Exists for that function."

                    dbConn.Close()
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


            Try
                dbConn.Open()
                FunctCount = Convert.ToInt32(dbComm.ExecuteScalar())
                If FunctCount = 0 Then
                    'Make Add Details View visible everything else invisible
                    FunctionLabel.Text = "Add a Split Case Stock Function"
                    EditFunct.Visible = "False"
                    AddFunction.Visible = True

                    SCSFunct.Visible = False
                    SideNavigation.Visible = False

                    Dim FunctionTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetFunction")
                    Dim RateTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetRate")
                    Dim DescriptionTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetDescrip")

                    FunctionTextBox.Text = ""
                    RateTextBox.Text = ""
                    DescriptionTextBox.Text = ""

                Else
                    SCSFunct.DataSourceID = "SCSFunctions"
                    SCSFunct.DataBind()
                    FunctionLabel.Text = "Split Case Stock Functions"

                    SCSFunct.Visible = True
                    AddFunction.Visible = False
                    EditFunct.Visible = False
                    SideNavigation.Visible = True
                End If

            Finally
                dbConn.Close()
            End Try

        ElseIf e.CommandName = "CancelThis" Then


            Try
                dbConn.Open()
                FunctCount = Convert.ToInt32(dbComm.ExecuteScalar())
                If FunctCount = 0 Then
                    'Make Add Details View visible everything else invisible
                    FunctionLabel.Text = "Add a Split Case Stock Function"
                    EditFunct.Visible = "False"
                    AddFunction.Visible = True

                    SCSFunct.Visible = False
                    SideNavigation.Visible = False

                    Dim FunctionTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetFunction")
                    Dim RateTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetRate")
                    Dim DescriptionTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetDescrip")

                    FunctionTextBox.Text = ""
                    RateTextBox.Text = ""
                    DescriptionTextBox.Text = ""

                Else
                    SCSFunct.DataSourceID = "SCSFunctions"
                    SCSFunct.DataBind()
                    FunctionLabel.Text = "Split Case Stock Functions"

                    SCSFunct.Visible = True
                    AddFunction.Visible = False
                    EditFunct.Visible = False
                    SideNavigation.Visible = True
                End If

            Finally
                dbConn.Close()
            End Try
        End If
    End Sub
    Protected Sub EditFunct_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdatedEventArgs)

        SCSFunct.DataSourceID = "SCSFunctions"
        SCSFunct.DataBind()
        SCSFunct.Visible = True
        EditFunct.Visible = False

        FunctionLabel.Visible = True
        SideNavigation.Visible = True

        FunctionLabel.Text = "Split Case Stock Functions"


    End Sub

    Protected Sub AddFunction_ModeChanging(ByVal sender As Object, ByVal e As DetailsViewModeEventArgs)

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        'Count the # of Functions listed for this department
        Dim CountSqlString As String
        CountSqlString = "SELECT Count(Functions.Function) AS CountOfFunctions FROM Functions WHERE Functions.Department =9"
        Dim FunctCount As Integer = 0
        Dim dbComm As New OleDbCommand(CountSqlString, dbConn)

        Try
            dbConn.Open()
            FunctCount = Convert.ToInt32(dbComm.ExecuteScalar())
            If FunctCount = 0 Then
                'Make Add Details View visible everything else invisible
                FunctionLabel.Text = "Add a Split Case Stock Function"
                AddFunction.Visible = True
                SCSFunct.Visible = False
                SideNavigation.Visible = False

                Dim FunctionTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetFunction")
                Dim RateTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetRate")
                Dim DescriptionTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetDescrip")

                FunctionTextBox.Text = ""
                RateTextBox.Text = ""
                DescriptionTextBox.Text = ""

            Else
                SCSFunct.DataSourceID = "SCSFunctions"
                SCSFunct.DataBind()
                FunctionLabel.Text = "Split Case Stock Functions"

                SCSFunct.Visible = True
                AddFunction.Visible = False
                EditFunct.Visible = False
                SideNavigation.Visible = True
            End If

        Finally
            dbConn.Close()
        End Try



    End Sub
    Protected Sub AddFunction_ItemInserting(ByVal sender As Object, _
         ByVal e As DetailsViewInsertEventArgs)

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection String

        Dim InsertSqlString As String
        InsertSqlString = "INSERT INTO Functions (Function, Rate, Description, Department) VALUES (@Function, @Rate, @Description, @Dept)"

        Dim RecordCount As Int32 = 0

        Dim sqlStrFunction As String
        sqlStrFunction = "SELECT Count(Functions.Function) AS CountOfFunction FROM Functions WHERE (Functions.Function) = @Function AND (Functions.Department=9)"

        Dim FunctionTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetFunction")
        Dim RateTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetRate")
        Dim DescriptionTextBox As TextBox = Page.FindControl("ctl00$MainContent$AddFunction$DetDescrip")

        Dim dbCommFunction As New OleDbCommand(sqlStrFunction, dbConn)
        dbCommFunction.Parameters.Add("@Function", OleDbType.VarWChar)
        dbCommFunction.Parameters("@Function").Value = FunctionTextBox.Text

        Dim dbCommInsert As New OleDbCommand(InsertSqlString, dbConn)
        dbCommInsert.Parameters.Add("@Function", OleDbType.VarWChar)
        dbCommInsert.Parameters("@Function").Value = FunctionTextBox.Text
        dbCommInsert.Parameters.Add("@Rate", OleDbType.VarWChar)
        dbCommInsert.Parameters("@Rate").Value = Convert.ToInt32(RateTextBox.Text)
        dbCommInsert.Parameters.Add("@Description", OleDbType.VarWChar)
        dbCommInsert.Parameters("@Description").Value = DescriptionTextBox.Text
        dbCommInsert.Parameters.Add("@Dept", OleDbType.Integer)
        dbCommInsert.Parameters("@Dept").Value = Convert.ToInt32(9)

        Try
            dbConn.Open()
            RecordCount = Convert.ToInt32(dbCommFunction.ExecuteScalar())

            If RecordCount <> 0 Then

                FunctionLabel.Text = "Function already exists."
                FunctionTextBox.Text = ""
                RateTextBox.Text = ""
                DescriptionTextBox.Text = ""
                dbConn.Close()
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


        SCSFunct.DataSourceID = "SCSFunctions"
        SCSFunct.DataBind()
        FunctionLabel.Text = "Split Case Stock Functions"

        SCSFunct.Visible = True
        AddFunction.Visible = False
        SideNavigation.Visible = True

    End Sub
End Class
