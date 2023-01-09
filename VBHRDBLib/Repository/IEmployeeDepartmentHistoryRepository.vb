Imports VBHRDBLib.Model

Namespace Repository
    Public Interface IEmployeeDepartmentHistoryRepository
        Function CreateEmployeeDepartmentHistory(ByVal edhi As EmployeeDepartmentHistory) As EmployeeDepartmentHistory

        Function DeleteEmployeeDepartmentHistory(ByVal id As Int32) As Int32

        Function FindAllEmployeeDepartmentHistory() As List(Of EmployeeDepartmentHistory)

        Function FindEmployeeDepartmentHistoryById(ByVal id As Int32) As EmployeeDepartmentHistory

        Function UpdateEmployeeDepartmentHistoryBySp(edhiId As Integer, empId As Integer, deptId As Integer, shiftId As Integer, Optional startDate As Date = Nothing, Optional endDate As Date = Nothing, Optional modDate As Date = Nothing, Optional showCommand As Boolean = False) As Boolean

    End Interface
End Namespace