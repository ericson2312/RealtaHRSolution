Imports VBHRDBLib.Base
Imports VBHRDBLib.Context

Public Class HRVbApi
    Implements IHRVbApi

    Private Property _repositoryManager As IRepositoryManager

    Private ReadOnly _repositoryContext As IRepositoryContext

    Public Sub New(ByVal connString As String)
        Console.WriteLine($"cs : {connString}")
        If _repositoryContext Is Nothing Then
            _repositoryContext = New RepositoryContext(connString)
        End If
    End Sub
    Public ReadOnly Property RepositoryManager As IRepositoryManager Implements IHRVbApi.RepositoryManager
        Get
            If _repositoryManager Is Nothing Then
                _repositoryManager = New RepositoryManager(_repositoryContext)
            End If
            Return _repositoryManager
        End Get
    End Property

End Class
