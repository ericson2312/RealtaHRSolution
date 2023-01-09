Namespace Model
    Public Class JobRole
        Private _joroId As Integer
        Private _joroName As String
        Private _joroModifiedDate As Date

        Public Sub New()
        End Sub

        Public Sub New(joroName As String, Optional joroModifiedDate As Date = Nothing)
            Me.JoroName = joroName
            Me.JoroModifiedDate = joroModifiedDate
        End Sub

        Public Sub New(joroId As Integer, joroName As String, Optional joroModifiedDate As Date = Nothing)
            Me.JoroId = joroId
            Me.JoroName = joroName
            Me.JoroModifiedDate = joroModifiedDate
        End Sub

        Public Overrides Function ToString() As String
            Return $"JoroId : {JoroId} | JoroName : {JoroName} | JoroModifiedDate : {JoroModifiedDate}"
        End Function

        Public Property JoroId As Integer
            Get
                Return _joroId
            End Get
            Set(value As Integer)
                _joroId = value
            End Set
        End Property

        Public Property JoroName As String
            Get
                Return _joroName
            End Get
            Set(value As String)
                _joroName = value
            End Set
        End Property

        Public Property JoroModifiedDate As Date
            Get
                Return _joroModifiedDate
            End Get
            Set(value As Date)
                _joroModifiedDate = value
            End Set
        End Property
    End Class
End Namespace