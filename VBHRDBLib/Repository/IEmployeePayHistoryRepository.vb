Imports VBHRDBLib.Model

Namespace Repository
    Public Interface IEmployeePayHistoryRepository
        Function CreateEmployeePayHistory(ByVal ephi As EmployeePayHistory) As EmployeePayHistory

        Function DeleteEmployeePayHistory(ByVal id As Int32) As Int32

        Function FindAllEmployeePayHistory() As List(Of EmployeePayHistory)

        Function FindEmployeePayHistoryById(ByVal id As Int32) As EmployeePayHistory

        Function UpdateEmployeePayHistoryBySp(id As Integer, rateChangeDate As Date, Optional rateSalary As Integer = Nothing, Optional payFrequence As Integer = Nothing,
                                              Optional modDate As Date = Nothing, Optional showCommand As Boolean = False) As Boolean

    End Interface
End Namespace