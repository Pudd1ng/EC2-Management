function GetConnectionString()
{
    $connectionString = "Data Source=.;Initial Catalog=EC2ManagementTool;Integrated Security=True"
    return $connectionString
}

function CreateSQLConnection($connectionString)
{
    $sqlConnection = New-Object System.Data.SqlClient.SqlConnection
    $sqlConnection.ConnectionString = $connectionString
    return $sqlConnection
}

function SelectDataSet($sqlConnection)
{
    $sqlConnection.Open()
    $sqlCmd = New-Object System.Data.SqlClient.SqlCommand
    $sqlCmd.CommandText = "SELECT ScheduledServer.ServerId, ScheduledServer.ScheduleName , Schedule.Hours, Schedule.Days
                           FROM ScheduledServer
                           INNER JOIN Schedule
                           ON ScheduledServer.ScheduleName = Schedule.ScheduleName"
    $sqlCmd.Connection = $sqlConnection
    $dataItems = $sqlCmd.ExecuteReader()
    $dataTable = New-Object System.Data.DataTable
    $dataTable.Load($dataItems) 
    $dataRows = $dataTable.Select()
    $dataHash = @{}
    foreach ($row in $dataRows)
    {
        $dataHash.Add($row.InstanceId,$row.Hours, $row.Days)
    }
    return $dataHash  
}

function ToArray
{
    begin
    {
        $output = @();
    }
    process
    {
        $output += $_;
    }
    end
    {
        return $output;
    }
}

function RunSchedules($data)
{
    $currentDate = Get-Date
    $currentDay = $currentDate.DayOfWeek
    $currentHour = $currentDate.Hour
    $previousHour = $currentHour - 1
    foreach ($server in $data.GetEnumerator())
    {
        $scheduleArray = $server.Value | ToArray
        $serverId = $server.Name
        if(($scheduleArray[$currenthour] -eq "1") -and ($scheduleArray[$previousHour] -eq "0"))
        {
            Start-EC2Instance $serverId
        }
        if(($scheduleArray[$currenthour] -eq "0") -and ($scheduleArray[$previousHour] -eq "1"))
        {
            Stop-EC2Instance $serverId
        }
    }
}

function Main()
{
    $connectionString = GetConnectionString
    $sqlConnection = CreateSQLConnection $connectionString 
    $data = SelectDataSet $sqlConnection
    $scheduleSet = RunSchedules $data
}

Main