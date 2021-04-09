
--Implementierung--

boersen_geschaeft.proto - Prototype class, dort werden der gRPC Service, Request Methoden und die
						  Response-Typen deklariert. Beim Build wird die Klasse BoersenGeschaeftGrpc
						  automatisch erzeugt, welche Hilfsmethoden generiert.
						  
						  
BoersenGeschaeftUtil.cs - Klasse, die Hilfsmethoden enthaelt(ReadFromRessource,Exists).

boersen_geschaeft_db.json - enthaelt Testdaten. 

BoersenGeschaeftImpl.cs - stellt die Implementierung des Servers bereit, ueberschreibt die
						  Methoden des gRPC Services(BoersenGeschaeft)
						  
BoersenGeschaeftServer.Program.cs - Testdaten laden, Port Angabe und Freigabe, 
									Server erstellen, Starten des Servers
									
BoersenGeschaeftClient.Program.cs - Implementierung des Clients und der Methoden, die im Client
									aufgerufen werden. Nachdem ein Client erstellt wird,
									werden die Methoden aufgerufen.
									
									
--Projektstart--									

1. Projekt Build im Verzeichnis BoersenGeschaeft : dotnet build BoersenGeschaeft.sln

2. Server starten im Verzeichnis BoersenGeschaeft/BoersenGeschaeftServer :  dotnet run -f netcoreapp2.1

3. Client starten im Verzeichnis BoersenGeschaeft/BoersenGeschaeftClient :  dotnet run -f netcoreapp2.1

!!! Testdaten koennen unter boersen_geschaeft_db.json editiert werden. In Abhaengigkeit vom Zeitstempel der Testdaten erfolgt die entsprechende Ausgabe !!!