Imports VBHRDBLib.Repository

Namespace Base
    Public Interface IRepositoryManager
        ReadOnly Property JobRole As IJobRoleRepository
        ReadOnly Property Shift As IShiftRepository
        ReadOnly Property Department As IDepartmentRepository
        ReadOnly Property EmployeePayHistory As IEmployeePayHistoryRepository
        ReadOnly Property EmployeeDepartmentHistory As IEmployeeDepartmentHistoryRepository

    End Interface
End Namespace