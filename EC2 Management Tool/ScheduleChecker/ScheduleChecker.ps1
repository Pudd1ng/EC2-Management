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
    $hourHash = @{}
    $dayHash  = @{}
    foreach ($row in $dataRows)
    {
        $hourHash.Add($row.ServerId,$row.Hours)
    }
    foreach ($row in $dataRows)
    {
        $dayHash.Add($row.ServerId,$row.Days)
    }
    $datahash.Add("dayHash",$dayHash)
    $datahash.Add("hourHash", $hourHash)
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

    foreach($server in $data.Item("dayHash"))
    {
        foreach($h in $server.GetEnumerator())
        {
        $chararray = [char[]]$h.Value
            switch ($currentDay)
            {
                Monday 
                {
                    if($chararray[0] -eq "1")
                    {
                        $sh = scheduleHour($data)
                    }
                }
                Tuesday 
                {
                    if($chararray[1] -eq "1")
                    {
                        $sh = scheduleHour($data)
                    }
                }
                Wednesday 
                {
                    if($chararray[2] -eq "1")
                    {
                        $sh = scheduleHour($data)
                    }

                }
                Thursday 
                {
                    if($chararray[3] -eq "1")
                    {
                        $sh = scheduleHour($data)
                    }
                }
                Friday 
                {
                    if($chararray[4] -eq "1")
                    {
                        $sh = scheduleHour($data)
                    }

                }
                Saturday 
                {
                    if($chararray[5] -eq "1")
                    {
                        $sh = scheduleHour($data)
                    }

                }
                Sunday 
                {
                    if($chararray[6] -eq 1)
                    {
                        $sh = scheduleHour($data)
                    }

                }
            }
        }
    }
}


function scheduleHour($data)
{
    foreach($server in $data.Item("hourHash"))
    {
        foreach ($h in $server.GetEnumerator())
        {
            $scheduleArray = [char[]]$h.Value
            $serverId = $h.Name
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
}
      

function Main()
{
    $connectionString = GetConnectionString
    $sqlConnection = CreateSQLConnection $connectionString 
    $data = SelectDataSet $sqlConnection
    $scheduleSet = RunSchedules $data
}

Main