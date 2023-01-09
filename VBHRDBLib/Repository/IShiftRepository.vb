Imports VBHRDBLib.Model

Namespace Repository
    Public Interface IShiftRepository
        Function CreateShift(ByVal shift As Shift) As Shift

        Function DeleteShift(ByVal id As Int32) As Int32

        Function FindAllShift() As List(Of Shift)

        Function FindShiftById(ByVal id As Int32) As Shift

        Function UpdateShiftBySp(id As Integer, name As String, startTime As DateTime, endTime As DateTime, Optional showCommand As Boolean = False) As Boolean

    End Interface
End Namespace