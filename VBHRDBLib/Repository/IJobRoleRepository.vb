Imports VBHRDBLib.Model

Namespace Repository
    Public Interface IJobRoleRepository
        Function CreateJobRole(ByVal jobRole As JobRole) As JobRole

        Function DeleteJobRole(ByVal id As Int32) As Int32

        Function FindAllJobRole() As List(Of JobRole)

        Function FindAllJobRoleAsync() As Task(Of List(Of JobRole))

        Function FindJobRoleById(ByVal id As Int32) As JobRole

        Function UpdateJobRoleBySp(id As Integer, name As String, Optional modDate As Date = Nothing, Optional showCommand As Boolean = False) As Boolean

    End Interface
End Namespace