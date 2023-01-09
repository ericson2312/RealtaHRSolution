Imports System.Data.SqlClient
Imports VBHRDBLib.Context
Imports VBHRDBLib.Model

Namespace Repository
    Public Class EmployeeDepartmentHistoryRepository
        Implements IEmployeeDepartmentHistoryRepository

        Private ReadOnly _context As IRepositoryContext
        Public Sub New(context As IRepositoryContext)
            _context = context
        End Sub

        Public Function CreateEmployeeDepartmentHistory(edhi As EmployeeDepartmentHistory) As EmployeeDepartmentHistory Implements IEmployeeDepartmentHistoryRepository.CreateEmployeeDepartmentHistory
            Dim newEmployeeDepartmentHistory As New EmployeeDepartmentHistory()

            'declare stnt

            Dim stmt As String = "SET IDENTITY_INSERT hr.employee_department_history ON; " &
                                 "Insert into hr.employee_department_history(edhi_id, edhi_emp_id, edhi_start_date, edhi_end_date, edhi_modified_date, edhi_dept_id, edhi_shift_id) " &
                                 "values (@edhiId, @empId, @startDate, @endDate, @modDate, @deptId, @shiftId); " &
                                 "select cast (scope_identity() as int); " &
                                 "SET IDENTITY_INSERT hr.employee_department_history OFF; "

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@edhiId", edhi.EdhiId)
                    cmd.Parameters.AddWithValue("@empId", edhi.EdhiEmpId)
                    cmd.Parameters.AddWithValue("@startDate", edhi.EdhiStartDate)
                    cmd.Parameters.AddWithValue("@endDate", edhi.EdhiEndDate)
                    cmd.Parameters.AddWithValue("@modDate", edhi.EdhiModifiedDate)
                    cmd.Parameters.AddWithValue("@deptId", edhi.EdhiDeptId)
                    cmd.Parameters.AddWithValue("@shiftId", edhi.EdhiShiftId)

                    Try
                        conn.Open()
                        'ExecuteScalar return 1 row and get first column
                        Dim regionId As Int32 = Convert.ToInt32(cmd.ExecuteScalar())
                        newEmployeeDepartmentHistory = FindEmployeeDepartmentHistoryById(edhi.EdhiId)

                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return newEmployeeDepartmentHistory
        End Function

        Public Function DeleteEmployeeDepartmentHistory(id As Integer) As Integer Implements IEmployeeDepartmentHistoryRepository.DeleteEmployeeDepartmentHistory
            Dim rowEffect As Int32 = 0

            'declare stnt
            Dim stmt As String = "delete from hr.employee_department_history where edhiId = @id;"

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

        Public Function FindAllEmployeeDepartmentHistory() As List(Of EmployeeDepartmentHistory) Implements IEmployeeDepartmentHistoryRepository.FindAllEmployeeDepartmentHistory
            Dim EmployeeDepartmentHistoryList As New List(Of EmployeeDepartmentHistory)

            'declare statement   hr.()
            Dim stmt As String = "select edhi_id, edhi_emp_id, edhi_start_date, edhi_end_date, edhi_modified_date, edhi_dept_id, edhi_shift_id from hr.employee_department_history " &
                                "order by edhi_id asc;"

            'try to connect
            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()

                        While reader.Read()
                            EmployeeDepartmentHistoryList.Add(New EmployeeDepartmentHistory() With {
                                .EdhiId = reader.GetInt32(0),
                                .EdhiEmpId = reader.GetString(1),
                                .EdhiStartDate = reader.GetDateTime(2),
                                .EdhiEndDate = reader.GetDateTime(3),
                                .EdhiModifiedDate = reader.GetDateTime(4),
                                .EdhiDeptId = reader.GetInt32(5),
                                .EdhiShiftId = reader.GetInt32(6)
                            })
                        End While

                        reader.Close()
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return EmployeeDepartmentHistoryList
        End Function

        Public Function FindEmployeeDepartmentHistoryById(id As Integer) As EmployeeDepartmentHistory Implements IEmployeeDepartmentHistoryRepository.FindEmployeeDepartmentHistoryById
            Dim employeeDepartmentHistory As New EmployeeDepartmentHistory With {.EdhiId = id}

            'sql statement
            Dim stmt As String = "select edhi_id, edhi_emp_id, edhi_start_date, edhi_end_date, edhi_modified_date, edhi_dept_id, edhi_shift_id from hr.employee_department_history " &
                                 "where edhi_id = @id;"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", id)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()
                            employeeDepartmentHistory.EdhiEmpId = reader.GetInt32(1)
                            employeeDepartmentHistory.EdhiStartDate = reader.GetDateTime(2)
                            employeeDepartmentHistory.EdhiEndDate = reader.GetDateTime(3)
                            employeeDepartmentHistory.EdhiModifiedDate = reader.GetDateTime(4)
                            employeeDepartmentHistory.EdhiDeptId = reader.GetInt32(5)
                            employeeDepartmentHistory.EdhiShiftId = reader.GetInt32(6)
                        End If

                        reader.Close()
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return employeeDepartmentHistory
        End Function

        Public Function UpdateEmployeeDepartmentHistoryBySp(edhiId As Integer, empId As Integer, deptId As Integer, shiftId As Integer, Optional startDate As Date = #1/1/0001 12:00:00 AM#, Optional endDate As Date = #1/1/0001 12:00:00 AM#, Optional modDate As Date = #1/1/0001 12:00:00 AM#, Optional showCommand As Boolean = False) As Boolean Implements IEmployeeDepartmentHistoryRepository.UpdateEmployeeDepartmentHistoryBySp
            Dim stmt As String = "hr.spUpdateEmployeeDepartmentHistory @edhiId, @empId, @startDate, @endDate, @modDate, @deptId, @shiftId"


            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@edhiId", edhiId)
                    cmd.Parameters.AddWithValue("@empId", empId)
                    cmd.Parameters.AddWithValue("@startDate", startDate)
                    cmd.Parameters.AddWithValue("@endDate", endDate)
                    cmd.Parameters.AddWithValue("@modDate", modDate)
                    cmd.Parameters.AddWithValue("@deptId", deptId)
                    cmd.Parameters.AddWithValue("@shiftId", shiftId)

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