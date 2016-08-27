Imports System.Data.OleDb
Imports System.Web.UI.Page
Imports System.Data
Imports System.Configuration

Partial Class Utility
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        Dim TMCount As Integer = 0

        Dim CountSqlString As String
        CountSqlString = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE (TMS.ShiftID)=3"

        Dim dbComm As New OleDbCommand(CountSqlString, dbConn)

        Try
            dbConn.Open()
            TMCount = Convert.ToInt32(dbComm.ExecuteScalar())
            TMCountLabel.Text = TMCount & " Team Members"

        Finally
            dbConn.Close()
        End Try
    End Sub
   
End Class