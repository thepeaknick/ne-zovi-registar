
$confirmation = Read-Host "Start migration?[y/n]"
if ($confirmation -eq 'y') {
	NeZoviReg.Migrations.Ef.exe
}
