Namespace Model
    Public Class EmployeeDepartmentHistory
        Private _edhiId As Integer
        Private _edhiEmpId As Integer
        Private _edhiDeptId As Integer
        Private _edhiShiftId As Integer
        Private _edhiStartDate As Date
        Private _edhiEndDate As Date
        Private _edhiModifiedDate As Date
        Public Sub New()
        End Sub

        Public Sub New(edhiId As Integer, edhiEmpId As Integer, edhiDeptId As Integer, edhiShiftId As Integer, Optional edhiStartDate As Date = Nothing,
                       Optional edhiEndDate As Date = Nothing, Optional edhiModifiedDate As Date = Nothing)
            Me.EdhiId = edhiId
            Me.EdhiEmpId = edhiEmpId
            Me.EdhiDeptId = edhiDeptId
            Me.EdhiShiftId = edhiShiftId
            Me.EdhiStartDate = edhiStartDate
            Me.EdhiEndDate = edhiEndDate
            Me.EdhiModifiedDate = edhiModifiedDate
        End Sub

        Public Property EdhiId As Integer
            Get
                Return _edhiId
            End Get
            Set(value As Integer)
                _edhiId = value
            End Set
        End Property

        Public Property EdhiEmpId As Integer
            Get
                Return _edhiEmpId
            End Get
            Set(value As Integer)
                _edhiEmpId = value
            End Set
        End Property

        Public Property EdhiDeptId As Integer
            Get
                Return _edhiDeptId
            End Get
            Set(value As Integer)
                _edhiDeptId = value
            End Set
        End Property

        Public Property EdhiShiftId As Integer
            Get
                Return _edhiShiftId
            End Get
            Set(value As Integer)
                _edhiShiftId = value
            End Set
        End Property

        Public Property EdhiStartDate As Date
            Get
                Return _edhiStartDate
            End Get
            Set(value As Date)
                _edhiStartDate = value
            End Set
        End Property

        Public Property EdhiEndDate As Date
            Get
                Return _edhiEndDate
            End Get
            Set(value As Date)
                _edhiEndDate = value
            End Set
        End Property

        Public Property EdhiModifiedDate As Date
            Get
                Return _edhiModifiedDate
            End Get
            Set(value As Date)
                _edhiModifiedDate = value
            End Set
        End Property
    End Class
End Namespace