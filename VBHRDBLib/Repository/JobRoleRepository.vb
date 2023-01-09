Imports System.Data.SqlClient
Imports VBHRDBLib.Context
Imports VBHRDBLib.Model

Namespace Repository
    Public Class JobRoleRepository
        Implements IJobRoleRepository

        Private ReadOnly _context As IRepositoryContext

        Public Sub New(context As IRepositoryContext)
            _context = context
        End Sub

        Public Function CreateJobRole(jobRole As JobRole) As JobRole Implements IJobRoleRepository.CreateJobRole
            Dim newJobRole As New JobRole()

            Dim stmt As String = "SET IDENTITY_INSERT hr.job_role ON; " &
                                 "Insert into hr.job_role(joro_id, joro_name, joro_modified_date) values (@id, @name, @modDate); " &
                                 "select cast (scope_identity() as int); " &
                                 "SET IDENTITY_INSERT hr.job_role OFF; "

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", jobRole.JoroId)
                    cmd.Parameters.AddWithValue("@name", jobRole.JoroName)
                    cmd.Parameters.AddWithValue("@modDate", jobRole.JoroModifiedDate)

                    Try
                        conn.Open()
                        'ExecuteScalar return 1 row and get first column
                        Dim regionId As Int32 = Convert.ToInt32(cmd.ExecuteScalar())
                        newJobRole = FindJobRoleById(jobRole.JoroId)

                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return newJobRole
        End Function

        Public Function DeleteJobRole(id As Integer) As Integer Implements IJobRoleRepository.DeleteJobRole
            Dim rowEffect As Int32 = 0

            'declare stnt
            Dim stmt As String = "delete from hr.job_role where joro_id = @id;"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", id)

                    Try
                        conn.Open()
                        rowEffect = cmd.ExecuteNonQuery()

                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return rowEffect
        End Function

        Public Function FindAllJobRole() As List(Of JobRole) Implements IJobRoleRepository.FindAllJobRole
            Dim jobRoleList As New List(Of JobRole)

            'declare statement
            Dim stmt As String = "select joro_id, joro_name, joro_modified_date from hr.job_role " &
                                "order by joro_id asc;"

            'try to connect
            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()

                        While reader.Read()
                            jobRoleList.Add(New JobRole() With {
                                .JoroId = reader.GetInt32(0),
                                .JoroName = reader.GetString(1),
                                .JoroModifiedDate = reader.GetDateTime(2)
                            })
                        End While

                        reader.Close()
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return jobRoleList
        End Function

        Public Function FindAllJobRoleAsync() As Task(Of List(Of JobRole)) Implements IJobRoleRepository.FindAllJobRoleAsync
            Throw New NotImplementedException()
        End Function

        Public Function FindJobRoleById(id As Integer) As JobRole Implements IJobRoleRepository.FindJobRoleById
            Dim jobRole As New JobRole With {.JoroId = id}

            'sql statement
            Dim stmt As String = "Select joro_id, joro_name, joro_modified_date from hr.job_role " &
                                 "where joro_id = @joroId;"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@joroId", id)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()
                            jobRole.JoroName = reader.GetString(1)
                            jobRole.JoroModifiedDate = reader.GetDateTime(2)
                        End If

                        reader.Close()
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return jobRole
        End Function

        Public Function UpdateJobRoleBySp(id As Integer, name As String, Optional modDate As Date = #1/1/0001 12:00:00 AM#, Optional showCommand As Boolean = False) As Boolean Implements IJobRoleRepository.UpdateJobRoleBySp
            'declare stnt
            Dim stmt As String = "hr.spUpdateJobRole @id, @name, @modDate"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", id)
                    cmd.Parameters.AddWithValue("@name", name)
                    cmd.Parameters.AddWithValue("@modDate", modDate)

                    'show command
                    If showCommand Then
                        Console.WriteLine(cmd.CommandText)
                    End If

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()

                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return True
        End Function
    End Class
End Namespace