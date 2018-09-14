Imports DemoDatatables.Models
Imports System
Imports System.Linq
Imports System.Web.Mvc
Imports System.Linq.Dynamic
Imports System.Data.Entity
Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions

Namespace Controllers
    Public Class DemoController
        Inherits Controller

        ' GET: Demo

        Public Function ShowGrid() As ActionResult
            Return View()
        End Function

        Public Function LoadData() As ActionResult
            Try

                Using _context As DatabaseContext = New DatabaseContext()
                    Dim draw = Request.Form.GetValues("draw").FirstOrDefault()
                    Dim start = Request.Form.GetValues("start").FirstOrDefault()
                    Dim length = Request.Form.GetValues("length").FirstOrDefault()
                    Dim sortColumn = Request.Form.GetValues("columns[" & Request.Form.GetValues("order[0][column]").FirstOrDefault() & "][name]").FirstOrDefault()
                    Dim sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault()
                    Dim searchValue = Request.Form.GetValues("search[value]").FirstOrDefault()
                    Dim pageSize As Integer = If(length IsNot Nothing, Convert.ToInt32(length), 0)
                    Dim skip As Integer = If(start IsNot Nothing, Convert.ToInt32(start), 0)
                    Dim recordsTotal As Integer = 0
                    Dim customerData = (From tempcustomer In _context.Customers Select tempcustomer)

                    If Not (String.IsNullOrEmpty(sortColumn) AndAlso String.IsNullOrEmpty(sortColumnDir)) Then
                        customerData = customerData.OrderBy(sortColumn & " " + sortColumnDir)
                    End If

                    If Not String.IsNullOrEmpty(searchValue) Then
                        customerData = customerData.Where(Function(m) m.CompanyName.Contains(searchValue) OrElse m.ContactName.Contains(searchValue) OrElse m.Country.Contains(searchValue))
                    End If

                    recordsTotal = customerData.Count()
                    Dim data = customerData.Skip(skip).Take(pageSize).ToList()
                    Return Json(New With {Key .draw = draw, Key .recordsFiltered = recordsTotal, Key .recordsTotal = recordsTotal, Key .data = data
                })
                End Using

            Catch __unusedException1__ As Exception
                Throw
            End Try
        End Function

        <HttpGet>
        Public Function Edit(ByVal ID As String) As ActionResult
            Try

                Using _context As DatabaseContext = New DatabaseContext()
                    Dim Customer = (From c In _context.Customers Where c.CustomerID = ID Select c).FirstOrDefault()
                    Return View(Customer)
                End Using

            Catch __unusedException1__ As Exception
                Throw
            End Try
        End Function

        <HttpPost>
        Public Function DeleteCustomer(ByVal ID As String) As JsonResult
            Using _context As DatabaseContext = New DatabaseContext()
                Dim customer = _context.Customers.Find(ID)
                If ID Is Nothing Then Return Json(data:="Not Deleted", behavior:=JsonRequestBehavior.AllowGet)
                _context.Customers.Remove(customer)
                _context.SaveChanges()
                Return Json(data:="Deleted", behavior:=JsonRequestBehavior.AllowGet)
            End Using
        End Function
    End Class


    Module extensionmethods
        <Extension()>
        Function OrderByField(Of T)(ByVal q As IQueryable(Of T), ByVal SortField As String, ByVal Ascending As Boolean) As IQueryable(Of T)
            Dim param = Expression.Parameter(GetType(T), "p")
            Dim prop = Expression.[Property](param, SortField)
            Dim exp = Expression.Lambda(prop, param)
            Dim method As String = If(Ascending, "OrderBy", "OrderByDescending")
            Dim types As Type() = New Type() {q.ElementType, exp.Body.Type}
            Dim mce = Expression.[Call](GetType(Queryable), method, types, q.Expression, exp)
            Return q.Provider.CreateQuery(Of T)(mce)
        End Function
    End Module

End Namespace