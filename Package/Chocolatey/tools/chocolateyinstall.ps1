$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.4.2/SmartContextMenu_v1.4.2.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = '2818d9651da10cd9467a5e4df21748c812872c1733b6825907356102f422bcec'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
