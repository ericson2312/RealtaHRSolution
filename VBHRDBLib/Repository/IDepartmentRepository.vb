Imports VBHRDBLib.Model

Namespace Repository
    Public Interface IDepartmentRepository

        Function CreateDepartment(ByVal dept As Department) As Department

        Function DeleteDepartment(ByVal id As Int32) As Int32

        Function FindAllDepartment() As List(Of Department)

        Function FindDepartmentById(ByVal id As Int32) As Department

        Function UpdateDepartmentBySp(id As Integer, name As String, Optional modDate As Date = Nothing, Optional showCommand As Boolean = False) As Boolean

    End Interface
End Namespace