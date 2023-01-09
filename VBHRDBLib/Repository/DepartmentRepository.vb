Imports System.Data.SqlClient
Imports VBHRDBLib.Context
Imports VBHRDBLib.Model

Namespace Repository
    Public Class DepartmentRepository
        Implements IDepartmentRepository

        Private ReadOnly _context As IRepositoryContext

        Public Sub New(context As IRepositoryContext)
            _context = context
        End Sub

        Public Function CreateDepartment(dept As Department) As Department Implements IDepartmentRepository.CreateDepartment
            Dim newDepartment As New Department()

            'declare stnt

            Dim stmt As String = "SET IDENTITY_INSERT hr.department ON; " &
                                 "Insert into hr.department(dept_id, dept_name, dept_modified_date) values (@id, @name, @modDate); " &
                                 "select cast (scope_identity() as int); " &
                                 "SET IDENTITY_INSERT hr.department OFF; "

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", dept.DeptId)
                    cmd.Parameters.AddWithValue("@name", dept.DeptName)
                    cmd.Parameters.AddWithValue("@modDate", dept.ModDate)

                    Try
                        conn.Open()
                        'ExecuteScalar return 1 row and get first column
                        Dim regionId As Int32 = Convert.ToInt32(cmd.ExecuteScalar())
                        newDepartment = FindDepartmentById(dept.DeptId)

                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return newDepartment
        End Function

        Public Function DeleteDepartment(id As Integer) As Integer Implements IDepartmentRepository.DeleteDepartment
            Dim rowEffect As Int32 = 0

            'declare stnt
            Dim stmt As String = "delete from hr.department where dept_id = @id;"

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

        Public Function FindAllDepartment() As List(Of Department) Implements IDepartmentRepository.FindAllDepartment
            Dim departmentList As New List(Of Department)

            'declare statement
            Dim stmt As String = "select dept_id, dept_name, dept_modified_date from hr.department " &
                                "order by dept_id asc;"

            'try to connect
            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()

                        While reader.Read()
                            departmentList.Add(New Department() With {
                                .DeptId = reader.GetInt32(0),
                                .DeptName = reader.GetString(1),
                                .ModDate = reader.GetDateTime(2)
                            })
                        End While

                        reader.Close()
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return departmentList
        End Function

        Public Function FindDepartmentById(id As Integer) As Department Implements IDepartmentRepository.FindDepartmentById
            Dim department As New Department With {.DeptId = id}

            'sql statement
            Dim stmt As String = "Select dept_id, dept_name, dept_modified_date from hr.department " &
                                 "where dept_id = @id;"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", id)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()
                            department.DeptName = reader.GetString(1)
                            department.ModDate = reader.GetDateTime(2)
                        End If

                        reader.Close()
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return department
        End Function

        Public Function UpdateDepartmentBySp(id As Integer, name As String, Optional modDate As Date = #1/1/0001 12:00:00 AM#, Optional showCommand As Boolean = False) As Boolean Implements IDepartmentRepository.UpdateDepartmentBySp
            'declare stnt
            Dim stmt As String = "hr.spUpdateDepartment @id, @name, @modDate"


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