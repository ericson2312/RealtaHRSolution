Namespace Model
    Public Class Department

        Private _deptId As Integer
        Private _deptName As String
        Private _modDate As Date

        Public Sub New()
        End Sub

        Public Sub New(deptName As String, Optional modDate As Date = Nothing)
            Me.DeptName = deptName
            Me.ModDate = modDate
        End Sub

        Public Sub New(deptId As Integer, deptName As String, Optional modDate As Date = Nothing)
            Me.DeptId = deptId
            Me.DeptName = deptName
            Me.ModDate = modDate
        End Sub

        Public Property DeptId As Integer
            Get
                Return _deptId
            End Get
            Set(value As Integer)
                _deptId = value
            End Set
        End Property

        Public Property DeptName As String
            Get
                Return _deptName
            End Get
            Set(value As String)
                _deptName = value
            End Set
        End Property

        Public Property ModDate As Date
            Get
                Return _modDate
            End Get
            Set(value As Date)
                _modDate = value
            End Set
        End Property
    End Class
End Namespace