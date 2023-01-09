Namespace Model
    Public Class Shift
        Private _shiftId As Integer
        Private _shiftName As String
        Private _shiftStartTime As DateTime
        Private _shiftEndTime As DateTime

        Public Sub New()
        End Sub

        Public Sub New(shiftId As Integer, shiftName As String, shiftStartTime As DateTime, shiftEndTime As DateTime)
            Me.ShiftId = shiftId
            Me.ShiftName = shiftName
            Me.ShiftStartTime = shiftStartTime
            Me.ShiftEndTime = shiftEndTime
        End Sub

        Public Property ShiftId As Integer
            Get
                Return _shiftId
            End Get
            Set(value As Integer)
                _shiftId = value
            End Set
        End Property

        Public Property ShiftName As String
            Get
                Return _shiftName
            End Get
            Set(value As String)
                _shiftName = value
            End Set
        End Property

        Public Property ShiftStartTime As DateTime
            Get
                Return _shiftStartTime
            End Get
            Set(value As DateTime)
                _shiftStartTime = value
            End Set
        End Property

        Public Property ShiftEndTime As DateTime
            Get
                Return _shiftEndTime
            End Get
            Set(value As DateTime)
                _shiftEndTime = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return $"ShiftId : {ShiftId} | ShiftName : {ShiftName} | ShiftStartTime : {ShiftStartTime} | ShiftEndTime : {ShiftEndTime}"
        End Function

    End Class
End Namespace