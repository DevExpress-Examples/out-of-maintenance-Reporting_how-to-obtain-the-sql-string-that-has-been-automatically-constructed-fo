#Region "#Reference"
Imports System
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraReports.UI
' ...
#End Region ' #Reference

Namespace TableQueryGetSql
    Partial Public Class XtraReport1
        Inherits DevExpress.XtraReports.UI.XtraReport

        Public Sub New()
            InitializeComponent()
        End Sub

        #Region "#Code"
        Private Sub XtraReport1_DataSourceDemanded(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.DataSourceDemanded
            ' Create a new SQL data source and specify its connection parameters.
            Dim sqlDataSource = New SqlDataSource(New Access97ConnectionParameters() With {.FileName = "..//..//nwind.mdb"})

            ' Create a query and assign it to the data source.
            Dim tableQuery As New TableQuery("Categories")
            Dim table As New TableInfo("Categories")
            table.SelectColumns("CategoryID", "CategoryName")
            tableQuery.Tables.Add(table)
            sqlDataSource.Queries.Add(tableQuery)

            '  Open the connection to the data source.
            sqlDataSource.Connection.Open()
            ' Obtain the database schema of the connected data source.
            Dim sql = tableQuery.GetSql(sqlDataSource.Connection.GetDBSchema())

            ' Assign the data source to the report.
            Me.DataSource = sqlDataSource
            Me.DataMember = "Categories"
        End Sub
        #End Region ' #Code


        Private Sub Detail_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
            ' Display the obtained data in the report.
            Me.xrLabel1.DataBindings.AddRange(New XRBinding() { New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Categories.CategoryName")})
        End Sub

    End Class
End Namespace
