Imports VBHRDBLib.Context
Imports VBHRDBLib.Repository

Namespace Base
    Public Class RepositoryManager
        Implements IRepositoryManager

        Private _jobRoleRepository As IJobRoleRepository
        Private _shiftRepository As IShiftRepository
        Private _departmentRepository As IDepartmentRepository
        Private _employeePayHistoryRepository As IEmployeePayHistoryRepository
        Private _employeeDepartmentHistoryRepository As IEmployeeDepartmentHistoryRepository
        Private ReadOnly _repositoryContext As IRepositoryContext

        Public Sub New(repositoryContext As IRepositoryContext)
            _repositoryContext = repositoryContext
        End Sub

        Public ReadOnly Property JobRole As IJobRoleRepository Implements IRepositoryManager.JobRole
            Get
                If _jobRoleRepository Is Nothing Then
                    _jobRoleRepository = New JobRoleRepository(_repositoryContext)
                End If
                Return _jobRoleRepository
            End Get
        End Property

        Public ReadOnly Property Shift As IShiftRepository Implements IRepositoryManager.Shift
            Get
                If _shiftRepository Is Nothing Then
                    _shiftRepository = New ShiftRepository(_repositoryContext)
                End If
                Return _shiftRepository
            End Get
        End Property

        Public ReadOnly Property Department As IDepartmentRepository Implements IRepositoryManager.Department
            Get
                If _departmentRepository Is Nothing Then
                    _departmentRepository = New DepartmentRepository(_repositoryContext)
                End If
                Return _departmentRepository
            End Get
        End Property

        Public ReadOnly Property EmployeePayHistory As IEmployeePayHistoryRepository Implements IRepositoryManager.EmployeePayHistory
            Get
                If _employeePayHistoryRepository Is Nothing Then
                    _employeePayHistoryRepository = New EmployeePayHistoryRepository(_repositoryContext)
                End If
                Return _employeePayHistoryRepository
            End Get
        End Property

        Public ReadOnly Property EmployeeDepartmentHistory As IEmployeeDepartmentHistoryRepository Implements IRepositoryManager.EmployeeDepartmentHistory
            Get
                If _employeeDepartmentHistoryRepository Is Nothing Then
                    _employeeDepartmentHistoryRepository = New EmployeeDepartmentHistoryRepository(_repositoryContext)
                End If
                Return _employeeDepartmentHistoryRepository
            End Get
        End Property
    End Class
End Namespace