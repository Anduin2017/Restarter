Param
(
    [Parameter(Mandatory = $false)]
    [String]$ComputerName
)

$s = New-PSSession -ComputerName $ComputerName
Invoke-Command -Session $s -Argu $ComputerName -ScriptBlock `
{
    param ($ComputerName)
    Write-Host "Shutting down $ComputerName..."
    shutdown -s -t 0
    Write-Host "Successfully shut down $ComputerName..."
}
Remove-PSSession $s