$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.3.2/SmartContextMenu_v1.3.2.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = '4f0379423c37e4c0fbc7f00b66efdae7a22d308fab4cd00e0535771ae896bd41'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
