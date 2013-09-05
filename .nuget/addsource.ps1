$user = Read-Host 'Username'
$passhash = Read-Host 'Password' -AsSecureString
$passclear = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($passhash))
.\nuget sources add -name "vidview-dev" -source https://www.myget.org/F/vidview-dev -ConfigFile .\Nuget.Config
.\nuget sources update -Name vidview-dev -User $user -pass $passclear
.\nuget sources add -name "kean" -source https://www.myget.org/F/kean -ConfigFile .\Nuget.Config
.\nuget sources update -Name kean -User $user -pass $passclear

pause