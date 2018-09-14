Imports System.Data.Entity

Public Class DatabaseContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("DBConnection")
    End Sub

    Public Property Customers As DbSet(Of Customers)
End Class
