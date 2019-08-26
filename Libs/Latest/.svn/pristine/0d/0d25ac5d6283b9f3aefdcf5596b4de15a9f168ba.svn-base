# ============================================================================
#      Author: James Joyce
# Create date: 21/09/2018
# Description: Updates the Libs with the latest versions
#
# To run: powershell -file UpdateLibs.ps1 <source libs file> MS63.txt
#
#   When     | Who        | What
# ----------------------------------------------------------------------------
#            |            | 
# ============================================================================

function ArgsHelp {
	Write-Host "Use UpdateLibs <source> <file> - eg: UpdateLibs D:\TheSwitch\Reset.ServiceBus\QlikviewRefreshService\Libs\Libs.txt MS63.txt"
	Exit 1000
}

function LoadLatest($latestFile) {
	Write-Host "Loading latest libs from $latestFile"
	
	[hashtable]$latest = @{}

	Get-Content $latestFile | ForEach-Object {
		#Each line will be something like:		^/VendorCode/Common.Logging/v3.0.0/net40 Common.Logging
		#So split it by space
		
		if ($_ -ne '') {		#Ignore empty lines
			$a,$b = $_.split(' ')
			
			#Write-Host "`t$b => $a"
			
			$latest.add($b, $a)
		}
	}
	
	#Write-Host "`n"
	
	return $latest
}

function ReplaceLatest($libsFile, $latest) {
	Write-Host "Replacing latest into $libsFile"
	Get-Content $libsFile | ForEach-Object {
		if ($_ -ne '') {
			$a,$b = $_.split(' ')
			
			$line = ''
			
			#If our Libs contains the "name" (ie Common.Logging)
			if ($latest.ContainsKey($b)) {
				#Write out $latest $b
				$found = $latest[$b]
				
				#For Shield, it has a different Repo - so we need to "tweak" the $found to match
				#		All:	^/VendorCode/Common.Logging/v3.0.0/net40 Common.Logging
				#		Shield:	/svn/VendorCode/Common.Logging/v3.0.0/net40 Common.Logging
				if ($a.StartsWith("/svn/")) {
					$found = $found.Replace("^/", "/svn/")
				}
				
				if ($found -ne $a) {
					$line = "$found $b"
				
					Write-Host "`t$_ => $line"
				}
				else {
					#Otherwise write out the original
					$line = "$a $b"
				}
			}
			else {
				#Otherwise write out the original
				$line = "$a $b"
			}	
			
			Write-Output $line
		}
	}
	
	Write-Host "`n"

}

if ($args.Count -ne '2') {
	ArgsHelp
}

$libsFile = $args[0]
$latestFile = $args[1]

Write-Host "Updating all Libs`n"

$latest = LoadLatest $latestFile

#Replace to temp.$$$
ReplaceLatest $libsFile $latest | Out-File .\Temp.$`$`$ -Encoding ASCII

#Rename temp.$$$ back to the Libs file
Move-Item .\Temp.$`$`$ $libsFile -Force

Write-Host "Updated all Libs`n"

