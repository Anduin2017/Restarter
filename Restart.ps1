Param
(
    [Parameter(Mandatory = $false)]
    [String]$ComputerName
)

$s = New-PSSession -ComputerName $ComputerName
Invoke-Command -Session $s -Argu $ComputerName -ScriptBlock `
{
    param ($ComputerName)
    Write-Host "Restarting $ComputerName..."
    shutdown -r -t 0
    Write-Host "Successfully Restarted $ComputerName..."
}
Remove-PSSession $s