
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("Customers")>
Public Class Customers
    <Key>
    Public Property CustomerID As String
    <Required(ErrorMessage:="Required CompanyName")>
    Public Property CompanyName As String
    <Required(ErrorMessage:="Required ContactName")>
    Public Property ContactName As String
    <Required(ErrorMessage:="Required ContactTitle")>
    Public Property ContactTitle As String
    Public Property Address As String
    <Required(ErrorMessage:="Required City")>
    Public Property City As String
    Public Property Region As String
    <Required(ErrorMessage:="Required PostalCode")>
    Public Property PostalCode As String
    <Required(ErrorMessage:="Required Country")>
    Public Property Country As String
    <Required(ErrorMessage:="Required Phone")>
    Public Property Phone As String
    Public Property Fax As String
End Class
