Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Data
Imports System.Runtime.CompilerServices

Namespace DemoDatatables.Utils
    Module DataTableHelper
        <Extension()>
        Function ToObject(Of T As {Class, New})(ByVal row As DataRow) As T
            Dim obj As T = New T()

            For Each prop In obj.[GetType]().GetProperties()

                Try

                    If prop.PropertyType.IsGenericType AndAlso prop.PropertyType.Name.Contains("Nullable") Then
                        If Not String.IsNullOrEmpty(row(prop.Name).ToString()) Then prop.SetValue(obj, Convert.ChangeType(row(prop.Name), Nullable.GetUnderlyingType(prop.PropertyType), Nothing))
                    Else
                        prop.SetValue(obj, Convert.ChangeType(row(prop.Name), prop.PropertyType), Nothing)
                    End If

                Catch
                    Continue For
                End Try
            Next

            Return obj
        End Function

        <Extension()>
        Function DataTableToList(Of T As {Class, New})(ByVal table As DataTable) As List(Of T)
            Try
                Dim list As List(Of T) = New List(Of T)()

                For Each row In table.AsEnumerable()
                    Dim obj = row.ToObject(Of T)()
                    list.Add(obj)
                Next

                Return list
            Catch
                Return Nothing
            End Try
        End Function
    End Module
End Namespace
